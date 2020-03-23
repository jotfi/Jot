using jotfi.Jot.Core.Settings;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Reflection;

namespace jotfi.Jot.Console
{
    public class Program
    {
        static Logger Log { get; } = LogManager.GetLogger(typeof(Program).FullName);

        static void Main()
        {
            var settings = new AppSettings();
            var appPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).CodeBase);
            var settingsFile = Path.Combine(appPath, "appsettings.json");
            if (File.Exists(settingsFile))
            {
                var settingsString = File.ReadAllText(settingsFile);
                try
                {
                    settings = (AppSettings)JsonConvert.DeserializeObject(settingsString, typeof(AppSettings));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            else
            {
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(settings));
            }
            new ConsoleApplication(settings.IsClient).Run();
        }
    }
}
