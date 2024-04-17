using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

            string json = "";

            //1. najprej pridobi podatke iz tistega jsona
            List<Pijaca> seznam_pijace = Utilities.StalnaPijaca();
            //2.Pridobi lokacijo bin/debug računalnika
            string filepath = AppDomain.CurrentDomain.BaseDirectory.ToString()+"seznam_pijac.json";
            //3. Preveri če obstaja datoteka seznam_pijace.json
            if (File.Exists(filepath))
            {
                //Datoteka že obstaja, samo preberemo jo
                json = File.ReadAllText("seznam_pijac.json");
                seznam_pijac = JsonSerializer.Deserialize<List<Pijaca>>(json);
            }
            else
            {
                //Datoteka ne obstaja, ustvarimo datoteko na tej lokaciji in jo napolnimo s podatki iz list<pijaca>, nato jo preberemo
                json = JsonSerializer.Serialize(seznam_pijace);
                try
                {
                    File.WriteAllText(filepath, json);
                    json = File.ReadAllText(filepath);
                    seznam_pijace = JsonSerializer.Deserialize<List<Pijaca>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Pri branju datoteke seznam_pijace.json je prišlo do napake: {ex.Message}");
                }
            }
        }
    }
}
