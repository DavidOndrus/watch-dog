using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WatchDog
{
    /// <summary>
    /// Interaction logic for FirstStartup.xaml
    /// </summary>
    public partial class FirstStartup : Window
    {
        ObservableCollection<ProgramItem> trackedProgramsList;
        
        public FirstStartup()
        {
            InitializeComponent();
        }

        public void fillCollection()
        {
            MyCollection myCollection = new MyCollection(File.ReadAllLines(Constants.TRACKED_PROGRAMS_PATH));
            trackedProgramsList = myCollection.makeCollection();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.FileName = "program.exe";
                dialog.DefaultExt = ".exe";
                dialog.Filter = "EXE files|*.exe";
                dialog.InitialDirectory = Constants.INITIAL_DIRECTORY_PATH;
                dialog.Title = "Select program to track";
                bool? success = dialog.ShowDialog();

                if (success == true)
                {
                    string programName = Path.GetFileNameWithoutExtension(dialog.FileName);
                    trackedProgramsList.Add(new ProgramItem(programName, dialog.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = String.Empty;
            if (trackedPrograms_listB.SelectedItem != null)
            {
                selectedItem = trackedPrograms_listB.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Nothing selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in trackedProgramsList)
            {
                if (item.ToString() == selectedItem)
                {
                    trackedProgramsList.Remove(item);
                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Constants.TRACKED_PROGRAMS_DIR))
            {
                fillCollection();
            }
            else
            {
                trackedProgramsList = new ObservableCollection<ProgramItem>();
            }
            trackedPrograms_listB.ItemsSource = trackedProgramsList;
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trackedProgramsList.Count != 0)
                {
                    if (!Directory.Exists(Constants.TRACKED_PROGRAMS_DIR))
                    {
                        Directory.CreateDirectory(Constants.TRACKED_PROGRAMS_DIR);
                    }

                    List<string> allLines = new List<string>();
                    foreach (var item in trackedProgramsList)
                    {
                        allLines.Add(item.GetNamePathInfo());
                    }

                    File.WriteAllLines(Constants.TRACKED_PROGRAMS_PATH, allLines);
                    
                    Application.Current.MainWindow.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You must add at least one program to save the configuration.\nIf you dont want to save anything, just close the window.", "Error while saving", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}