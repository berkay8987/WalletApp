using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Wallet.Web.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddHttpClient("WalletAPI", client =>
            {
                client.BaseAddress = new Uri("https://wallet.api:8081/");
            }).ConfigurePrimaryHttpMessageHandler(() =>
                new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            return services;
        }
    }
}
