namespace NPlant.UI
{
    public interface IMainScreen
    {
        string Title { get; set; }
        void AddFileView(FileViewType type, string filePath);
        void DisplayUserNotification(UserNotificationEvent @event);
    }
}