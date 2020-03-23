using jotfi.Jot.Core.Settings;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Reflection;
using Terminal.Gui;

namespace jotfi.Jot.Console
{
    public class Program
    {
        static Logger Log { get; } = LogManager.GetLogger(typeof(Program).FullName);

        static void Main()
        {
            Application.Init();
            var appSettings = new AppSettings();
            var appPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            var settingsFile = Path.Combine(appPath, "appsettings.json");
            if (File.Exists(settingsFile))
            {
                var settingsString = File.ReadAllText(settingsFile);
                try
                {
                    appSettings = (AppSettings)JsonConvert.DeserializeObject(settingsString, typeof(AppSettings));
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            else
            {
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(appSettings));
            }
            new ConsoleApplication(appSettings).Run();
        }
    }
}
