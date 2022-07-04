using Microsoft.OpenApi.Models;
using Testcase.Appointments.API.Helper;
using Testcase.Appointments.API.Services;
using Testcase.Appointments.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppointmentAPI", Version = "v1" });
});
builder.Services.AddSwaggerConfiguration(builder.Configuration);
var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Appoinment API V1");

    });
}
app.MapControllers();

app.Run();
