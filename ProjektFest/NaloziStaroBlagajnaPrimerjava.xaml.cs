using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for NaloziStaroBlagajnaPrimerjava.xaml
    /// </summary>
    public partial class NaloziStaroBlagajnaPrimerjava : Page
    {
        ShraniObjektSank sosKopija {  get; set; }
        DataTable KomoraKopija { get; set; }
        DataTable NosacKopija { get; set; }
        DataTable RazlikaKopija { get; set; }
        MainWindow mainWindowKopija { get; set; }
        int Index { get; set; }


        public NaloziStaroBlagajnaPrimerjava(ShraniObjektSank sos,DataTable Komora, DataTable Nosac, DataTable Razlika, MainWindow mainWindow)
        {
            InitializeComponent();
            this.sosKopija = sos;
            this.KomoraKopija = Komora;
            this.NosacKopija = Nosac;
            this.RazlikaKopija = Razlika;
            this.mainWindowKopija = mainWindow;

            DataTable PrikaziTabelo = Utilities.UstvariKomoraSumiranaDataTable(Komora);
            
            dataTable1BlagajnaStaro.ItemsSource = PrikaziTabelo.DefaultView;
            dataTable1BlagajnaStaro.IsReadOnly = true;

            sos.Blagajna.Columns[0].ReadOnly = true;
            dataTable2BlagajnaStaro.ItemsSource = sos.Blagajna.DefaultView;

        }

        private void PrimerjajStaroButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable Komora = ((DataView)dataTable1BlagajnaStaro.ItemsSource).Table;
            DataTable Blagajna = ((DataView)dataTable2BlagajnaStaro.ItemsSource).Table;
            DataTable Rezultat = Utilities.VrniPrimerjavoKomoreInBlagajne(Komora, Blagajna);

            dataTable3BlagajnaStaro.ItemsSource = Rezultat.DefaultView;
            dataTable3BlagajnaStaro.IsReadOnly=true;
            PorociloButton2.IsEnabled = true;
        }

        private void PorociloStaroButton_Click(object sender, RoutedEventArgs e)
        {
            //Jaša jaz sem razmišlo da bi vse metode dala v Utilities class, malo sem pozno vido da mamo toti razred, mam pa namen vse napisane metode tja dat, da bo bolj pregledno!
            DataTable blagajna = ((DataView)dataTable2BlagajnaStaro.ItemsSource).Table;
            DataTable primerjavaKomoraBlagajna = ((DataView)dataTable3BlagajnaStaro.ItemsSource).Table;
            string naziv = sosKopija.imePrireditve + " " + sosKopija.letoPrireditve;
            string nosacIme = $"{sosKopija.nosac.ime} {sosKopija.nosac.priimek}";
            string sank = sosKopija.sank;
            List<Oseba> natakarji = sosKopija.kelnarji;
            Utilities.UstvariPDF(KomoraKopija, NosacKopija, RazlikaKopija, blagajna, primerjavaKomoraBlagajna, naziv, nosacIme, sank, natakarji);
        }

        private void ShraniStaroPodatke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable KomoraSum = ((DataView)dataTable1BlagajnaStaro.ItemsSource).Table;
                DataTable BlagajnaVred = ((DataView)dataTable2BlagajnaStaro.ItemsSource).Table;
                DataTable RazlikaBlagKom = ((DataView)dataTable3BlagajnaStaro.ItemsSource).Table;

                // Create an instance of ShraniObjektSank
                ShraniObjektSank sos = new ShraniObjektSank
                {
                    // Assign properties of sos
                    imePrireditve = sosKopija.imePrireditve,
                    letoPrireditve = sosKopija.letoPrireditve,
                    sank = sosKopija.sank,
                    kelnarji = sosKopija.kelnarji,
                    nosac = sosKopija.nosac,
                    Komora = this.KomoraKopija,
                    NosacDataTable = this.NosacKopija,
                    Rezultat = this.RazlikaKopija,
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
