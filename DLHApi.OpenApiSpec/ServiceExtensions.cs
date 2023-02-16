using System;
using System.IO;
using System.Reflection;
using DLHApi.Common.Logger;
using DLHApi.Common.Logger.Contracts;
using DLHApi.DAL.Data;
using DLHApi.DAL.EISHandler.Authentication;
using DLHApi.DAL.Repo;
using DLHApi.DAL.Services;
using DLHApi.DTO.V1.Mapper;
using DLHApi.EIS.Authentication;
using DLHApi.EIS.Services.PDFMerge;
using DLHApi.Shared;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Org.OpenAPITools.Filters;
using Org.OpenAPITools.OpenApi;

namespace DLHApi.OpenApiSpec
{
    /// <summary>
    /// Registering various services and setup files here.
    /// </summary>
    public static class ServiceExtensions
	{
        /// <summary>
        /// Added services configure calls for varous layers
        /// </summary>
        public static void RegisterServices(this IServiceCollection collection)
        {
            RegisterDALServices(collection);
            RegisterDTOServices(collection);
            RegisterEISServices(collection);
        }

        /// <summary>
        /// For DAL layer services
        /// </summary>
        public static void RegisterDALServices(this IServiceCollection collection)
        {
            collection.AddTransient<IDlhService, DlhService>();
            collection.AddTransient<IDlhRepo, DlhRepo>();
            collection.AddTransient<IAuditService, AuditService>();
            collection.AddTransient<IAuditRepo, AuditRepo>();   
        }

        /// <summary>
        /// For DTO layer services
        /// </summary>
        public static void RegisterDTOServices(this IServiceCollection collection)
        {
            collection.AddTransient<DlhistoryModelMapper, DlhistoryModelMapper>();
        }

        /// <summary>
        /// Foor EIS layer services
        /// </summary>
        public static void RegisterEISServices(this IServiceCollection collection)
        {
            collection.AddTransient<ITokenHandler, EIS.Authentication.TokenHandler>();
            collection.AddTransient<IPdfMergeService, PdfMergeService>();
            collection.AddTransient<GenerateToken, GenerateToken>();
        }

        /// <summary>
        /// Databse setup services
        /// </summary>
        public static void DbSetupServices(this IServiceCollection services)
        {
            var dlhDbServer = Environment.GetEnvironmentVariable("DlhDBServer");
            var dlhDbName = Environment.GetEnvironmentVariable("DlhDBName");
            var dlhDbUserId = Environment.GetEnvironmentVariable("DlhDbUserId");
            var dlhDbPassword = Environment.GetEnvironmentVariable("DlhDbPassword");

            services.AddHttpClient<PdfMergeService>();
            services.AddHttpClient<AuditRepo>();

            services.AddDbContext<DlhdevDbContext>(options => options.UseSqlServer($"Server={dlhDbServer};Database={dlhDbName};User Id={dlhDbUserId};Password={dlhDbPassword};TrustServerCertificate=True"));

        }

        /// <summary>
        /// Swagger setup service
        /// </summary>
        public static void EnableSwaggerServices(this IServiceCollection services)
        {
            var ibmUri = "https://www.ibm.com/ca-en";
            services
                .AddSwaggerGen(c =>
                {
                    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                    c.SwaggerDoc("0.0.1", new OpenApiInfo
                    {
                        Title = "IBM MOVES DLH API",
                        Description = "IBM MOVES DLH API (ASP.NET Core 6.0)",
                        TermsOfService = new Uri(ibmUri),
                        Contact = new OpenApiContact
                        {
                            Name = "IBM API Support Team",
                            Url = new Uri(ibmUri),
                            Email = "support@ibm.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "NoLicense",
                            Url = new Uri(ibmUri)
                        },
                        Version = "0.0.1",
                    });
                    c.CustomSchemaIds(type => type.FriendlyId(true));
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetEntryAssembly().GetName().Name}.xml");
                    // Sets the basePath property in the OpenAPI document generated
                    c.DocumentFilter<BasePathFilter>("/v1");

                    // Include DataAnnotation attributes on Controller Action parameters as OpenAPI validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();

                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "JWT Authentication",
                        Description = "Enter a valid JWT bearer token",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {securityScheme, Array.Empty<string>() }
                    });

                });
            services
                .AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// Authentication setup
        /// </summary>
        public static void ConfigureAuthenticationService(this IServiceCollection services)
        {
            var authenticationOptions = new KeycloakAuthenticationOptions
            {
                AuthServerUrl = "https://keycloak-keycloak.apps.pesdev.hcscloud.net/",
                Realm = "pesrealm",
                Resource = "pesclient",
                //SslRequired = "none",
                VerifyTokenAudience = false,
            };

            services.AddKeycloakAuthentication(authenticationOptions);
        }

        /// <summary>
        /// Cors set up
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: Environment.GetEnvironmentVariable("CorsPolicy"),
                                  policy =>
                                  {
                                      //* foor now, later we can give a fixed list of urls tobe allowed
                                      policy.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

        /// <summary>
        /// Extention method for loggers.
        /// </summary>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}

