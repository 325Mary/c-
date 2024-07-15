using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Producto.Services;
using System;
using System.Management; // Necesario para consultas de gestión en Windows
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UserJwtAuthApp.Data;
using UserJwtAuthApp.Services;
using Producto.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("GestionProductos"));
builder.Services.AddDbContext<ProductoContext>(options => options.UseInMemoryDatabase("GestionProductos"));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
// builder.Services.AddSingleton(new prodc)

// Generación de clave JWT
byte[] keyBytes = new byte[32]; // 256 bits / 8 = 32 bytes
RandomNumberGenerator.Fill(keyBytes);

string base64Key = Convert.ToBase64String(keyBytes);
builder.Configuration["Jwt:Key"] = base64Key;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
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

var timer = new Timer(state =>
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        ObjectQuery query = new ObjectQuery("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        ulong memorySizeGB = 0;

        try
        {
            foreach (ManagementObject queryObj in searcher.Get())
            {
                ulong memorySizeKB = Convert.ToUInt64(queryObj["TotalVisibleMemorySize"]);
                memorySizeGB = memorySizeKB / 1024 / 1024; // Convertir KB a GB
            }

            Console.WriteLine($"RAM total ejecutable: {memorySizeGB} GB");
        }
        catch (ManagementException e)
        {
            Console.WriteLine("Error al obtener la información de RAM: " + e.Message);
        }
    }
    else
    {
        Console.WriteLine("La consulta de información del sistema solo es compatible con Windows.");
    }
}, 
null, TimeSpan.Zero, TimeSpan.FromSeconds(100)
); 
app.Run();
