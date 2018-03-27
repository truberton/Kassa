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
        public List<Ostukorv> OstukorvList { get; private set; }
        public List<Ostukorv> Poodlist { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            OstukorvList = new List<Ostukorv>();
            Poodlist = new List<Ostukorv>();
            Pood_Listview.ItemsSource = Poodlist;
            Ostukorv_Listview.ItemsSource = OstukorvList;
        }

        private void todoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLisa_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(Hind.Text) > 0)
            {
                var matches = Poodlist.Where(p => String.Equals(p.Nimi, Nimetus.Text, StringComparison.CurrentCulture));

                if (matches.Any())
                {

                }
                else
                {
                    //Pood_Listview.Items.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                    Poodlist.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                    Pood_Listview.ItemsSource = null;
                    Pood_Listview.ItemsSource = Poodlist;
                }
            }
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            //Ostukorv_Listview.Items.Remove(Pood_Listview.SelectedItem);
            //Poodlist.RemoveAt(Pood_Listview.SelectedIndex);
        }

        private void Osta_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Ostukorv_Listview.Items)
            {

            }
            var tšekk = new Tšekk();
            tšekk.Print(OstukorvList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Kogus.Text))
            {
                OstukorvList.Add(Poodlist[Pood_Listview.SelectedIndex]);

                OstukorvList[OstukorvList.Count - 1].Kogus = int.Parse(Kogus.Text);
                Ostukorv_Listview.ItemsSource = null;
                Ostukorv_Listview.ItemsSource = OstukorvList;
            }
        }

    }
    public class Ostukorv
    {
        public string Nimi { get; set; }
        public int Kogus { get; set; }
        public double Hind { get; set; }
    }
}
