using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirebaseNotifications
{
    public interface IFirebaseNotificationService
    {
        Task SendNotification(List<string> deviceIds, string message, string title, dynamic notificationData = null);
    }
}
