using System.Configuration;
using System.Runtime.CompilerServices;

namespace Service.Utilities
{
    public class AppSettings : ConfigAssistant
    {
        public static string UploadFolderPhysicalPath => GetConfigValue();
        public static string UploadFolderHttpPath => GetConfigValue();
    }

    public class ConfigAssistant
    {
        protected static string GetConfigValue([CallerMemberName]string key = "")
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
