using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RazorPageDemo.BL.Mapper;
using RazorPageDemo.BL.Services;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;
using RazorPagesUI.Api.Clients;
using RazorPagesUI.Api.Controllers.Auth;
using RazorPagesUI.Infrastructure.Auth;
using RazorPagesUI.Infrastructure.Routing;

var builder = WebApplication.CreateBuilder(args);

#region API configuration

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// HttpClient for internal API calls
builder.Services.AddHttpClient("Api", (sp, client) =>
{
    var http = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
    client.BaseAddress = new Uri($"{http.Request.Scheme}://{http.Request.Host}");
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Input: Bearer <token>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trainees website API", Version = "v1" });
});

// Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IModelsMarker>();

// API auth (JWT)
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtTokenFactory.Issuer,
            ValidAudience = JwtTokenFactory.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenFactory.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IMapperService, MapperService>();
builder.Services.AddSingleton<IDataRepository, MockRepository>();

builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<UserApi>();
builder.Services.AddScoped<AddressApi>();
builder.Services.AddScoped<AuthApi>();

#endregion

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation"); });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    if (path.Equals(AppRoutes.LoginPage, StringComparison.OrdinalIgnoreCase) ||
        path.Equals(AppRoutes.LogoutPage, StringComparison.OrdinalIgnoreCase) || path.StartsWithSegments("/api") ||
        path.StartsWithSegments("/swagger"))
    {
        await next();
        return;
    }

    if (!context.Request.Cookies.ContainsKey(AuthConstants.AccessTokenCookie))
    {
        var returnUrl = context.Request.Path + context.Request.QueryString;
        context.Response.Redirect($"{AppRoutes.LoginPage}?returnUrl={Uri.EscapeDataString(returnUrl)}");
        return;
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();