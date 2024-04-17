using Microsoft.Win32;
using Newtonsoft.Json;
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
using Path = System.IO.Path;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for PregledFolderaPreteklePrireditve.xaml
    /// </summary>
    public partial class PregledFolderaPreteklePrireditve : Page
    {
        MainWindow mainWindowKopija {  get; set; }
        public PregledFolderaPreteklePrireditve(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainWindowKopija = mainwindow;
            List<string> Datoteke = new List<string>();
            Datoteke = GetFestFilesInFolder();
            FilesListView.ItemsSource = Datoteke;

        }

        // Event handler for the "Preglej" button click
        private void PreglejButton_Click(object sender, RoutedEventArgs e)
        {

            if (FilesListView.SelectedItem != null)
            {
                string selectedFile = FilesListView.SelectedItem.ToString();
                ShraniObjektSank sos = DeserializeShraniObjektSank(selectedFile);

                if (sos != null)
                {
                    NaloziStaroPage nsp = new NaloziStaroPage(sos, mainWindowKopija);
                    mainWindowKopija.Main.Content = nsp;
                }
                else
                {
                    // Show message if deserialization failed
                    MessageBox.Show("Napaka pri deserializaciji podatkov. Morda je datoteka pokvarjena.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Izberite najprej Šank.");
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

        public static List<string> GetFestFilesInFolder()
        {
            List<string> festFiles = new List<string>();
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Izberite Prireditev";
                dialog.CheckFileExists = true;
                dialog.ValidateNames = false;
                dialog.FileName = "Izbira prireditve";

                if (dialog.ShowDialog() == true)
                {
                    string folderPath = Path.GetDirectoryName(dialog.FileName);
                    // Check if the selected folder exists
                    if (!Directory.Exists(folderPath))
                    {
                        Console.WriteLine("Izbrana datoteka ne obstaja!.");
                        return festFiles;
                    }

                    // Get all .fest files in the selected folder
                    string[] files = Directory.GetFiles(folderPath, "*.fest");
                    festFiles.AddRange(files);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return festFiles;
        }
    }
}
