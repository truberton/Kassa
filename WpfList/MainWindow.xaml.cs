using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                Poodlist.Add(new Ostukorv { Nimi = Nimetus.Text, Kogus = int.Parse(Kogus.Text), Hind = Convert.ToDouble(Hind.Text) });

                Pood_Listview.ItemsSource = null;
                Pood_Listview.ItemsSource = Poodlist;
            }

            Nimetus.Text = "";
            Hind.Text = "";
        }

        private void Osta_Click(object sender, RoutedEventArgs e)
        {
            var tšekk = new Tšekk();
            tšekk.Print(OstukorvList);
        }

        private void LisaOstukorvi_Click(object sender, RoutedEventArgs e)
        {
            if (Pood_Listview.SelectedIndex > -1)
            {
                //Vaatab kas ostukorvis on juba olemas toode
                var matches = OstukorvList.Where(p => String.Equals(p.Nimi, Poodlist[Pood_Listview.SelectedIndex].Nimi, StringComparison.CurrentCulture));

                if (matches.Any())
                {
                    foreach (var item in matches)
                    {
                        item.Kogus += int.Parse(Kogus.Text);
                    }
                    Ostukorv_Listview.ItemsSource = null;
                    Ostukorv_Listview.ItemsSource = OstukorvList;
                }
                else
                {
                    OstukorvList.Add(Poodlist[Pood_Listview.SelectedIndex]);

                    OstukorvList[OstukorvList.Count - 1].Kogus = int.Parse(Kogus.Text);

                    //itemssource kirjutan nii palju igal pool, sest listview ei uuenda muidu, kui muuta listi
                    Ostukorv_Listview.ItemsSource = null;
                    Ostukorv_Listview.ItemsSource = OstukorvList;
                }
            }
            else
            {
                MessageBox.Show("Palun vali midagi");
            }
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            int index = Ostukorv_Listview.SelectedIndex;
            if (index > -1)
            {
                OstukorvList.RemoveAt(index);

                Ostukorv_Listview.ItemsSource = null;
                Ostukorv_Listview.ItemsSource = OstukorvList;
            }
            else
            {
                MessageBox.Show("Palun vali midagi");
            }
        }

        //Algab StackOverflow Copy-Paste https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void Hind_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
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
