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

namespace Kassa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Ostukorv> list { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            list = new List<Ostukorv>();
        }

        private void todoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLisa_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(Hind.Text) > 0)
            {
                var matches = list.Where(p => String.Equals(p.Nimi, Nimetus.Text, StringComparison.CurrentCulture));

                if (matches.Any())
                {
                    foreach (var item in list)
                    {
                        if (item.Nimi == Nimetus.Text)
                        {
                            item.Kogus += 1;
                        }
                    }
                    Ostukorv_Listview.Items.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                }
                else
                {
                    Ostukorv_Listview.Items.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                    list.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) }); 
                }
            }
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            Ostukorv_Listview.Items.Remove(Ostukorv_Listview.SelectedItem);
        }

        private void Osta_Click(object sender, RoutedEventArgs e)
        {
            var tšekk = new Tšekk();
            tšekk.Print(list);
        }
    }

    public class Ostukorv
    {
        public string Nimi { get; set; }
        public int Kogus { get; set; }
        public double Hind { get; set; }
    }
}
