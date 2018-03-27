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
using System.Text.RegularExpressions;

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
            var SarnaneNimi = Poodlist.FirstOrDefault(x => x.Nimi.Contains(Nimetus.Text));
            if (Convert.ToDouble(Hind.Text) > 0 && SarnaneNimi == null)
            {
                //Pood_Listview.Items.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                Poodlist.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });
                Pood_Listview.ItemsSource = null;
                Pood_Listview.ItemsSource = Poodlist;
            }
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            //Ostukorv_Listview.Items.Remove(Pood_Listview.SelectedItem);
            //Poodlist.RemoveAt(Pood_Listview.SelectedIndex);
        }

        private void Osta_Click(object sender, RoutedEventArgs e)
        {
            var tšekk = new Tšekk();
            tšekk.Print(OstukorvList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Pood_Listview.SelectedIndex > -1)
            {
                var matches = OstukorvList.Where(p => String.Equals(p.Nimi, Poodlist[Pood_Listview.SelectedIndex].Nimi, StringComparison.CurrentCulture));

                if (matches.Any())
                {
                    foreach (var item in matches)
                    {
                        item.Kogus += 1;
                    }
                    Ostukorv_Listview.ItemsSource = null;
                    Ostukorv_Listview.ItemsSource = OstukorvList;
                }
                else
                {
                    OstukorvList.Add(Poodlist[Pood_Listview.SelectedIndex]);

                    OstukorvList[OstukorvList.Count - 1].Kogus = int.Parse(Kogus.Text);
                    Ostukorv_Listview.ItemsSource = null;
                    Ostukorv_Listview.ItemsSource = OstukorvList;
                } 
            }
        }

        //Algab StackOverflow Copy-Paste https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void Hind_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Kogus_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //Lõppeb StackOverflow Copy-Paste
    }
    public class Ostukorv
    {
        public string Nimi { get; set; }
        public int Kogus { get; set; }
        public double Hind { get; set; }
    }
}
