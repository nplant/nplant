using System;

namespace NPlant.UI
{
    public class UserNotificationEvent
    {
        public UserNotificationEvent(string message, UserNotificationType type = UserNotificationType.Info)
        {
            this.NotificationType = type;
            this.Message = message;
        }

        public UserNotificationEvent(string message, Exception exception)
        {
            this.Message = "{0} - {1}".FormatWith(message, exception.Message);
            this.NotificationType = UserNotificationType.Error;
        }

        public UserNotificationType NotificationType { get; private set; }
        public string Message { get; set; }
    }

    public enum UserNotificationType
    {
        Info,
        Warning,
        Error
    }
}