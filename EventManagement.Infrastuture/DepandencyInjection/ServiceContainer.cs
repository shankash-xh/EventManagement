using EventManagement.Application.Behaviour;
using EventManagement.Application.Interface;
using EventManagement.Application.Mapper;
using EventManagement.Application.Model;
using EventManagement.Application.Validation;
using EventManagement.Domain.Entity;
using EventManagement.Infrastuture.DataBase;
using EventManagement.Infrastuture.ExceptionHandler;
using EventManagement.Infrastuture.Repository;
using EventManagement.Infrastuture.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace EventManagement.Infrastuture.DepandencyInjection;

public static class ServiceContainer        
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {

        // Register repositories and unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
 .AddJwtBearer("Bearer", options =>
 {

     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = config.GetSection("Jwt:Issuer").Value!,
         ValidAudience = config.GetSection("Jwt:Audience").Value!,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value!)),
         ClockSkew = TimeSpan.Zero,
     };

 });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        // add logger it used 

        services.AddValidatorsFromAssemblyContaining<AddBookingValidation>();
        services.AddValidatorsFromAssemblyContaining<AddEventValidation>();
        services.AddValidatorsFromAssemblyContaining<UserRequestValidation>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookingValidation>();
        services.AddValidatorsFromAssemblyContaining<UpdateEventValidation>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("AppDb"));
            options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        services.AddTransient<GlobalExceptionHandler>();

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("SmartMeet")
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddHttpContextAccessor();


        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.Console()
            .CreateLogger();

        services.Configure<IdentityOptions>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequiredLength = 6;
            o.Password.RequiredUniqueChars = 1;
            o.Password.RequireUppercase = true;
        });

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        //services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

        return services;
    }

    public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        app.UseCors("AllowAllOrigins");
        return app;
    }
}
