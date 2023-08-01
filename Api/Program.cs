using System.Text;
using Api.Data;
using Api.Extensions;
using Api.Interfaces;
using Api.MiddleWare;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//to add context  and make it use sqllite

builder.Services.AddIdentityServices(builder.Configuration);
//////
var app = builder.Build();
//to reduce the amount of information in error that return
// if(builder.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }
//handle error throw ExceptionMiddleWare class in any request send if there is error then call ExceptionMiddleWare class 
app.UseMiddleware<ExceptionMiddleWare>();
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//to read seed data

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
 var context = services.GetRequiredService<DataContext>();
 var logger = services.GetService<ILogger<Program>>();
 
try
{
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{

    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
