using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

using Take.Api.TaskHard.Facades.Extensions;
using Take.Api.TaskHard.Middleware;
using Take.Api.TaskHard.Models;
using Take.Api.TaskHard.Models.UI;

using HealthChecks.UI.Client;

using Lime.Protocol.Serialization.Newtonsoft;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Take.Api.Health.Eir.Extensions;
using Take.Api.Security.Heimdall.Extensions;

using Prometheus;

//Using JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.AspNetCore;
using FluentValidation;
using Take.Api.TaskHard.Models.Models;
using Take.Api.TaskHard.Models.Validations;

namespace Take.Api.TaskHard
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string SWAGGER_FILE_PATH = "./swagger/v1/swagger.json";
        private const string API_VERSION = "v1";
        private const string SETTINGS_SECTION = "Settings";
        private const string HEALTH_CHECK_ENDPOINT = "/health";
        private const string BLIP_CSS = "blip.css";
        private const string API_CHECK_KEY = "API";
        private const string POLICY = "*";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds BLiP's Json Serializer to use on BLiP's Builder
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                foreach (var settingsConverter in JsonNetSerializer.Settings.Converters)
                {
                    options.SerializerSettings.Converters.Add(settingsConverter);
                }
            });

            // Allows Blip's plugins and extensions to communicate with the Back-End APIs
            services.AddCors(options =>
            {
                options.AddPolicy(POLICY,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSingletons(Configuration);

            var settings = Configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();
            Configuration.GetConnectionString("urlGoogleApiToken");
            services.UseBotAuthentication(settings.BlipBotSettings.Authorization);

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                 });
            });

            AddSwagger(services);

            services.AddControllers().AddFluentValidation(x => x
                .RegisterValidatorsFromAssemblyContaining<Startup>());

            //Add Auth JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //Validation
                    options.Authority = "https://securetoken.google.com/netcorefirebaseatt";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Param Validations
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/netcorefirebaseatt",
                        ValidateAudience = true,
                        ValidAudience = "netcorefirebaseatt",
                        ValidateLifetime = true,
                    };
                });

            services.AddApiHealthCheck();

            AddHealthCheckUI(services, settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware(typeof(ErrorMiddleware));
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Swagger
            app.UseSwagger()
               .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint(SWAGGER_FILE_PATH, Constants.PROJECT_NAME + API_VERSION);
                });

            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            app.UseHttpsRedirection()
               .UseAuthentication()
               .UseRouting()
               .UseHttpMetrics()
               .UseCors(POLICY)
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapMetrics();
                    MapHealthCheck(endpoints);
                })
               .UseJsonResponseHealthChecks();
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(API_VERSION, new OpenApiInfo { Title = Constants.PROJECT_NAME, Version = API_VERSION });
                var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + Constants.XML_EXTENSION;
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private static void AddHealthCheckUI(IServiceCollection services, ApiSettings apiSettings)
        {
            services.AddHealthChecksUI(setupSettings: settings =>
            {
                settings.AddHealthCheckEndpoint(API_CHECK_KEY, apiSettings.HealthChecksUiUrl + HEALTH_CHECK_ENDPOINT);
            });
        }

        private static void MapHealthCheck(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecksUI(settings =>
            {
                settings.AddCustomStylesheet(BLIP_CSS);
            });

            endpoints.MapHealthChecks(HEALTH_CHECK_ENDPOINT, new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        }
    }
}
