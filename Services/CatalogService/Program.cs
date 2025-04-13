using System.Text;
using CatalogService.Data;
using Dtos.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.AddEndpointsApiExplorer(); // 🔥 OpenAPI metadata servisi

builder.Services.AddDbContext<Context>();


// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options =>
//     {
//         options.Authority = "http://localhost:5214"; // IdentityService URL’in
//         options.RequireHttpsMetadata = false;
//         options.Audience = "identity"; // API adı
//     });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:5214",

            ValidateAudience = true,
            ValidAudience = "identity",

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MySuperSecretKeyThatIsVeryLong123!CHBSGJHDKDLSCNLNXMŞLXİXİ")),

            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication(); // Bu önce gelmeli
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
