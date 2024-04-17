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
using Newtonsoft.Json;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        MainWindow mainwindow;

        public StartPage()
        {
            InitializeComponent();
        }

        public StartPage(MainWindow mw)
        {
            InitializeComponent();
            mainwindow = mw;
        }

        private void NalziStaroBtn_Click(object sender, RoutedEventArgs e)
        {
            PregledFolderaPreteklePrireditve nsp = new PregledFolderaPreteklePrireditve(mainwindow);
            mainwindow.Main.Content = nsp;
        }

        private void UstvariNovoBtn_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.Main.Content = new NovaPrireditevPage(mainwindow);
        }
    }
}
