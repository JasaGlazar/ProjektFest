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

            DataTable PrikaziTabelo = UstvariKomoraSumiranaDataTable(komoraKopija);
            dataTable1Blagajna.ItemsSource = PrikaziTabelo.DefaultView;
            dataTable1Blagajna.IsReadOnly = true;

            DataTable PrikaziBlagajno = UstvariBlagajnoZaVstavljanje();
            dataTable2Blagajna.ItemsSource = PrikaziBlagajno.DefaultView;
        }

        private void PrimerjajBlagajnoButton_Click(object sender, RoutedEventArgs e)
        {

            DataTable NewKomoraDataTable = ((DataView)dataTable1Blagajna.ItemsSource).Table;

            DataRow row0 = NewKomoraDataTable.Rows[0];
            DataRow row1 = NewKomoraDataTable.Rows[1];
            DataRow row2 = NewKomoraDataTable.Rows[2];
            DataRow row3 = NewKomoraDataTable.Rows[3];
            DataRow row4 = NewKomoraDataTable.Rows[4];
            DataRow row5 = NewKomoraDataTable.Rows[5];
            DataRow row6 = NewKomoraDataTable.Rows[6];
            DataRow row7 = NewKomoraDataTable.Rows[7];
            DataRow row8 = NewKomoraDataTable.Rows[8];
            DataRow row9 = NewKomoraDataTable.Rows[9];
            DataRow row10 = NewKomoraDataTable.Rows[10];
            DataRow row11 = NewKomoraDataTable.Rows[11];
            DataRow row12 = NewKomoraDataTable.Rows[12];
            DataRow row13 = NewKomoraDataTable.Rows[13];
            DataRow row14 = NewKomoraDataTable.Rows[14];
            DataRow row15 = NewKomoraDataTable.Rows[15];
            DataRow row16 = NewKomoraDataTable.Rows[16];
            DataRow row17 = NewKomoraDataTable.Rows[17];
            DataRow row18 = NewKomoraDataTable.Rows[18];
            DataRow row19 = NewKomoraDataTable.Rows[19];
            DataRow row20 = NewKomoraDataTable.Rows[20];

            //Pretvorba v enote za primerjavo
            double KomoraCola = (Convert.ToDouble(row0[1]) * 15) + (Convert.ToDouble(row1[1]) * 5);
            double KomoraSchweps = (Convert.ToDouble(row2[1]) * 15);
            double KomoraOra = (Convert.ToDouble(row3[1]) * 15) + (Convert.ToDouble(row4[1]) * 5);
            double KomoraLedeniCaj = (Convert.ToDouble(row5[1]) * 5);
            double KomoraVodaOkus = (Convert.ToDouble(row6[1]) * 5);
            double KomoraVodaBrez = (Convert.ToDouble(row7[1]) * 5);
            double KomoraJuice = (Convert.ToDouble(row8[1]) * 10);
            double KomoraRadenska = (Convert.ToDouble(row9[1]) * 5);
            double KomoraEnergy = (Convert.ToDouble(row10[1]) * 2.5);
            double KomoraLasko = (Convert.ToDouble(row11[1]) * 1);
            double KomoraUnion = (Convert.ToDouble(row12[1]) * 1);
            double KomoraUniBrez = (Convert.ToDouble(row13[1]) * 1);
            double KomoraVinoBelo = (Convert.ToDouble(row14[1]) * 10);
            double KomoraVinoRdeče = (Convert.ToDouble(row15[1]) * 10);
            double KomoraVodka = (Convert.ToDouble(row16[1]) * 33);
            double KomoraBorovnicke = (Convert.ToDouble(row17[1]) * 33);
            double KomoraJagermaiset = (Convert.ToDouble(row18[1]) * 33);
            double KomoraJackDaniels = (Convert.ToDouble(row19[1]) * 33);
            double KomoraGin = (Convert.ToDouble(row20[1]) * 33);

            //Enote zaj mam semizdi, zaj bi pa mogo simulirat cenik pa ga vkup dat pm...

            DataTable newBlagajnaDataTable = ((DataView)dataTable2Blagajna.ItemsSource).Table;

            DataRow Brow0 = newBlagajnaDataTable.Rows[0];
            DataRow Brow1 = newBlagajnaDataTable.Rows[1];
            DataRow Brow2 = newBlagajnaDataTable.Rows[2];
            DataRow Brow3 = newBlagajnaDataTable.Rows[3];
            DataRow Brow4 = newBlagajnaDataTable.Rows[4];
            DataRow Brow5 = newBlagajnaDataTable.Rows[5];
            DataRow Brow6 = newBlagajnaDataTable.Rows[6];
            DataRow Brow7 = newBlagajnaDataTable.Rows[7];
            DataRow Brow8 = newBlagajnaDataTable.Rows[8];
            DataRow Brow9 = newBlagajnaDataTable.Rows[9];
            DataRow Brow10 = newBlagajnaDataTable.Rows[10];
            DataRow Brow11 = newBlagajnaDataTable.Rows[11];
            DataRow Brow12 = newBlagajnaDataTable.Rows[12];
            DataRow Brow13 = newBlagajnaDataTable.Rows[13];
            DataRow Brow14 = newBlagajnaDataTable.Rows[14];
            DataRow Brow15 = newBlagajnaDataTable.Rows[15];
            DataRow Brow16 = newBlagajnaDataTable.Rows[16];
            DataRow Brow17 = newBlagajnaDataTable.Rows[17];
            DataRow Brow18 = newBlagajnaDataTable.Rows[18];
            DataRow Brow19 = newBlagajnaDataTable.Rows[19];
            DataRow Brow20 = newBlagajnaDataTable.Rows[20];
            DataRow Brow21 = newBlagajnaDataTable.Rows[21];
            DataRow Brow22 = newBlagajnaDataTable.Rows[22];
            DataRow Brow23 = newBlagajnaDataTable.Rows[23];
            DataRow Brow24 = newBlagajnaDataTable.Rows[24];
            DataRow Brow25 = newBlagajnaDataTable.Rows[25];
            DataRow Brow26 = newBlagajnaDataTable.Rows[26];
            DataRow Brow27 = newBlagajnaDataTable.Rows[27];
            DataRow Brow28 = newBlagajnaDataTable.Rows[28];
            DataRow Brow29 = newBlagajnaDataTable.Rows[29];
            DataRow Brow30 = newBlagajnaDataTable.Rows[30];
            DataRow Brow31 = newBlagajnaDataTable.Rows[31];
            DataRow Brow32 = newBlagajnaDataTable.Rows[32];
            DataRow Brow33 = newBlagajnaDataTable.Rows[33];
            DataRow Brow34 = newBlagajnaDataTable.Rows[34];
            DataRow Brow35 = newBlagajnaDataTable.Rows[35];
            DataRow Brow36 = newBlagajnaDataTable.Rows[36];
            DataRow Brow37 = newBlagajnaDataTable.Rows[37];
            DataRow Brow38 = newBlagajnaDataTable.Rows[38];

            double BlagajnaCola = (Convert.ToDouble(Brow0[1]) * 1) + (Convert.ToDouble(Brow1[1]) * 5) + (Convert.ToDouble(Brow21[1]) * 1) + (Convert.ToDouble(Brow27[1]) * 1) + (Convert.ToDouble(Brow29[1]) * 1) + (Convert.ToDouble(Brow30[1]) * 1) + (Convert.ToDouble(Brow34[1]) * 15) + (Convert.ToDouble(Brow36[1]) * 15) + (Convert.ToDouble(Brow37[1]) * 15);
            double BlagajnaSchweps = (Convert.ToDouble(Brow11[1]) * 1) + (Convert.ToDouble(Brow31[1]) * 1) + (Convert.ToDouble(Brow38[1]) * 15);
            double BlagajnaOra = (Convert.ToDouble(Brow2[1]) * 1) + (Convert.ToDouble(Brow3[1]) * 5) + (Convert.ToDouble(Brow19[1]) * 1) + (Convert.ToDouble(Brow20[1]) * 1) + (Convert.ToDouble(Brow36[1]) * 15);
            double BlagajnaLedeniCaj = (Convert.ToDouble(Brow5[1]) * 5);
            double BlagajnaVodaOkus = (Convert.ToDouble(Brow6[1]) * 5);
            double BlagajnaBrezOkus = (Convert.ToDouble(Brow7[1]) * 5);
            double BlagajnaJuice = (Convert.ToDouble(Brow4[1]) * 1) + (Convert.ToDouble(Brow28[1]) * 1) + (Convert.ToDouble(Brow35[1]) * 10);
            double BlagajnaRadenska = (Convert.ToDouble(Brow8[1]) * 1) + (Convert.ToDouble(Brow9[1]) * 5) + (Convert.ToDouble(Brow18[1]) * 1);
            double BlagajnaEnergijskaPijaca = (Convert.ToDouble(Brow10[1]) * 2.5) + (Convert.ToDouble(Brow32[1]) * 2.5) + (Convert.ToDouble(Brow33[1]) * 15);
            //Pitaj če je v blagajni posebaj lasko in union al je skupaj???
            double BlagajnaLasko = (Convert.ToDouble(Brow12[1]) * 1);
            double BlagajnaUnion = (Convert.ToDouble(Brow12[1]) * 1);
            double BlagajnaUniBrez = (Convert.ToDouble(Brow13[1]) * 1);
            double BlagajnaVinoBelo = (Convert.ToDouble(Brow14[1]) * 1) + (Convert.ToDouble(Brow15[1]) * 10) + (Convert.ToDouble(Brow18[1]) * 1) + (Convert.ToDouble(Brow19[1]) * 1);
            double BlagajnaVinoRdece = (Convert.ToDouble(Brow16[1]) * 1) + (Convert.ToDouble(Brow17[1]) * 10) + (Convert.ToDouble(Brow20[1]) * 1) + (Convert.ToDouble(Brow21[1]) * 1);
            double BlagajnaVodka = (Convert.ToDouble(Brow22[1]) * 1) + (Convert.ToDouble(Brow27[1]) * 1) + (Convert.ToDouble(Brow28[1]) * 1) + (Convert.ToDouble(Brow32[1]) * 2) + (Convert.ToDouble(Brow33[1]) * 33) + (Convert.ToDouble(Brow34[1]) * 33) + (Convert.ToDouble(Brow35[1]) * 33);
            double BlagajnaBorovnicke = (Convert.ToDouble(Brow25[1]) * 1);
            double BlagajnaJagermaister = (Convert.ToDouble(Brow23[1]) * 1) + (Convert.ToDouble(Brow29[1]) * 1) + (Convert.ToDouble(Brow37[1]) * 33);
            double BlagajnaJackDaniels = (Convert.ToDouble(Brow24[1]) * 1) + (Convert.ToDouble(Brow30[1]) * 1) + (Convert.ToDouble(Brow36[1]) * 33);
            double BlagajnaGin = (Convert.ToDouble(Brow26[1]) * 1) + (Convert.ToDouble(Brow31[1]) * 1) + (Convert.ToDouble(Brow38[1]) * 33);

            //Konca primerjava
            // + pomeni da se je izdalo več pijače kot se je prodalo ( neki ne stima )
            // - pomeni da se je prodalo več pijače kot se je prineslo? ( neki ne stima )
            // mislim da bi mogo lih kontra nardit, kao od blagajne odstet da bi vedle če je minus prave i think kao da se je vec prineslo kak prodalo
            double RezultatCola = BlagajnaCola - KomoraCola;
            double RezultatSchweps = BlagajnaSchweps - KomoraSchweps;
            double RezultatOra = BlagajnaOra - KomoraOra;
            double RezultatLedeniCaj = BlagajnaLedeniCaj - KomoraLedeniCaj;
            double RezultatVodaOkus = BlagajnaVodaOkus - KomoraVodaOkus;
            double RezultatVodaBrez = BlagajnaBrezOkus - KomoraVodaBrez;
            double RezultatJuice = BlagajnaJuice - KomoraJuice;
            double RezultatRadenska = BlagajnaRadenska - KomoraRadenska;
            double RezultatEnergijski = BlagajnaEnergijskaPijaca - KomoraEnergy;
            double RezultatLasko = BlagajnaLasko - KomoraLasko;
            double RezultatUnion = BlagajnaUnion - KomoraUnion;
            double RezultatUnibrez = BlagajnaUniBrez - KomoraUniBrez;
            double RezultatVinoBelo = BlagajnaVinoBelo - KomoraVinoBelo;
            double RezultatVinoRdece = BlagajnaVinoRdece - KomoraVinoRdeče;
            double RezultatVodka = BlagajnaVodka - KomoraVodka;
            double RezultatBorovnicke = BlagajnaBorovnicke - KomoraBorovnicke;
            double RezultatJagermeister = BlagajnaJagermaister - KomoraJagermaiset;
            double RezultatJackDaniels = BlagajnaJackDaniels - KomoraJackDaniels;
            double RezultatGin = BlagajnaGin - KomoraGin;

            //TODO ustvari tabelo 3 pa pripiši vrednosti also neke vrednosti bi se dale pomoje zračunat ne :)
            DataTable DiffDT = new DataTable();
            DiffDT.Columns.Add("Pijaca");
            DiffDT.Columns.Add("Vrednost po enotah");

            DiffDT.Rows.Add("Pepsi Cola (enote oziroma 1dcl)", RezultatCola);
            DiffDT.Rows.Add("Schweps (enote oziroma 1dcl", RezultatSchweps);
            DiffDT.Rows.Add("Ora (enote oziroma 1dcl)", RezultatOra);
            DiffDT.Rows.Add("Ledeni Caj (enote oziroma 1dcl)", RezultatLedeniCaj);
            DiffDT.Rows.Add("Voda z okusom (enote oziroma 1dcl)", RezultatVodaOkus);
            DiffDT.Rows.Add("Voda brez okusa (enote oziroma 1dcl)", RezultatVodaBrez);
            DiffDT.Rows.Add("Juice (enote oziroma 1dcl)", RezultatJuice);
            DiffDT.Rows.Add("Radenska (enote oziroma 1dcl)", RezultatRadenska);
            DiffDT.Rows.Add("Energijski (enote oziroma 2.5dcl)", RezultatEnergijski);
            DiffDT.Rows.Add("Pivo Lasko (enote oziroma 1 Pivo)", RezultatLasko);
            DiffDT.Rows.Add("Pivo Union (enote oziroma 1 Pivo)", RezultatUnion);
            DiffDT.Rows.Add("Pivo Brez Alkh. (enote oziroma 1 Pivo)", RezultatUnibrez);
            DiffDT.Rows.Add("Belo vino (enote oziroma 1dcl)", RezultatVinoBelo);
            DiffDT.Rows.Add("Rdeče vino (enote oziroma 1dcl)", RezultatVinoRdece);
            DiffDT.Rows.Add("Vodka (enote oziroma 1l)", RezultatVodka/33);
            DiffDT.Rows.Add("Borovnicke (enote oziroma 1l)", RezultatBorovnicke/33);
            DiffDT.Rows.Add("Jagermeister (enote oziroma 1l)", RezultatJagermeister / 33);
            DiffDT.Rows.Add("Jack Daniels (enote oziroma 1l)", RezultatJackDaniels / 33);
            DiffDT.Rows.Add("Gin (enote oziroma 1l)", RezultatGin / 33);

            dataTable3Blagajna.ItemsSource = DiffDT.DefaultView;
            dataTable3Blagajna.IsReadOnly = true;

            MessageBox.Show("POMOČ: \n Če je končno število pri 3 tabeli negativno, to pomeni, da se je iz komore odneslo več ven kot pa se je prodalo! \n " +
                "Če je končno število pri 3 tabeli pozitivno, to pomeni, da se je prodalo več artiklov, kot jih je bilo nesenih iz komore! \n " +
                "Če je število 0 pomeni, da se je enako število artiklov odneslo iz komore in prodalo.");
        }


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
                    MessageBox.Show("The data has been saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Show message if the user cancels the save operation
                    MessageBox.Show("Save operation canceled by the user.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Display error message if any exception occurs during the process
                MessageBox.Show($"An error occurred while saving the data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("Shrani");
        }
        private DataTable UstvariKomoraSumiranaDataTable(DataTable komora)
        {

            DataTable newDataTable = new DataTable();

            newDataTable.Columns.Add("Pijaca",typeof(string));
            newDataTable.Columns.Add("Prodana kolicina",typeof(decimal));

            for (int i = 0; i < komora.Rows.Count; i++)
            {
                DataRow komoraRow = komora.Rows[i];
                DataRow DiffRow = newDataTable.NewRow();

                DiffRow[0] = komoraRow[0];
                
                double komoraZac = komoraRow[2] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[2].ToString()) ? 0 : Convert.ToDouble(komoraRow[2]);

                string[] komoraValues = komoraRow[3].ToString().Split(',');
                double sumKomora = 0;
                foreach (string value in komoraValues)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        // Parse each value using TryParse to handle decimal numbers correctly
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                            sumKomora += parsedValue;
                    }
                }

                double komoraKoncna = komoraRow[4] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[4].ToString()) ? 0 : double.TryParse(komoraRow[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result) ? result : 0;

                double ProdanaKolicinaPijace = (komoraZac + sumKomora) - komoraKoncna;

                DiffRow[1] = ProdanaKolicinaPijace;

                newDataTable.Rows.Add(DiffRow);

            }

            return newDataTable;
        }
        private DataTable UstvariBlagajnoZaVstavljanje()
        {
            DataTable newDataTable = new DataTable();
            List<Pijaca> PijacaCenik = Utilities.StalnaPijaca();

            newDataTable.Columns.Add("Ime Pijace");
            newDataTable.Columns.Add("Kolicina");

            for (int i = 0; i < PijacaCenik.Count; i++)
            {
                DataRow newRow = newDataTable.NewRow();

                newRow[0] = PijacaCenik[i].ime;
                newRow[1] = 0;

                newDataTable.Rows.Add(newRow);
            }
            newDataTable.Columns[0].ReadOnly = true;
            return newDataTable;
        }

    }
}
