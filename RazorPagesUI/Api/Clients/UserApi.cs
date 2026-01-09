using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models.DTO;

namespace RazorPagesUI.Api.Clients;

public sealed class UserApi
{
    private const string UserController = "api/User";

    private readonly ApiClient _api;
    public UserApi(ApiClient api) => _api = api;

    public Task<ApiResult<IEnumerable<UserDtoResponse>>> GetAllAsync(PageModel page, string? token, string returnUrl)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: false);
        var req = new HttpRequestMessage(HttpMethod.Get, UserController);
        return _api.SendAsync<IEnumerable<UserDtoResponse>>(ctx, token, req);
    }

    public Task<ApiResult<UserDtoResponse>> GetByIdAsync(PageModel page, string? token, string returnUrl, string id)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: false);
        var req = new HttpRequestMessage(HttpMethod.Get, $"{UserController}/{id}");
        return _api.SendAsync<UserDtoResponse>(ctx, token, req);
    }

    public Task<IActionResult?> CreateAsync(PageModel page, string? token, string returnUrl, UserDtoRequest dto)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: true);
        var req = new HttpRequestMessage(HttpMethod.Post, UserController) { Content = JsonContent.Create(dto) };
        return _api.SendAsync(ctx, token, req);
    }

    public Task<IActionResult?> UpdateAsync(PageModel page, string? token, string returnUrl, string id, UserDtoRequest dto)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: true);
        var req = new HttpRequestMessage(HttpMethod.Put, $"{UserController}/{id}") { Content = JsonContent.Create(dto) };
        return _api.SendAsync(ctx, token, req);
    }

    public Task<IActionResult?> DeleteAsync(PageModel page, string? token, string returnUrl, string id)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: true, ForbiddenMessage: "Only Admin can delete items.");
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{UserController}/{id}");
        return _api.SendAsync(ctx, token, req);
    }
}
