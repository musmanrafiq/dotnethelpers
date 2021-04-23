using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseNotifications
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        readonly FirebaseOptions _firebaseOptions;
        public FirebaseNotificationService(IOptions<FirebaseOptions> options)
        {
            _firebaseOptions = options.Value;
        }
        public async Task SendNotification(List<string> deviceIds, string message, string title, dynamic notificationData = null)
        {
            await SendFcmWebRequest(PrepareFcmData(deviceIds, message, title, notificationData));
        }
        private async Task SendFcmWebRequest(object data)
        {
            var byteArray = GetByteArray(data);
            var serverApiKey = _firebaseOptions.ServerKey;
            var tRequest = WebRequest.Create(_firebaseOptions.Url);
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));
            tRequest.ContentLength = byteArray.Length;
            tRequest.UseDefaultCredentials = true;
            tRequest.PreAuthenticate = true;
            tRequest.Credentials = CredentialCache.DefaultCredentials;
            using (var dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                await tRequest.GetResponseAsync();
            }
        }
        private object PrepareFcmData(List<string> deviceIds, string message, string title, dynamic notificationData = null)
        {
            dynamic dynamicResponse;
            if (notificationData != null)
            {
                dynamicResponse = new
                {
                    data = notificationData
                };
            }
            else
            {
                dynamicResponse = new
                {
                    data = string.Empty
                };
            }
            object data = new
            {
                registration_ids = deviceIds,
                notification = new
                {
                    title,
                    body = message,
                    sound = "default"
                },
                data = dynamicResponse
            };
            return data;
        }
        private byte[] GetByteArray(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(json);
            return byteArray;
        }
    }
}
