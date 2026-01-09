using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;

namespace RazorPagesUI.Api.Clients;

public sealed class AddressApi
{
    private const string BasePath = "api/Address";

    private readonly ApiClient _api;
    public AddressApi(ApiClient api) => _api = api;

    public Task<ApiResult<IEnumerable<Address>>> GetAllAsync(PageModel page, string? token, string returnUrl)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: false);
        var req = new HttpRequestMessage(HttpMethod.Get, BasePath);

        return _api.SendAsync<IEnumerable<Address>>(ctx, token, req);
    }

    public Task<ApiResult<Address>> GetByIdAsync(PageModel page, string? token, string returnUrl, string id)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: false);
        var req = new HttpRequestMessage(HttpMethod.Get, $"{BasePath}/{id}");

        return _api.SendAsync<Address>(ctx, token, req);
    }

    public Task<IActionResult?> CreateAsync(PageModel page, string? token, string returnUrl, Address address)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: true);
        var req = new HttpRequestMessage(HttpMethod.Post, BasePath)
        {
            Content = JsonContent.Create(address)
        };

        return _api.SendAsync(ctx, token, req);
    }

    public Task<IActionResult?> DeleteAsync(PageModel page, string? token, string returnUrl, string id)
    {
        var ctx = new ApiCallContext(page, returnUrl, StayOnForbidden: true,
            ForbiddenMessage: "Only Admin can delete items.");

        var req = new HttpRequestMessage(HttpMethod.Delete, $"{BasePath}/{id}");

        return _api.SendAsync(ctx, token, req);
    }
}