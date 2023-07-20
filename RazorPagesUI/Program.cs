using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using RazorPageDemo.BL.Mapper;
using RazorPageDemo.BL.Services;
using RazorPageDemo.Services;
using RazorPagesDemo.Models;

var builder = WebApplication.CreateBuilder(args);

#region API configuration

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trainees website API", Version = "v1" });
});

// Validation configuration
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IModelsMarker>();

builder.Services.AddScoped<IMapperService, MapperService>();
builder.Services.AddSingleton<IDataRepository, MockRepository>();

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
