using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string ENCRYPTION_KEY = "Z35KX88B9P48E6I79DCLB36CE9E4C0C1";
        public static IServiceCollection AddIWProtectedBrowserStorage(this IServiceCollection services, string? key = null)
        {
            return services.AddScoped(enc => new Encryption(GetEncryptionKey(key)))
                .AddScoped<IIWLocalStorageService, IWLocalStorageService>()
                .AddScoped<IIWSessionStorageService, IWSessionStorageService>();
        }

        public static IServiceCollection AddIWProtectedBrowserStorageAsSingleton(this IServiceCollection services, string? key = null)
        {
            return services.AddSingleton(enc => new Encryption(GetEncryptionKey(key)))
                .AddSingleton<IIWLocalStorageService, IWLocalStorageService>()
                .AddSingleton<IIWSessionStorageService, IWSessionStorageService>();
        }

        private static string GetEncryptionKey(string? key)
        {
            return string.IsNullOrWhiteSpace(key) ? ENCRYPTION_KEY : key;
        }
    }
}
