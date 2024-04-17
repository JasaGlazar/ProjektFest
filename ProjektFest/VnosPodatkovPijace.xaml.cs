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

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for VnosPodatkovPijace.xaml
    /// </summary>
    public partial class VnosPodatkovPijace : Page
    {
        MainWindow mainwindow { get; set; }
        List<Pijaca> pijacas { get; set; }

        public VnosPodatkovPijace(List<Pijaca> pijacas, MainWindow mainWindow, int index)
        {
            InitializeComponent();
            this.pijacas = pijacas;
            this.mainwindow = mainWindow;

            //mogoce bi si mogo kot konstruktor vrzt iz prejsne strani index selectanega taba, pa mainwindow da bi dobo tote lastnosti


            //Pridobi ime sanka iz tab-a
            ImeSanka.Content = this.mainwindow.prireditev.sanki.ElementAt(index).ime;
            //pridobi imena kelnarjev
            KelnarjiListView.ItemsSource = this.mainwindow.prireditev.sanki.ElementAt(index).natakarji;
            //pridobi ime nosacev
            NosacText.Text = $"{this.mainwindow.prireditev.sanki.ElementAt(index).nosac.ime} {this.mainwindow.prireditev.sanki.ElementAt(index).nosac.priimek}";

           
            DataTable komora = ustvariPraznoTabelo();
            //Odstrani zadnji column v komora tabeli ker nerabimo koncnega stanja
            if (komora.Columns.Count > 0)
                komora.Columns.RemoveAt(komora.Columns.Count - 1);
            DataTable nosac = ustvariPraznoTabelo();
            //Prikazi tabli
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
            UstvariPDF();
        }

        private void Primerjaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                PorociloButton.IsEnabled = true;
                //Ustvarjena tretja tabela ki bo prikazala rezultate glede na prvi dve
                DataTable diffTable = new DataTable();

                //Pridobivanje prvih dveh izpolnjenih tabel
                DataTable Komora = ((DataView)dataTable1.ItemsSource).Table;
                DataTable Nosac = ((DataView)dataTable2.ItemsSource).Table;

                //Dodajanje vrstic diffTabeli
                for (int i = 0; i < Nosac.Columns.Count; i++)
                {
                    diffTable.Columns.Add(Nosac.Columns[i].ColumnName, Nosac.Columns[i].DataType);
                }

                //Dodaten column znesek za prikaz minus/plus v tretji tabeli
                diffTable.Columns.Add("Znesek", typeof(int));

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

                    //Pretvorba praznih vrednosti 0, da nebo napak z null vrednostmi
                    int komoraZac = Convert.ToInt32(komoraRow[2] == DBNull.Value ? 0 : komoraRow[2]);
                    int nosacZac = Convert.ToInt32(nosacRow[2] == DBNull.Value ? 0 : nosacRow[2]);
                    //pridobim povratno pijaco ki se nese nazaj v komoro in se ni prodala, torej se mora dodati dodatnemu minusu ki je ze oziroma zmanjsa tisti plus
                    int povratnaPijaca = Convert.ToInt32(nosacRow[4] == DBNull.Value ? 0 : nosacRow[4]);

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

                    //Primerjava suminarih vrednosti
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
                    int razlika = Convert.ToInt32(diffRow[3]) - povratnaPijaca;

                    var KoncniZnesek = razlika * cenaPijace;

                    diffRow[4] = razlika;
                    diffRow[5] = KoncniZnesek;


                    //Dodajanje izpolnjene vrstice v novo generirano tabelo
                    diffTable.Rows.Add(diffRow);
                }

                // Set diffTable as the ItemsSource of dataTable3
                dataTable3.ItemsSource = diffTable.DefaultView;
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
                        var writer = new PdfWriter(fs);
                        var pdf = new PdfDocument(writer);
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

                        iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("Porocilo o prodani pijaci")
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

                        iText.Layout.Element.Paragraph datumNastankaParagraph = new iText.Layout.Element.Paragraph("Datum nastanka porocila: " + datumNastanka.ToString());
                        document.Add(datumNastankaParagraph);

                        iText.Layout.Element.Paragraph imeNosacaParagraph = new iText.Layout.Element.Paragraph("Ime nosasa: " + imeNosaca);
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
