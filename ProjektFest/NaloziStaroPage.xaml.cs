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

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for NaloziStaroPage.xaml
    /// </summary>
    public partial class NaloziStaroPage : Page
    {
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
            dataTable1Staro.ItemsSource = sos.Komora.DefaultView;
            dataTable2Staro.ItemsSource = sos.NosacDataTable.DefaultView;
            dataTable3Staro.ItemsSource = sos.Rezultat.DefaultView;

        }

        private void PrimerjajStaro_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Primerjaj Staro");
        }

        private void PorociloButtonStaro_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Porocilo");
        }

        private void ShraniPodatkeStaro_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Shrani podatke");
        }

        private void BlagajnaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sosKopija != null)
                {
                    NaloziStaroBlagajnaPrimerjava nsp = new NaloziStaroBlagajnaPrimerjava(this.sosKopija,mainWindow1);
                    mainWindow1.Main.Content = nsp;
                }
            }
            catch (Exception ew)
            {

                throw new Exception("Neki steka" + ew);
            }
        }
    }
}
