using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Kassa
{
    class Tšekk
    {
        public void Print(List<Ostukorv> list)
        {
            List<string> Tekst = new List<string>();
            double kokku = 0;
            foreach (var item in list)
            {
                Tekst.Add(string.Format("{0}x {1} - €{2}", Convert.ToString(item.Kogus), item.Nimi, item.Hind));
                kokku += item.Hind * item.Kogus;
            }
            File.WriteAllLines("Tšekk.txt", Tekst);
            File.AppendAllText("Tšekk.txt", "-------------------------" + Environment.NewLine + "Kokku: €" + kokku);

            Process.Start("Tšekk.txt");
        }
    }
}
