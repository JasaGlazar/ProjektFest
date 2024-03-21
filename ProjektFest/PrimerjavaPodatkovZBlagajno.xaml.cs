using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for PrimerjavaPodatkovZBlagajno.xaml
    /// </summary>
    public partial class PrimerjavaPodatkovZBlagajno : Page
    {
        DataTable komoraKopija {  get; set; }
        DataTable NosacKopija { get; set; }
        DataTable RazlikaKopija { get; set; }
        MainWindow MainWindowKopija { get; set; }
        int Index {  get; set; }

        public PrimerjavaPodatkovZBlagajno(DataTable Komora, DataTable Nosac, DataTable Razlika, MainWindow mainWindow, int index)
        {
            InitializeComponent();
            this.komoraKopija = Komora;
            this.NosacKopija = Nosac;
            this.RazlikaKopija = Razlika;
            this.MainWindowKopija = mainWindow;
            this.Index = index;

            DataTable PrikaziTabelo = Utilities.UstvariKomoraSumiranaDataTable(komoraKopija);
            dataTable1Blagajna.ItemsSource = PrikaziTabelo.DefaultView;
            dataTable1Blagajna.IsReadOnly = true;

            DataTable PrikaziBlagajno = Utilities.UstvariBlagajnoZaVstavljanje();
            dataTable2Blagajna.ItemsSource = PrikaziBlagajno.DefaultView;
        }

        private void PrimerjajBlagajnoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable Komora = ((DataView)dataTable1Blagajna.ItemsSource).Table;
                DataTable blagajna = ((DataView)dataTable2Blagajna.ItemsSource).Table;
                DataTable DiffDT = Utilities.VrniPrimerjavoKomoreInBlagajne(Komora, blagajna);

                dataTable3Blagajna.ItemsSource = DiffDT.DefaultView;
                dataTable3Blagajna.IsReadOnly = true;

                MessageBox.Show("POMOČ: \n Če je končno število pri 3 tabeli negativno, to pomeni, da se je iz komore odneslo več ven kot pa se je prodalo! \n " +
                    "Če je končno število pri 3 tabeli pozitivno, to pomeni, da se je prodalo več artiklov, kot jih je bilo nesenih iz komore! \n " +
                    "Če je število 0 pomeni, da se je enako število artiklov odneslo iz komore in prodalo.");
            }
            catch (Exception ex)
            {

                throw new Exception("Napaka: " + ex.Message);
            }
        }

        //TODO
        private void PorociloButton_Click(object sender, RoutedEventArgs e)
        {
            //Porocilo
        }
        private void ShraniPodatke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable KomoraSum = ((DataView)dataTable1Blagajna.ItemsSource).Table;
                DataTable BlagajnaVred = ((DataView)dataTable2Blagajna.ItemsSource).Table;
                DataTable RazlikaBlagKom = ((DataView)dataTable3Blagajna.ItemsSource).Table;

                // Create an instance of ShraniObjektSank
                ShraniObjektSank sos = new ShraniObjektSank
                {
                    // Assign properties of sos
                    imePrireditve = MainWindowKopija.prireditev.ime_prireditve,
                    letoPrireditve = MainWindowKopija.prireditev.leto_prireditve,
                    sank = MainWindowKopija.prireditev.sanki[this.Index].ime,
                    kelnarji = MainWindowKopija.prireditev.sanki[this.Index].natakarji,
                    nosac = MainWindowKopija.prireditev.sanki[this.Index].nosac,
                    Komora = komoraKopija,
                    NosacDataTable = NosacKopija,
                    Rezultat = RazlikaKopija,
                    KomoraSumirane = KomoraSum,
                    Blagajna = BlagajnaVred,
                    RazlikaBlagajnaKomora = RazlikaBlagKom,
                };

                // Serialize sos object to JSON string
                string json = JsonConvert.SerializeObject(sos);

                // Display SaveFileDialog to choose the file location
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "Fest Files|*.fest|All Files|*.*";
                saveFileDialog.Title = "Save Fest Data";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Write the JSON string to the selected file
                    File.WriteAllText(saveFileDialog.FileName, json);

                    // Show success message if the data is saved successfully
                    MessageBox.Show("Podatki so bili uspešno shranjeni!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Show message if the user cancels the save operation
                    MessageBox.Show("Shranjevanje je uporabnik prekinil.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Display error message if any exception occurs during the process
                MessageBox.Show($"Napaka pri shranjevanju!: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
