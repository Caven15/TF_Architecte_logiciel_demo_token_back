using Demo.BLL.Services;
using demoToken.API.Infrastructure;
using DemoToken.BLL.Interfaces;
using demoToken.DAL.Interfaces;
using demoToken.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tools;

public class Program
{
    public static void Main(string[] args)
    {
        // �tape 1: Cr�ation du builder pour configurer l'application ASP.NET Core
        var builder = WebApplication.CreateBuilder(args);

        // �tape 2: Configuration de l'application � partir du fichier appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // �tape 3: Ajout des services n�cessaires � l'application
        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        }));

        builder.Services.AddControllers();

        // �tape 4: Configuration de Swagger pour la documentation de l'API
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoToken", Version = "v1" });
            // Configuration du sch�ma de s�curit� Bearer pour Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                              "Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // Configuration des exigences de s�curit� pour Swagger
            OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            };
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                [openApiSecurityScheme] = new List<string>()
            });
        });

        // �tape 5: Configuration de l'authentification et de l'autorisation avec JWT
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            // R�cup�ration de l'instance unique de TokenManager
            var tokenManager = builder.Services.BuildServiceProvider().GetService<TokenManager>();

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManager._secret)),
                ValidateIssuer = true,
                ValidIssuer = tokenManager._issuer, // Utilisation de l'Issuer du TokenManager
                ValidateAudience = true,
                ValidAudience = tokenManager._audience, // Utilisation de l'Audience du TokenManager
            };
        });

        // �tape 6: Configuration des singletons (instances uniques pour toute l'application)
        builder.Services.AddSingleton(sp => new Connection(builder.Configuration.GetConnectionString("default")));
        builder.Services.AddSingleton<TokenManager>();

        // �tape 7: Configuration des repositories (instances cr��es pour chaque requ�te)
        builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

        // �tape 8: Configuration des services (instances cr��es pour chaque requ�te)
        builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

        // �tape 9: Construction de l'application
        var app = builder.Build();

        // �tape 10: Configuration de l'application
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Make3D.API v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("MyPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        // �tape 11: Configuration des routes pour les contr�leurs
        app.MapControllers();

        // �tape 12: Ex�cution de l'application
        app.Run();
    }
}
