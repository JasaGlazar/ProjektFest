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
    /// Interaction logic for NaloziStaroBlagajnaPrimerjava.xaml
    /// </summary>
    public partial class NaloziStaroBlagajnaPrimerjava : Page
    {
        ShraniObjektSank sosKopija {  get; set; }
        MainWindow mainWindowKopija { get; set; }


        public NaloziStaroBlagajnaPrimerjava(ShraniObjektSank sos, MainWindow mainWindow)
        {
            InitializeComponent();
            this.sosKopija = sos;
            this.mainWindowKopija = mainWindow;


            dataTable1BlagajnaStaro.ItemsSource = sos.KomoraSumirane.DefaultView;
            dataTable1BlagajnaStaro.IsReadOnly = true;
            dataTable2BlagajnaStaro.ItemsSource = sos.Blagajna.DefaultView;
            dataTable3BlagajnaStaro.ItemsSource = sos.RazlikaBlagajnaKomora.DefaultView;
            dataTable3BlagajnaStaro.IsReadOnly = true;
        }

        private void PrimerjajStaroButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Primerjaj staro!");
        }

        private void PorociloStaroButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Porocilo Staro");
        }

        private void ShraniStaroPodatke_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Shrani staro");
        }
    }
}
