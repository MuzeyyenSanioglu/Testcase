using Microsoft.OpenApi.Models;
using System.Reflection;
using Testcase.API.Helpers;
using Testcase.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo { Title = "UserAPI", Version = "v1" });
});

builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Configuration);
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(url: String.Format(builder.Configuration.GetSection("Swagger:UseSwaggerUI:SwaggerEndpoint").Value, builder.Configuration.GetSection("Swagger:SwaggerName").Value),
        name: "Version CoreSwaggerWebAPI-1");

});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}
app.MapControllers();

app.Run();
