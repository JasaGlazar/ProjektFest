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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for SankiPage.xaml
    /// </summary>
    public partial class SankiPage : Page
    {
        MainWindow mainwindow { get; set; }

        public SankiPage(int stevilo_sankov, MainWindow mw)
        {
            InitializeComponent();
            mainwindow = mw;
            for(int i = 0; i< stevilo_sankov; i++)
            {
                TabItem ti = new TabItem();
                string ime = "Šank " + (i + 1);
                ti.Header = ime;
                Sank sank = new Sank(ime);
                mainwindow.prireditev.sanki.Add(sank);
                ti.Content = new TabTemplate(mainwindow,sank);
                this.sankiTabControl.Items.Add(ti);
            }
        }

        //to je samo metoda da sem prevero če se vsi podatki vredu shranijo
        private void PotrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Prireditev));

                string potDoMape = Utilities.PridobiMapoTrenutnePrireditve(mainwindow.prireditev.ime_prireditve, mainwindow.prireditev.leto_prireditve);

                string celotnaPot = System.IO.Path.Combine(potDoMape, "serialized.xml");

                using (FileStream fs = new FileStream(celotnaPot, FileMode.Create))
                {
                    serializer.Serialize(fs, mainwindow.prireditev);
                }

                // Create the new page instance
                VnosPodatkovPijace newPage = new VnosPodatkovPijace(mainwindow.prireditev.seznam_pijace);

                mainwindow.Main.Content = newPage;
                
                Window thiswindow = Window.GetWindow(this);
                if (thiswindow != null)
                {
                    thiswindow.WindowState = WindowState.Maximized;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
