using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        MainWindow mainwindow;

        public StartPage()
        {
            InitializeComponent();
        }

        public StartPage(MainWindow mw)
        {
            InitializeComponent();
            mainwindow = mw;
        }

        private void NalziStaroBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Display OpenFileDialog to choose the file location
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Fest Files|*.fest|All Files|*.*";
                openFileDialog.Title = "Open Fest Data";

                if (openFileDialog.ShowDialog() == true)
                {
                    // Deserialize the data from the selected file
                    string filePath = openFileDialog.FileName;
                    ShraniObjektSank sos = DeserializeShraniObjektSank(filePath);

                    if (sos != null)
                    {
                        NaloziStaroPage nsp = new NaloziStaroPage(sos, mainwindow);
                        mainwindow.Main.Content = nsp;
                    }
                    else
                    {
                        // Show message if deserialization failed
                        MessageBox.Show("Failed to deserialize the data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any other errors that occur during the process
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ShraniObjektSank DeserializeShraniObjektSank(string filePath)
        {
            try
            {
                // Read the JSON data from the file
                string jsonData = File.ReadAllText(filePath);

                // Deserialize the JSON data into a ShraniObjektSank object
                ShraniObjektSank sos = JsonConvert.DeserializeObject<ShraniObjektSank>(jsonData);

                return sos;
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during deserialization
                MessageBox.Show($"An error occurred while deserializing the data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        private void UstvariNovoBtn_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Main.Content = new NovaPrireditevPage(mainwindow);
        }
    }
}
