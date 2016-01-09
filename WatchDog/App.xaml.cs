using System.Windows;
using System.IO;

namespace WatchDog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(Constants.TRACKED_PROGRAMS_DIR) && !File.Exists(Constants.TRACKED_PROGRAMS_PATH))
            {
                FirstStartup firstStartupWindow = new FirstStartup();
                firstStartupWindow.Show();
                MessageBox.Show("Now, please, add programs in which you want to track your activity.", "First startup setup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
