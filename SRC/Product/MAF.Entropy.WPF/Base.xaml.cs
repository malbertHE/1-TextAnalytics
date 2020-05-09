using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace MAF.Entropy.WPF
{
    /// <summary>
    /// Interaction logic for Base.xaml
    /// </summary>
    public partial class Base : Page
    {
        BackgroundWorker worker = new BackgroundWorker();
        EntropyCalculator ec;
        string extension;

        public Base()
        {
            InitializeComponent();
            tbFile.Text = Properties.Settings.Default.DataFile;
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Feldolgozandó fájlok (*.txt)|*.txt|Eredmény fájlok (*.xml)|*.xml";

            if (tbFile.Text != string.Empty)
            {
                try
                {
                    if (File.Exists(tbFile.Text))
                        openFileDialog.InitialDirectory = Path.GetDirectoryName(tbFile.Text);
                    else if (Directory.Exists(tbFile.Text))
                        openFileDialog.InitialDirectory = tbFile.Text;
                }
                catch
                {
                }
            }

            if (openFileDialog.ShowDialog() == true)
            {
                tbFile.Text = openFileDialog.FileName;
                extension = Path.GetExtension(openFileDialog.FileName).ToLower();
                SetButtonRunAnalytics();
            }
        }

        void SetButtonRunAnalytics()
        {
            try
            {
                if (File.Exists(tbFile.Text))
                {
                    bRunAnalytics.IsEnabled = true;
                    if (extension == ".xml")
                    {
                        RunButtonText.Text = "Fájl betöltése";
                        LoadTextTabItem.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        RunButtonText.Text = "Fájl feldolgozása";
                        LoadTextTabItem.Visibility = Visibility.Visible;
                    }
                }
                else
                    bRunAnalytics.IsEnabled = false;

            }
            catch
            { }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                About login = new About();
                login.Owner = Application.Current.MainWindow;
                login.ShowDialog();

                bLoginTB1.Text = "Köszönöm, hogy megtekintetted a névjegyet!";
                AboutButton.Click -= AboutButton_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A névjegy ablak betöltése nem sikerült! {Environment.NewLine}Hiba: {ex.Message}", "Hiba");
            }
        }

        private void RunAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(tbFile.Text))
                {
                    MessageBox.Show("Nem létezik a megadott fájl!");
                    return;
                }

                if (extension == ".xml")
                {
                    XElement result = XElement.Load(tbFile.Text);
                    StatisticsGrid.DataContext = result;
                    DataPanel.Visibility = Visibility.Visible;
                    return;
                }

                IsEnabled = false;

                LoadingAnimation.Visibility = Visibility.Visible;
                DataPanel.Visibility = Visibility.Hidden;
                DataText.Text = File.ReadAllText(tbFile.Text);

                ec = new EntropyCalculator();
                ec.RunCalculation(tbFile.Text);

                Run r = (Run)LoadingAnimation.FindName("info");
                r.Text = $"{ec.RunningThreadCount} száll dolgozik";

                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                IsEnabled = true;
                LoadingAnimation.Visibility = Visibility.Hidden;
                MessageBox.Show($"A feldolgozás nem sikerült! {Environment.NewLine}Hiba: {ex.Message}", "Hiba");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XElement result = XElement.Load(ec.ResultFile);
            StatisticsGrid.DataContext = result;

            DataFile.Content = $"Feldolgozott fájl: {tbFile.Text}";

            LoadingAnimation.Visibility = Visibility.Hidden;
            DataPanel.Visibility = Visibility.Visible;
            IsEnabled = true;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ec.WaitForAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A feldolgozás nem sikerült! {Environment.NewLine}Hiba: {ex.Message}", "Hiba");
            }
        }

        delegate void WriteInfoBackground();

        private void StatisticsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StatisticsGrid.SelectedIndex == -1)
                return;
            ((MainWindow)Application.Current.MainWindow).Content = new Items((XElement)StatisticsGrid.SelectedItem);
        }

        private void File_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                extension = Path.GetExtension(tbFile.Text).ToLower();
            }
            catch
            {
                extension = "txt";
            }
            SetButtonRunAnalytics();
        }
    }
}