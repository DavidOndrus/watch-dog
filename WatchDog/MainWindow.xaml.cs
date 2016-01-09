using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;

namespace WatchDog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ProgramItem> trackedProgramsStats;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addRemove_btn_Click(object sender, RoutedEventArgs e)
        {
            FirstStartup fsWindow = new FirstStartup();
            fsWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadCollection();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, Constants.MINUTE_COUNT, 0);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < trackedProgramsStats.Count; i++)
            {
                ProgramItem item = trackedProgramsStats[i];
                if (Process.GetProcessesByName(item.Name).Length != 0)
                {
                    item.increaseRunningTime(Constants.MINUTE_COUNT);
                    int index = trackedProgramsStats.IndexOf(item);
                    trackedProgramsStats.RemoveAt(index);
                    trackedProgramsStats.Insert(index, item);
                }
            }
        }

        private void loadCollection()
        {
            MyCollection mc = new MyCollection(File.ReadAllLines(Constants.TRACKED_PROGRAMS_PATH));
            trackedProgramsStats = mc.makeCollection();
            programs_listB.ItemsSource = trackedProgramsStats;
        }
    }
}