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
            Tekst.Add("Nimetus | Kogus | Hind | Summa");
            foreach (var item in list)
            {
                string NimeTühik = new string(' ', 12 - item.Nimi.Length);
                string KoguseTühik = new string(' ', 7 - Convert.ToString(item.Kogus).Length);
                string HinnaTühik = new string(' ', 7 - Convert.ToString(item.Hind).Length);

                //Tegin nii, et tekst ja numbrid jääksid nii ja naa samale joonele
                Tekst.Add(string.Format("{0}" + NimeTühik + "{1}" + KoguseTühik + "{2}" + HinnaTühik + "{3}", item.Nimi, Convert.ToString(item.Kogus), item.Hind, item.Hind * item.Kogus));
                kokku += item.Hind * item.Kogus;
            }
            File.WriteAllLines("Tšekk.txt", Tekst);
            File.AppendAllText("Tšekk.txt", "-------------------------" + Environment.NewLine + "Kokku: €" + kokku);

            Process.Start("Tšekk.txt");
        }
    }
}
