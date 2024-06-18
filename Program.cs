using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;
using UserJwtAuthApp.Data;
using UserJwtAuthApp.Services;
using System.Runtime.InteropServices; 
using System.Diagnostics; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("UserDb"));
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<SystemInfoService>(); // Registrar el servicio aquí

byte[] keyBytes = new byte[32]; // 256 bits / 8 = 32 bytes
RandomNumberGenerator.Fill(keyBytes); // Uso del método estático

string base64Key = Convert.ToBase64String(keyBytes);
Console.WriteLine("Clave secreta generada: " + base64Key);

builder.Configuration["Jwt:Key"] = base64Key;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // No validar el emisor
        ValidateAudience = false, // No validar la audiencia
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes) // Utiliza la clave generada
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
