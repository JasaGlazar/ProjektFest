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
    /// Interaction logic for SankiPage.xaml
    /// </summary>
    public partial class SankiPage : Page
    {
        MainWindow mainwindow { get; set; }
        int stevilo_sankov { get; set; }

        public SankiPage(int stevilo_sankov, MainWindow mw)
        {
            InitializeComponent();
            mainwindow = mw;
            this.stevilo_sankov = stevilo_sankov;
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
    }
}
