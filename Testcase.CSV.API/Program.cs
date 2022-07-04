using Microsoft.OpenApi.Models;
using Testcase.Appointments.API.Helper;
using Testcase.CSV.API.Helper;
using Testcase.CSV.Application;
using TestCase.ICsvInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplications(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Configuration);
#region Swagger Dependencies

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestCase.CSV",
        Version = "v1"
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSV API ");
    });
}
app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
