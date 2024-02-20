using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFest
{
    internal class Utilities
    {
        //Utilites class za podporne metode...
        private Utilities()
        {
        }

        //Še treba dodat, niso še vse pijače not
        public static List<Pijaca> StalnaPijaca()
        {
            return new List<Pijaca>
            {
                new Pijaca("Pepsi Cola 0,1L", 1.00),
                new Pijaca("Pepsi Cola 0,5L", 3.00),
                new Pijaca("Ora 0,1L", 1.00),
                new Pijaca("Ora 0,5L", 3.00),
                new Pijaca("Ledeni čaj 0,5L", 3.00),
                new Pijaca("Voda z okusom 0,5L", 3.00),
                new Pijaca("Voda brez okusa 0,5L", 2.50),
                new Pijaca("Radenska 0,1L", 0.50),
                new Pijaca("Radenska 0,5L", 2.00),
                new Pijaca("Energijska pijača (Red Bull) 0,25L", 3.50),
                new Pijaca("Vino Belo 1L", 14.00),
                new Pijaca("Vino Rdeče 1L", 16.00)
            };
        }

        //Nevem če sta tak misla da bi sproti shranjevali vse datoteke povezane z eno prireditvijo, naredi se folder FestPrireditve not pa folder "imeprireditve+leto"
        //Shrani pa se v C:\Users\USERNAME\AppData\Local
        //Mogoce bo treba spremenit v SpecialFolder.CommonApplicationData, zaradi pravic za pisanje...
        public static void UstvariMapoZaPrireditev(string ime, string leto)
        {
            string osnovnaPot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string prireditevFolderPot = System.IO.Path.Combine(osnovnaPot, "FestPrireditve", ime + leto);

            if (!Directory.Exists(prireditevFolderPot))
            {
                Directory.CreateDirectory(prireditevFolderPot);
            }
        }

        public static string PridobiMapoTrenutnePrireditve(string ime, string leto)
        {
            string osnovnaPot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string prireditevFolderPot = System.IO.Path.Combine(osnovnaPot, "FestPrireditve", ime + leto);

            return prireditevFolderPot;

        }
    }
}
