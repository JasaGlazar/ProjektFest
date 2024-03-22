using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ProjektFest
{
    /// <summary>
    /// Interaction logic for NovaPrireditevPage.xaml
    /// </summary>
    public partial class NovaPrireditevPage : Page
    {
        MainWindow mainwindow { get; set; }
        Dictionary<string, Pijaca> slovar_pijac = new Dictionary<string, Pijaca>();
        string izbran_key;
        //naredo sem dictionary da mam key value paire da lahko na podlagi stringa iz listboxa brišem pijace znotraj seznama_pijac iz mainwindowa
        public NovaPrireditevPage(MainWindow mw)
        {
            InitializeComponent();
            mainwindow = mw;
            napolni_listbox();
        }

        private void napolni_listbox()
        {
            foreach(Pijaca p in mainwindow.seznam_pijac)
            {
                string key = String.Format("{0} | {1}€", p.ime, p.cena);
                SeznamPijacListBox.Items.Add(key);
                slovar_pijac.Add(key, p);
            }
        }

        private void DodajPijacoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cena_input = CenaPijaceINput.Text;
                //cena_input = cena_input.Replace(',', '.');
                Pijaca p = new Pijaca(ImePijaceInput.Text.ToString(), Convert.ToDouble(cena_input));
                string key = String.Format("{0} | {1}€", p.ime, p.cena);
                if (!slovar_pijac.ContainsKey(key))
                {
                    mainwindow.seznam_pijac.Add(p);
                    SeznamPijacListBox.Items.Add(key);
                    slovar_pijac.Add(key, p);
                }
                else
                {
                    MessageBox.Show("Produkt z istim imenom že obstaja!");
                }
            } catch (FormatException)
            {
                MessageBox.Show("Naprevilen vnos pijače!");
            }

        }


        private void naslednjiKorakBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainwindow.prireditev.ime_prireditve = ImePrireditveInput.Text;
                mainwindow.prireditev.leto_prireditve = LetoPrireditveInput.Text;

                Utilities.UstvariMapoZaPrireditev(mainwindow.prireditev.ime_prireditve, mainwindow.prireditev.leto_prireditve);

                foreach(Pijaca p in mainwindow.seznam_pijac)
                {
                    mainwindow.prireditev.seznam_pijace.Add(p);
                }

                mainwindow.Main.Content = new SankiPage(Convert.ToInt32(StSankovInput.Text), mainwindow);
            } catch(System.FormatException)
            {
                MessageBox.Show("Naprevilen vnos števila šankov");
            }
        }

        private void PotrdiSpremembeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (izbran_key != null)
            {
                Pijaca pijaca_znotraj_seznama = mainwindow.seznam_pijac.FirstOrDefault(p => p.Equals(slovar_pijac[izbran_key]));
                if (pijaca_znotraj_seznama != null)
                {
                    pijaca_znotraj_seznama.ime = ImePijaceInput.Text;
                    try
                    {
                        string nova_cena = CenaPijaceINput.Text;
                        if (CenaPijaceINput.Text.Contains(','))
                        {
                            nova_cena = CenaPijaceINput.Text.Replace(',','.');
                        } 
                        pijaca_znotraj_seznama.cena = Convert.ToDouble(nova_cena);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Napačni vnos cene!");
                        return;
                    }
                    slovar_pijac.Clear();
                    SeznamPijacListBox.Items.Clear();
                    napolni_listbox();

                    string json = JsonSerializer.Serialize(mainwindow.seznam_pijac);
                    File.WriteAllText("seznam_pijac.json", json);
                }
                ImePijaceInput.Text = string.Empty;
                CenaPijaceINput.Text = string.Empty;
            }



        }

        private void SeznamPijacListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            izbran_key = SeznamPijacListBox.SelectedItem as string;         
            if (izbran_key != null)
            {
                Pijaca izbrana_pijaca = slovar_pijac[izbran_key];
                ImePijaceInput.Text = izbrana_pijaca.ime;
                CenaPijaceINput.Text = izbrana_pijaca.cena.ToString();
            }
            
        }
    }
}
