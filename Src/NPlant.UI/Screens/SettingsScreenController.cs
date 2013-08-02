namespace NPlant.UI.Screens
{
    public class SettingsScreenController
    {
        private readonly ISettingScreen _screen;

        public SettingsScreenController(ISettingScreen screen)
        {
            _screen = screen;
        }

        public void Start()
        {
            SystemSettings settings = SystemEnvironment.GetSettings();
            _screen.JavaPath = settings.JavaPath;
        }

        public void SaveChanges()
        {
            SystemEnvironment.SetSettings(new SystemSettings {JavaPath = _screen.JavaPath});
        }
    }
}