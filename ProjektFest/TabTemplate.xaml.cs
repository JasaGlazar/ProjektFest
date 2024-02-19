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
    }
}
