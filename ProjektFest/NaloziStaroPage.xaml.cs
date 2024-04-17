using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for NaloziStaroPage.xaml
    /// </summary>
    public partial class NaloziStaroPage : Page
    {

        //TODO onemogoci dodajanje rows
        ShraniObjektSank sosKopija {  get; set; }
        MainWindow mainWindow1 {  get; set; }

        public NaloziStaroPage(ShraniObjektSank sos, MainWindow mainWindow)
        {
            InitializeComponent();
            this.sosKopija = sos;
            this.mainWindow1 = mainWindow;

            ImeSankaStaro.Content = sos.sank;
            KelnarjiListViewStaro.ItemsSource = sos.kelnarji;
            NosacTextStaro.Text = $"{sos.nosac.ime} {sos.nosac.priimek}";

            //komora
            sos.Komora.Columns[0].ReadOnly = true;
            sos.Komora.Columns[1].ReadOnly = true;
            dataTable1Staro.ItemsSource = sos.Komora.DefaultView;
            //nosac
            sos.NosacDataTable.Columns[0].ReadOnly = true;
            sos.NosacDataTable.Columns[1].ReadOnly = true;
            dataTable2Staro.ItemsSource = sos.NosacDataTable.DefaultView;
            //Razlika
            dataTable3Staro.IsReadOnly = true;
            dataTable3Staro.ItemsSource = sos.Rezultat.DefaultView;
        }

        private void PrimerjajStaro_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                //Ustvarjena tretja tabela ki bo prikazala rezultate glede na prvi dve
                DataTable diffTable = new DataTable();

                //Pridobivanje prvih dveh izpolnjenih tabel
                DataTable Komora = ((DataView)dataTable1Staro.ItemsSource).Table;
                DataTable Nosac = ((DataView)dataTable2Staro.ItemsSource).Table;

                bool prviPogoj = Utilities.ValidateDataTable(Komora);
                bool drugiPogoj = Utilities.ValidateDataTable(Nosac);

                if (prviPogoj && drugiPogoj)
                {
                    //Dodajanje vrstic diffTabeli
                    for (int i = 0; i < Nosac.Columns.Count; i++)
                    {
                        diffTable.Columns.Add(Nosac.Columns[i].ColumnName, Nosac.Columns[i].DataType);
                    }

                    //Dodaten column znesek za prikaz minus/plus v tretji tabeli
                    diffTable.Columns.Add("Skupna razlika", typeof(decimal));

                    //Grem skozi vse vrstice nosaca
                    int rowCount = Math.Min(Komora.Rows.Count, Nosac.Rows.Count);
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow komoraRow = Komora.Rows[i];
                        DataRow nosacRow = Nosac.Rows[i];

                        //ustvarim novo vrstico za novo tabelo za vsako tabelo iz osnovnih
                        DataRow diffRow = diffTable.NewRow();

                        //prvi dve celici v vseh vrsticah sta enaki zato samo vrednost prepisemo
                        diffRow[0] = komoraRow[0];
                        diffRow[1] = komoraRow[1];

                        // Pretvorba praznih vrednosti 0, da ne bo napak z null vrednostmi
                        double komoraZac = komoraRow[2] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[2].ToString()) ? 0 : Convert.ToDouble(komoraRow[2]);
                        double nosacZac = nosacRow[2] == DBNull.Value || string.IsNullOrWhiteSpace(nosacRow[2].ToString()) ? 0 : Convert.ToDouble(nosacRow[2]);

                        //Primerjava komora - nosac
                        if (komoraZac != nosacZac)
                        {
                            diffRow[2] = komoraZac - nosacZac;
                        }
                        else
                        {
                            diffRow[2] = 0;
                        }

                        //Splitnem vrednosti da pridobim koncno vrednost
                        string[] komoraValues = komoraRow[3].ToString().Split(',');
                        string[] nosacValues = nosacRow[3].ToString().Split(',');

                        double sumKomora = 0;
                        double sumNosac = 0;

                        foreach (string value in komoraValues)
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                // Parse each value using TryParse to handle decimal numbers correctly
                                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                                    sumKomora += parsedValue;
                            }
                        }

                        foreach (string value in nosacValues)
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                // Parse each value using TryParse to handle decimal numbers correctly
                                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                                    sumNosac += parsedValue;
                            }
                        }

                        //Primerjava suminarih vrednosti
                        if (Math.Abs(sumKomora - sumNosac) > double.Epsilon) // using double.Epsilon to account for floating point errors
                        {
                            diffRow[3] = sumKomora - sumNosac;
                        }
                        else
                        {
                            diffRow[3] = 0;
                        }

                        double komoraKoncna = komoraRow[4] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[4].ToString()) ? 0 : double.TryParse(komoraRow[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result) ? result : 0;
                        double NosacKoncna = nosacRow[4] == DBNull.Value || string.IsNullOrWhiteSpace(nosacRow[4].ToString()) ? 0 : double.TryParse(nosacRow[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double resultNosac) ? resultNosac : 0;
                        //Primerjava suminarih vrednosti
                        if (komoraKoncna != NosacKoncna)
                        {
                            diffRow[4] = komoraKoncna - NosacKoncna;
                        }
                        else
                        {
                            diffRow[4] = 0;
                        }

                        double vseSkupajKomora = komoraZac + sumKomora + komoraKoncna;
                        double vseSkupajNosac = nosacZac + sumNosac + NosacKoncna;

                        //Torej če je končna vrednost v celici "-", pomeni da je nosač ven odnesel več kot pa je komora zapisala, 
                        //Če je pa število pozitivno, pomeni da je komora več zapisala kot pa je nosač nesel ven
                        //Če je 0 pomeni da se vse sklada
                        decimal SkupnaRazlika = Convert.ToDecimal(vseSkupajKomora - vseSkupajNosac);

                        diffRow[5] = SkupnaRazlika;

                        //Dodajanje izpolnjene vrstice v novo generirano tabelo
                        diffTable.Rows.Add(diffRow);
                    }
                    // Set diffTable as the ItemsSource of dataTable3
                    dataTable3Staro.ItemsSource = diffTable.DefaultView;
                    dataTable3Staro.IsReadOnly = true;

                    sosKopija.Komora = ((DataView)dataTable1Staro.ItemsSource).Table;
                    sosKopija.NosacDataTable = ((DataView)dataTable2Staro.ItemsSource).Table;
                    sosKopija.Rezultat = ((DataView)dataTable2Staro.ItemsSource).Table;

                }
                else
                {
                    MessageBox.Show("Vrednost v celici ni število!");
                }
            }
            catch (Exception ex)
            {
                // Alert the user of any errors
                MessageBox.Show($"Pojavila se je napaka!: {ex.Message}", "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BlagajnaButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable Komora = ((DataView)dataTable1Staro.ItemsSource).Table;
            DataTable Nosac = ((DataView)dataTable2Staro.ItemsSource).Table;
            DataTable Razlika = ((DataView)dataTable3Staro.ItemsSource).Table;

            mainWindow1.Main.Content = new NaloziStaroBlagajnaPrimerjava(sosKopija,Komora, Nosac, Razlika, this.mainWindow1);
        }
    }
}
