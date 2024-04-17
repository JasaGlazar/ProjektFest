using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Pijaca> seznam_pijac;
        //public List<Pijaca> seznam_pijac = Utilities.StalnaPijaca();
        public Prireditev prireditev = new Prireditev();
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StartPage(this);
            string json = File.ReadAllText("seznam_pijac.json");
            seznam_pijac = JsonSerializer.Deserialize<List<Pijaca>>(json);
        }
    }
}
