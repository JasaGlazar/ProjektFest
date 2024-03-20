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
using System.Reflection.Metadata.Ecma335;
using System.Printing;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Renderer;
using iText.Kernel.Font;
using iText.IO.Font;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Ocsp;
using System.Windows.Markup;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Globalization;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for VnosPodatkovPijace.xaml
    /// </summary>
    public partial class VnosPodatkovPijace : Page
    {
        MainWindow mainwindow { get; set; }
        List<Pijaca> pijacas { get; set; }
        List<Pijaca> pijacaDataTable { get; set; }
        int index { get; set; }

        public VnosPodatkovPijace(List<Pijaca> pijacas, MainWindow mainWindow, int index)
        {
            InitializeComponent();
            this.pijacas = pijacas;
            this.pijacaDataTable = Pijaca.DataTablePijaca();
            this.mainwindow = mainWindow;
            this.index = index;

            //Pridobi ime sanka iz tab-a
            ImeSanka.Content = this.mainwindow.prireditev.sanki.ElementAt(index).ime;
            //pridobi imena kelnarjev
            KelnarjiListView.ItemsSource = this.mainwindow.prireditev.sanki.ElementAt(index).natakarji;
            //pridobi ime nosacev
            NosacText.Text = $"{this.mainwindow.prireditev.sanki.ElementAt(index).nosac.ime} {this.mainwindow.prireditev.sanki.ElementAt(index).nosac.priimek}";

            DataTable komora = ustvariPraznoTabelo();
            DataTable nosac = ustvariPraznoTabelo();

            //Prikazi tabli
            dataTable1.ItemsSource = komora.DefaultView;
            dataTable2.ItemsSource = nosac.DefaultView;

        }


        private void GenerirajPorocilo_ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Generiraj porocilo");
            UstvariPDF();
        }
        private void Primerjaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                PorociloButton.IsEnabled = true;
                BlagajnaButton.IsEnabled = true;
                //Ustvarjena tretja tabela ki bo prikazala rezultate glede na prvi dve
                DataTable diffTable = new DataTable();

                //Pridobivanje prvih dveh izpolnjenih tabel
                DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
                DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;

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
                    dataTable3.ItemsSource = diffTable.DefaultView;
                    dataTable3.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Vrednost v celici ni število!");
                }
            }
            catch (Exception ex)
            {
                // Alert the user of any errors
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UstvariPDF()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF document (*.pdf)|*.pdf";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
                DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;
                DataTable Rezultat = ((DataView)dataTable3.ItemsSource).Table;

                string nazivPrireditve = this.mainwindow.prireditev.ime_prireditve + " " + this.mainwindow.prireditev.leto_prireditve;


                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        var writer = new iText.Kernel.Pdf.PdfWriter(fs);
                        var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                        var document = new Document(pdf);

                       // string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                       // string relativeFontPath = System.IO.Path.Combine("Resources", "Roboto-Medium.ttf");
                       // string fullFontPath = System.IO.Path.Combine (baseDirectory, relativeFontPath);

                       // PdfFont font = PdfFontFactory.CreateFont(fullFontPath, PdfEncodings.IDENTITY_H);

                       // document.SetFont(font);

                        string imeNosaca = NosacText.Text;
                        string imeSanka = (string)ImeSanka.Content;
                        List<Oseba> seznamNatakarjev = (List<Oseba>)KelnarjiListView.ItemsSource;
                        DateTime datumNastanka = DateTime.Now;

                        iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("Poročilo o prodani pijači")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(20);
                        document.Add(header);

                        iText.Layout.Element.Paragraph header1 = new iText.Layout.Element.Paragraph("Fest.si")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(header1);

                        iText.Layout.Element.Paragraph naslov = new iText.Layout.Element.Paragraph("Rajšpova ulica 16")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(naslov);

                        iText.Layout.Element.Paragraph posta = new iText.Layout.Element.Paragraph("2250 Ptuj")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(posta);

                        LineSeparator lineSeparator = new LineSeparator(new SolidLine());
                        document.Add(lineSeparator);

                        iText.Layout.Element.Paragraph imePrireditveParagraph = new iText.Layout.Element.Paragraph("Ime prireditve: " + nazivPrireditve);
                        document.Add(imePrireditveParagraph);

                        iText.Layout.Element.Paragraph imeSankaParagraph = new iText.Layout.Element.Paragraph("Ime šanka: " + imeSanka);
                        document.Add(imeSankaParagraph);

                        iText.Layout.Element.Paragraph datumNastankaParagraph = new iText.Layout.Element.Paragraph("Datum nastanka poročila: " + datumNastanka.ToString());
                        document.Add(datumNastankaParagraph);

                        iText.Layout.Element.Paragraph imeNosacaParagraph = new iText.Layout.Element.Paragraph("Ime nosača: " + imeNosaca);
                        document.Add(imeNosacaParagraph);

                        iText.Layout.Element.Paragraph natakarjiParagraph = new iText.Layout.Element.Paragraph("Natakarji: ");
                        document.Add(natakarjiParagraph);

                        foreach (var item in seznamNatakarjev)
                        {
                            iText.Layout.Element.Paragraph imeNatakarjaParagraph = new iText.Layout.Element.Paragraph(item.ime + " " + item.priimek);
                            document.Add(imeNatakarjaParagraph);

                        }

                        IncludeDataTableInPdf(document, "Komora", Komora);
                        IncludeDataTableInPdf(document, "Nosac", Nosac);
                        IncludeDataTableInPdf(document, "Rezultat", Rezultat);


                        document.Close();

                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message);
            }
        }
        private void IncludeDataTableInPdf(Document document, string tableName, DataTable dataTable)
        {
            iText.Layout.Element.Paragraph tableTitle = new iText.Layout.Element.Paragraph(tableName)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(tableTitle);

            // Create a table
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(dataTable.Columns.Count);

            // Add table headers
            foreach (DataColumn column in dataTable.Columns)
            {
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(column.ColumnName)));
            }

            // Add table rows
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.ToString())));
                }
            }

            // Add the table to the document
            document.Add(table);

            // Add space between tables
            document.Add(new AreaBreak());
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

            // Set the first and second columns as read-only
            datatable.Columns[0].ReadOnly = true;
            datatable.Columns[1].ReadOnly = true;

            for (int i = 0; i < pijacaDataTable.Count; i++)
            {
                DataRow row = datatable.NewRow();
                row[0] = pijacaDataTable[i].ime;
                row[1] = "Kol.";

                datatable.Rows.Add(row);
            }
            return datatable;
        }
        private void ShraniPodatke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get DataTable instances from data sources
                DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
                DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;
                DataTable Rezultat = ((DataView)dataTable3.ItemsSource).Table;

                // Create an instance of ShraniObjektSank
                ShraniObjektSank sos = new ShraniObjektSank
                {
                    // Assign properties of sos
                    imePrireditve = mainwindow.prireditev.ime_prireditve,
                    letoPrireditve = mainwindow.prireditev.leto_prireditve,
                    sank = mainwindow.prireditev.sanki[this.index].ime,
                    kelnarji = mainwindow.prireditev.sanki[this.index].natakarji,
                    nosac = mainwindow.prireditev.sanki[this.index].nosac,
                    Komora = Komora,
                    NosacDataTable = Nosac,
                    Rezultat = Rezultat,
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

        }
        private void Blagajna_Click(object sender, RoutedEventArgs e)
        {
            DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
            DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;
            DataTable Razlika = ((DataView)dataTable3.ItemsSource).Table;

            mainwindow.Main.Content = new PrimerjavaPodatkovZBlagajno(Komora, Nosac, Razlika, this.mainwindow, this.index);
        }
    }
}
