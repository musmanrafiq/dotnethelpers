using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirebaseNotifications.Interfaces
{
    public interface IFirebaseNotificationService
    {
        Task SendNotification(List<string> deviceIds, string message, string title, dynamic notificationData = null, string topic = "");
        Task SendNotification(string serverKey, string messageTitle, string message, List<string> deviceIds = null, string topic = "");
    }
}
