using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace jotfi.Jot.Base.Settings
{
    public class SettingsUtils
    {
        static Logger Log { get; } = LogManager.GetLogger(typeof(SettingsUtils).FullName);

        public static T GetSettings<T>()
        {
            T appSettings = default;
            var settingsFile = GetSettingsFile();
            if (File.Exists(settingsFile))
            {
                var settingsString = File.ReadAllText(settingsFile);
                try
                {
                    appSettings = (T)JsonConvert.DeserializeObject(settingsString, typeof(T));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            return appSettings;
        }

        public static void SaveSettings(BaseSettings settings) => 
            File.WriteAllText(GetSettingsFile(), JsonConvert.SerializeObject(settings));

        static string GetSettingsFile()
        {
            var appPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            return Path.Combine(appPath, "appsettings.json");
        }
    }
}
