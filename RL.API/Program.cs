global using RL.Entity.DTOs;
global using RL.Utility;
global using RL.Utility.IServices;
global using RL.Utility.JWT;
global using RL.Services;
using RL.DataAccess;
using Microsoft.EntityFrameworkCore;
using RL.Utility.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Context

builder.Services.AddDbContext<dbContextRoyalLibrary>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));


//Services
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IBookServices, BooksServices>();
builder.Services.AddScoped<IUserServices, UserServices>();

//JWT
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IJwtOptions, JwtOptions>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials()
           ); 

app.Run();
