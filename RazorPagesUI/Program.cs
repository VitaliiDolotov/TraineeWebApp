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

var builder = WebApplication.CreateBuilder(args);

#region API configuration

builder.Services.AddControllers();

builder.Services.AddHttpClient("Api", (sp, client) =>
{
	var http = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
	client.BaseAddress = new Uri($"{http.Request.Scheme}://{http.Request.Host}");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Validation configuration
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IModelsMarker>();

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

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
	var path = context.Request.Path.Value?.ToLower();

	if (path is not null &&
	    (path.StartsWith("/login")
	     || path.StartsWith("/logout")
	     || path.StartsWith("/api")
	     || path.StartsWith("/swagger")
	     || path.StartsWith("/css")
	     || path.StartsWith("/js")
	     || path.StartsWith("/lib")
	     || path.StartsWith("/images")))
	{
		await next();
		return;
	}

	if (!context.Request.Cookies.ContainsKey("access_token"))
	{
		context.Response.Redirect($"/Login?returnUrl={Uri.EscapeDataString(context.Request.Path)}");
		return;
	}

	await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
