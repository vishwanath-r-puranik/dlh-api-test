using System;
using System.IO;
using System.Reflection;
using DLHApi.DAL.Data;
using DLHApi.DAL.EISHandler.Authentication;
using DLHApi.DAL.Repo;
using DLHApi.DAL.Services;
using DLHApi.DTO.V1.Mapper;
using DLHApi.EIS.Authentication;
using DLHApi.EIS.Services.PDFMerge;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Org.OpenAPITools.Filters;
using Org.OpenAPITools.OpenApi;

namespace DLHApi.OpenApiSpec
{
    public static class ServiceExtensions
	{
        public static void RegisterServices(this IServiceCollection collection)
        {
            RegisterDALServices(collection);
            RegisterDTOServices(collection);
            RegisterEISServices(collection);
        }

        public static void RegisterDALServices(this IServiceCollection collection)
        {
            collection.AddTransient<IDlhService, DlhService>();
            collection.AddTransient<IDlhRepo, DlhRepo>();
            collection.AddTransient<IAuditService, AuditService>();
            collection.AddTransient<IAuditRepo, AuditRepo>();   
        }

        public static void RegisterDTOServices(this IServiceCollection collection)
        {
            collection.AddTransient<DlhistoryModelMapper, DlhistoryModelMapper>();
        }

        public static void RegisterEISServices(this IServiceCollection collection)
        {
            collection.AddTransient<ITokenHandler, EIS.Authentication.TokenHandler>();
            collection.AddTransient<IPdfMergeService, PdfMergeService>();
            collection.AddTransient<GenerateToken, GenerateToken>();
        }

        public static void DbSetupServices(this IServiceCollection services)
        {
            var dlhDbServer = Environment.GetEnvironmentVariable("DlhDBServer");
            var dlhDbName = Environment.GetEnvironmentVariable("DlhDBName");
            var dlhDbUserId = Environment.GetEnvironmentVariable("DlhDbUserId");
            var dlhDbPassword = Environment.GetEnvironmentVariable("DlhDbPassword");

            services.AddHttpClient<PdfMergeService>();

            services.AddDbContext<DlhdevDbContext>(options => options.UseSqlServer($"Server={dlhDbServer};Database={dlhDbName};User Id={dlhDbUserId};Password={dlhDbPassword};TrustServerCertificate=True"));

            var auditDbServer = Environment.GetEnvironmentVariable("AuditDBServer");
            var auditDbName = Environment.GetEnvironmentVariable("AuditDBName");
            var auditDbUserId = Environment.GetEnvironmentVariable("AuditDbUserId");
            var auditDbPassword = Environment.GetEnvironmentVariable("AuditDbPassword");

            services.AddDbContext<DlhdevAuditContext>(options => options.UseSqlServer($"Server ={auditDbServer}; Database ={auditDbName}; User Id = {auditDbUserId}; Password ={auditDbPassword}; TrustServerCertificate = True"));

        }

        public static void EnableSwaggerServices(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                    c.SwaggerDoc("0.0.1", new OpenApiInfo
                    {
                        Title = "IBM MOVES DLH API",
                        Description = "IBM MOVES DLH API (ASP.NET Core 6.0)",
                        TermsOfService = new Uri("https://www.ibm.com/ca-en"),
                        Contact = new OpenApiContact
                        {
                            Name = "IBM API Support Team",
                            Url = new Uri("https://www.ibm.com/ca-en"),
                            Email = "support@ibm.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "NoLicense",
                            Url = new Uri("https://www.ibm.com/ca-en")
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
                        {securityScheme, new string[] {} }
                    });

                });
            services
                .AddSwaggerGenNewtonsoftSupport();
        }

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

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: Environment.GetEnvironmentVariable("CorsPolicy"),
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

    }
}

