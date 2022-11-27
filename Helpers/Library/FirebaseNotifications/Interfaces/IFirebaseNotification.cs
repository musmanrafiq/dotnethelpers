using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirebaseNotifications.Interfaces
{
    // Firebase notificatio service interface
    public interface IFirebaseNotificationService
    {
        // send notifications to provided device ids, by assuming serverKey is provided
        // through config
        Task SendNotification(List<string> deviceIds, string message, string title, 
            dynamic notificationData = null, string topic = "");
        // send notifications to provided device ids using specific server key
        Task SendNotification(string serverKey, string messageTitle, string message, 
            List<string> deviceIds = null, string topic = "");
    }
}
