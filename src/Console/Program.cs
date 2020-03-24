using jotfi.Jot.Base.Settings;
using jotfi.Jot.Core.Settings;

namespace jotfi.Jot.Console
{
    public class Program
    {
        static void Main()
        {
            Terminal.Gui.Application.Init();
            var settings = SettingsUtils.GetSettings<AppSettings>() ?? new AppSettings();
            new ConsoleApplication(settings).Run();
        }
    }
}
