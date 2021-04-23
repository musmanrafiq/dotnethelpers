using FirebaseNotifications.Common;
using FirebaseNotifications.Interfaces;
using FirebaseNotifications.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FirebaseNotifications.DI
{
    public static class ServiceInjection
    {
        public static void RegisterFirebaseMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseOptions>(configuration.GetSection("FirebaseMessaging"));
            services.AddSingleton<IFirebaseNotificationService, FirebaseNotificationService>();
        }
    }
}
