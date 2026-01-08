using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Infrastructure.Routing;

namespace RazorPagesUI.Api.Clients;

public sealed record ApiCallContext(
    PageModel Page,
    string ReturnUrl,
    bool StayOnForbidden,              
    string? ForbiddenMessage = null,
    bool RedirectNotFoundToIndex = true);

public sealed record ApiResult<T>(T? Data, IActionResult? Action);

public sealed class ApiClient
{
    private readonly IHttpClientFactory _factory;

    public ApiClient(IHttpClientFactory factory) => _factory = factory;

    private HttpClient Create(string? token)
    {
        var client = _factory.CreateClient("Api");
        if (!string.IsNullOrWhiteSpace(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private static IActionResult RedirectToLogin(PageModel page, string returnUrl)
        => page.RedirectToPage(AppRoutes.LoginPage, new { returnUrl });

    private static IActionResult RedirectToIndex(PageModel page)
        => page.RedirectToPage(AppRoutes.IndexPage);

    private static void AddApiError(PageModel page, HttpStatusCode status, string? body, string? prefix = null)
    {
        var msg = prefix ?? "API error";
        page.ModelState.AddModelError(string.Empty, $"{msg}: {(int)status} {status}. {body}");
    }

    private static IActionResult? HandleStatus(ApiCallContext ctx, HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.Unauthorized =>
                RedirectToLogin(ctx.Page, ctx.ReturnUrl),

            HttpStatusCode.Forbidden =>
                ctx.StayOnForbidden ? AddForbiddenAndStay(ctx) : RedirectToIndex(ctx.Page),

            HttpStatusCode.NotFound =>
                ctx.RedirectNotFoundToIndex ? RedirectToIndex(ctx.Page) : null,

            _ => null
        };

        static IActionResult? AddForbiddenAndStay(ApiCallContext ctx)
        {
            ctx.Page.ModelState.AddModelError(
                string.Empty,
                ctx.ForbiddenMessage ?? "You don't have permissions to do this action.");

            return null; 
        }
    }

    public async Task<ApiResult<T>> SendAsync<T>(ApiCallContext ctx, string? token, HttpRequestMessage req)
    {
        if (token is null)
            return new ApiResult<T>(default, RedirectToLogin(ctx.Page, ctx.ReturnUrl));

        var client = Create(token);
        using var resp = await client.SendAsync(req);

        var routed = HandleStatus(ctx, resp.StatusCode);
        if (routed is not null)
            return new ApiResult<T>(default, routed);

        if (ctx.StayOnForbidden && resp.StatusCode == HttpStatusCode.Forbidden)
            return new ApiResult<T>(default, null);

        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync();
            AddApiError(ctx.Page, resp.StatusCode, body);
            return new ApiResult<T>(default, null);
        }

        var data = await resp.Content.ReadFromJsonAsync<T>();
        return new ApiResult<T>(data, null);
    }

    public async Task<IActionResult?> SendAsync(
        ApiCallContext ctx,
        string? token,
        HttpRequestMessage req,
        string? successRedirectPage = null)
    {
        if (token is null)
            return RedirectToLogin(ctx.Page, ctx.ReturnUrl);

        var client = Create(token);
        using var resp = await client.SendAsync(req);

        var routed = HandleStatus(ctx, resp.StatusCode);

        if (ctx.StayOnForbidden && resp.StatusCode == HttpStatusCode.Forbidden)
            return null;

        if (routed is not null)
            return routed;

        if (resp.IsSuccessStatusCode)
            return ctx.Page.RedirectToPage(successRedirectPage ?? AppRoutes.IndexPage);

        var body = await resp.Content.ReadAsStringAsync();
        AddApiError(ctx.Page, resp.StatusCode, body);
        return null;
    }
}
