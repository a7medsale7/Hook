using Hook.Application;
using Hook.Application.Services.Implementation;
using Hook.Application.Services.Interfaces;
using Hook.Infrastructure;
using Hook.Infrastructure.Authentication;
using Hook.Infrastructure.Mail;
using Hook.Application.Abstractions;
using FluentValidation;
using Hangfire;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using Hook.Domain.Abstractions;
using Hook.Infrastructure.Persistence;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection.Metadata;
using System.Text;
using Hook.Domain.Abstractions.Repositories;
using Hook.Infrastructure.Repositories;
using Hook.Infrastructure.Authentication.Filters;
using Hook.Api.Middleware;
using Microsoft.OpenApi.Models;


namespace Hook.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddInfrastructureLayer(configuration)
            .AddRepositories()
            .AddApplicationServices()
            .AddPresentation()
            .AddSwaggerDocumentation()
            .AddMapsterConfiguration()
            .AddFluentValidationConfiguration()
            .AddAuthenticationConfiguration(configuration)
            .AddHangfireConfig(configuration);

        return services;
    }

    // ================================
    // Infrastructure Layer
    // ================================
    private static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.Configure<MailSetting>(configuration.GetSection(nameof(MailSetting)));
        services.AddTransient<IEmailSender, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddHttpContextAccessor();

        return services;
    }

    // ================================
    // Repositories Registration
    // ================================
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IBoatOwnerRepository, BoatOwnerRepository>();
        services.AddScoped<IBoatRepository, BoatRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<ITripDateRepository, TripDateRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        


        return services;
    }

    // ================================
    // Application Services
    // ================================
    private static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IBoatOwnerService, BoatOwnerService>();
        services.AddScoped<IBoatService, BoatService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IReviewService, ReviewService>();
        return services;
    }

    // ================================
    // Controllers + API Explorer
    // ================================
    private static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }

    // ================================
    // Swagger Configuration
    // ================================
    private static IServiceCollection AddSwaggerDocumentation(
    this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fishing Platform API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter your JWT token here."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    // ================================
    // Mapster Configuration
    // ================================
    private static IServiceCollection AddMapsterConfiguration(
        this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(typeof(Hook.Application.AssemblyReference).Assembly);
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }

    // ================================
    // FluentValidation Configuration
    // ================================
    private static IServiceCollection AddFluentValidationConfiguration(
        this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(
            typeof(Hook.Application.AssemblyReference).Assembly);

        return services;
    }

    // ================================
    // AddAuthentication Configuration
    // ================================

    private static IServiceCollection AddAuthenticationConfiguration(
     this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuotherzationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        var settings = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()!;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(settings.Key))
            };
        });

        return services;
    }

    // ================================
    // Hangfire Configuration
    // ================================
    private static IServiceCollection AddHangfireConfig(
       this IServiceCollection services,
       IConfiguration configuration)
    {

        services.AddHangfire(config => config
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"))); // أو DefaultConnection حسب إنت مسجل الداتا بيس بتاعتك فين

        return services;
    }

}
