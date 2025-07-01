using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Wallet.Web.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddHttpClient("WalletAPI", client =>
            {
                client.BaseAddress = new Uri("https://wallet.api:8081/");
            }).ConfigurePrimaryHttpMessageHandler(() =>
                new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/login";
                    options.LogoutPath = "/Account/logout";
                });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
                options.InstanceName = "WalletAppRedis";
            });

            return services;
        }
    }
}
