using FirebaseNotifications.Common;
using FirebaseNotifications.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseNotifications.Services
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        readonly FirebaseOptions _firebaseOptions;
        public FirebaseNotificationService(IOptions<FirebaseOptions> options)
        {
            _firebaseOptions = options.Value;
        }

        public async Task SendNotification(List<string> deviceIds, string messageTitle, string message, dynamic notificationData = null, string topic = "")
        {
            object preparedData = deviceIds != null ? PrepareFcmDataForDeviceIds(deviceIds, messageTitle, message) : (!string.IsNullOrEmpty(topic) ? PrepareFcmDataForTopic(topic, messageTitle, message) : null);
            if (preparedData != null)
                await SendFcmWebRequest(preparedData).ConfigureAwait(false);
        }

        public async Task SendNotification(string serverKey, string messageTitle, string message, List<string> deviceIds = null, string topic = "")
        {
            object preparedData = deviceIds != null ? PrepareFcmDataForDeviceIds(deviceIds, messageTitle, message) : (!string.IsNullOrEmpty(topic) ? PrepareFcmDataForTopic(topic, messageTitle, message) : null);
            if (preparedData != null)
                await SendFcmWebRequest(serverKey, preparedData).ConfigureAwait(false);
        }

        private async Task SendFcmWebRequest(object data)
        {
            ValidateServiceUrl();
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

        private async Task SendFcmWebRequest(string serverApiKey, object data)
        {
            ValidateServiceUrl();
            var byteArray = GetByteArray(data);
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
        private object PrepareFcmDataForDeviceIds(List<string> deviceIds, string messageTitle, string message)
        {

            object data = new
            {
                registration_ids = deviceIds,
                notification = new
                {
                    title = messageTitle,
                    body = message,
                    sound = "default"
                },
                data = new
                {
                    data = string.Empty,
                    isMessage = false
                }
            };

            return data;
        }
        private object PrepareFcmDataForTopic(string topic, string messageTitle, string message)
        {

            object data = new
            {
                to = "/topics/" + topic,
                notification = new
                {
                    title = messageTitle,
                    body = message,
                    sound = "default"
                },
                data = new
                {
                    data = string.Empty,
                    isMessage = false
                }
            };

            return data;
        }
        private byte[] GetByteArray(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(json);
            return byteArray;
        }

        private void ValidateServiceUrl()
        {
            if (string.IsNullOrEmpty(_firebaseOptions.Url))
            {
                _firebaseOptions.Url = Constants.ServiceUrl;
            }
        }
    }
}
