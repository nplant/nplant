namespace NPlant.UI
{
    public class UserNotificationEvent
    {
        public UserNotificationEvent(string message, UserNotificationType type = UserNotificationType.Info)
        {
            this.NotificationType = type;
            this.Message = message;
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