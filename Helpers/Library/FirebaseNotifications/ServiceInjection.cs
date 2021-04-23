using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FirebaseNotifications
{
    public static class ServiceInjection
    {
        public static void RegisterFirebaseMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseOptions>((x) => configuration.GetSection("FirebaseMessaging"));
            services.AddSingleton<IFirebaseNotificationService, FirebaseNotificationService>();
        }
    }
}
