# Dot Net Helpers

[![NuGet Package](https://img.shields.io/nuget/v/UHelpers.svg)](https://www.nuget.org/packages/UHelpers)

# FirebaseNotifications

[![NuGet Package](https://img.shields.io/nuget/v/FirebaseNotifications.svg)](https://www.nuget.org/packages/FirebaseNotifications)

To use firebase notifications package there are two scenarios : 

1 - We have only one server key to use for sending notifications 

In that case add one property i.e. ServerKey in your {config}.json file in your project like 

```csharp
"FirebaseMessaging" : 
{ "ServerKey" : "YOUR SERVER KEY HERE" 
} 
```
And next step is to add RegisterFirebaseMessaging(configuration); in your startup.cs file. 
Third and last step is to inject IFirebaseNotificationService in your class where you want to use it i.e. 

```csharp
private readonly IFirebaseNotificationService firebaseMessagingService; 
public MyClass(IFirebaseNotificationService firebaseMessagingService){ this.firebaseMessagingService = firebaseMessagingService; } 
```

// use firebaseMessagingService to call methods where required 


```csharp
// if you have device ids and only have one serverkey 
await firebaseMessagingService.SendNotification(deviceIds, "message", "title"); 

// if you have device ids and only have one serverkey and want to send to specific topic
await firebaseMessagingService.SendNotification(deviceIds, "message", "title", "my topic");
```

2 - We have multiple server keys ( multiple projects to support ) to use for sending notifications 
In that case we don't need update {config}.json file

```csharp

just add RegisterFirebaseMessaging(configuration); in Startup.cs 

// if you are supporting multiple server keys ( firebase projects ), and have device ids 
await firebaseMessagingService.SendNotification(serverKey, "message", "title", deviceIds); 

// if you are supporting multiple server keys ( firebase projects ), and have a topic 
await firebaseMessagingService.SendNotification(serverKey, "message", "title", "topic");
```
