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
    /// Interaction logic for TabTemplate.xaml
    /// </summary>
    public partial class TabTemplate : UserControl
    {
        MainWindow mainwindow;
        Sank izbrani_sank;
        Dictionary<string,Oseba> seznam_natakarjev = new Dictionary<string,Oseba>();
        Dictionary<string, Oseba> seznam_nosacev = new Dictionary<string, Oseba>();
        public TabTemplate(MainWindow mw,Sank sank)
        {
            this.izbrani_sank= sank;    
            InitializeComponent();
            this.mainwindow = mw;
        }

        private void DodajNatakarjaBtn_Click(object sender, RoutedEventArgs e)
        {
            Oseba s = new Oseba(ImeInput.Text, PriimekInput.Text);
            string key = String.Format("{0} {1}", s.ime, s.priimek);
            if (!seznam_natakarjev.ContainsKey(key))
            {
                natakarji_listbox.Items.Add(key);
                seznam_natakarjev.Add(key, s);
                izbrani_sank.natakarji.Add(s);
                ImeInput.Clear();
                PriimekInput.Clear();
            } else
            {
                MessageBox.Show("Natakar že obstaja");
            }
        }

        private void izbrisiIzbranoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (natakarji_listbox.SelectedItem != null)
            {
                Oseba s = seznam_natakarjev[natakarji_listbox.SelectedItem.ToString()];
                seznam_natakarjev.Remove(natakarji_listbox.SelectedItem.ToString());
                natakarji_listbox.Items.Remove(natakarji_listbox.SelectedItem);
                izbrani_sank.natakarji.Remove(s);
            }
        }

        private void DodajNosacaBtn_Click(object sender, RoutedEventArgs e)
        {
            Oseba o = new Oseba(ImeInputNosac.Text, PriimekInputNosac.Text);
            string key = String.Format("{0} {1}", o.ime, o.priimek);
            if (seznam_nosacev.Count == 0)
            {
                nosaci_listbox.Items.Add(key);
                seznam_nosacev.Add(key, o);
                izbrani_sank.nosac = o;
                ImeInputNosac.Clear();
                PriimekInputNosac.Clear();
            }
            else
            {
                MessageBox.Show("Seznam lahko vsebuje samo enega nosača");
            }
        }

        private void izbrisiIzbranoBtnNOsac_Click(object sender, RoutedEventArgs e)
        {
            if (nosaci_listbox.SelectedItem != null)
            {
                Oseba s = seznam_nosacev[nosaci_listbox.SelectedItem.ToString()];
                seznam_nosacev.Remove(nosaci_listbox.SelectedItem.ToString());
                nosaci_listbox.Items.Remove(nosaci_listbox.SelectedItem);
                izbrani_sank.natakarji.Remove(s);
            }
        }
    }
}
