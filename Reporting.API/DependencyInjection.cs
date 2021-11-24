using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Reporting.API.Services;
using Reporting.BBL.ApiInterfaces;
using Reporting.BBL.Infrastructure.Mappings;
using Reporting.BBL.Interfaces;
using Reporting.BBL.Services;
using Reporting.Common.Constants;
using Reporting.DAL.EF;
using Reporting.DAL.Repositories;
using Reporting.Domain.Interfaces;
using RestEase.HttpClientFactory;

namespace Reporting.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration[AppConstants.SyncfusionLicenseKey]);

            services.AddDbContext<ReportingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(AppConstants.ReportingDb),
                    b => b.MigrationsAssembly(typeof(ReportingDbContext).Assembly.FullName)));

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(configuration[AppConstants.CorsOrigin])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddMemoryCache();

            var key = Encoding.ASCII.GetBytes(configuration[AppConstants.Secret]);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };

                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddLogging(loggingBuilder =>
            {
                var loggingSection = configuration.GetSection("Logging");
                loggingBuilder.AddFile(loggingSection);
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRestEaseClient<IIeeeXploreApi>(configuration[AppConstants.IeeeXploreApiUrl], c =>
                c.JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy(),
                    },
                });

            services.AddRestEaseClient<IScientificJournalsApi>(configuration[AppConstants.ScientificJournalsApiUrl]);

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IConferencesService, ConferencesService>();
            services.AddTransient<IPublicationsService, PublicationsService>();
            services.AddTransient<IStudentsWorkService, StudentsWorkService>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddTransient<IHtmlParserService, HtmlParserService>();
            services.AddTransient<IReportsService, ReportsService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IPublicationsRepository, PublicationsRepository>();
            services.AddTransient<IStudentsWorkRepository, StudentsWorkRepository>();

            return services;
        }
    }
}
