using System;
using System.Collections.Generic;
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
using System.Data;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for VnosPodatkovPijace.xaml
    /// </summary>
    public partial class VnosPodatkovPijace : Page
    {
        MainWindow mainwindow { get; set; }
        List<Pijaca> pijacas { get; set; }

        public VnosPodatkovPijace(List<Pijaca> pijacas)
        {
            InitializeComponent();
            this.pijacas = pijacas;

            DataTable komora = ustvariPraznoTabelo();
            DataTable nosac = ustvariPraznoTabelo();

            // Set DataTable as the ItemsSource of both data grids
            dataTable1.ItemsSource = komora.DefaultView;
            dataTable2.ItemsSource = nosac.DefaultView;

        }

        //To bas event za generiranje pdf-ja jaša, mormo pa se postudirat malo kak omejit tote gumbe ce ponesreci kdo kline, v smislu da morajo biti vsa polja
        //Izpolnjena pa to...

        /*
            Note to self, zmenit se moramo kako prikazujemo podatke v tretji tabeli, torej kdaj bo + in kdaj - torej ce si komora ne zapise ali ce si nosac nezapise
            prav tako se moramo dogovorit za stvari ki se vrnejo nazaj, če se te odštejejo od koncnega rezulata računanja stanja, ker kolko vem to piše samo nosač
            komora pa ne
         */

        private void GenerirajPorocilo_ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Generiraj porocilo");
        }

        private void Primerjaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Create a new DataTable to store the differences
            DataTable diffTable = new DataTable();

            // Get DataTables from DataGrids
            DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
            DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;

            // Add columns to the diffTable (same as Komora and Nosac)
            for (int i = 0; i < Komora.Columns.Count; i++)
            {
                diffTable.Columns.Add(Komora.Columns[i].ColumnName, Komora.Columns[i].DataType);
            }

            // Add the "Znesek" column to the diffTable
            diffTable.Columns.Add("Znesek", typeof(int));

            // Iterate through each row of Komora and Nosac
            int rowCount = Math.Min(Komora.Rows.Count, Nosac.Rows.Count);
            for (int i = 0; i < rowCount; i++)
            {
                DataRow komoraRow = Komora.Rows[i];
                DataRow nosacRow = Nosac.Rows[i];

                // Create a new row for the diffTable
                DataRow diffRow = diffTable.NewRow();

                // Fill the first and second columns with the same values from the previous tables
                diffRow[0] = komoraRow[0];
                diffRow[1] = komoraRow[1];

                // Convert empty cells to 0 in the third column
                int komoraZac = Convert.ToInt32(komoraRow[2] == DBNull.Value ? 0 : komoraRow[2]);
                int nosacZac = Convert.ToInt32(nosacRow[2] == DBNull.Value ? 0 : nosacRow[2]);

                // Compare summed values
                if (komoraZac != nosacZac)
                {
                    diffRow[2] = komoraZac - nosacZac;
                }
                else
                {
                    diffRow[2] = 0;
                }

                // Split and sum values in the 4th column
                string[] komoraValues = komoraRow[3].ToString().Split(',');
                string[] nosacValues = nosacRow[3].ToString().Split(',');

                int sumKomora = 0;
                int sumNosac = 0;

                foreach (string value in komoraValues)
                {
                    if (!string.IsNullOrEmpty(value))
                        sumKomora += int.Parse(value);
                }

                foreach (string value in nosacValues)
                {
                    if (!string.IsNullOrEmpty(value))
                        sumNosac += int.Parse(value);
                }

                // Compare summed values
                if (sumKomora != sumNosac)
                {
                    diffRow[3] = sumKomora - sumNosac;
                }
                else
                {
                    diffRow[3] = 0;
                }

                //value v 5tki
                double cenaPijace = pijacas[i].cena;
                int razlika = Convert.ToInt32(diffRow[3]);

                var KoncniZnesek = razlika * cenaPijace;

                diffRow[5] = KoncniZnesek;


                // Add the diffRow to the diffTable
                diffTable.Rows.Add(diffRow);
            }

            // Set diffTable as the ItemsSource of dataTable3
            dataTable3.ItemsSource = diffTable.DefaultView;

        }


        private DataTable ustvariPraznoTabelo()
        {
            // Create DataTable with 5 columns
            DataTable datatable = new DataTable();
            for (int i = 1; i <= 5; i++)
            {
                datatable.Columns.Add($"Column{i}", typeof(string));
            }
            datatable.Columns[0].ColumnName = "Pijaca";
            datatable.Columns[1].ColumnName = "Kolicina";
            datatable.Columns[2].ColumnName = "Zacetno";
            datatable.Columns[3].ColumnName = "Vmesno";
            datatable.Columns[4].ColumnName = "Koncno";

            for (int i = 0; i < pijacas.Count; i++)
            {
                DataRow row = datatable.NewRow();
                row[0] = pijacas[i].ime;
                row[1] = "Kol.";

                datatable.Rows.Add(row);
            }
            return datatable;
        }
    }
}
