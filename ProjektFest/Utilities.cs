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
            //39
            return new List<Pijaca>
            {
                //Brezalkoholne pijače
                new Pijaca("Pepsi Cola 0,1L", 1.00),
                new Pijaca("Pepsi Cola 0,5L", 3.00),
                new Pijaca("Ora 0,1L", 1.00),
                new Pijaca("Ora 0,5L", 3.00),
                new Pijaca("Juice 0,1L", 1.00),
                new Pijaca("Ledeni čaj 0,5L", 3.00),
                new Pijaca("Voda z okusom 0,5L", 3.00),
                new Pijaca("Voda brez okusa 0,5L", 2.50),
                new Pijaca("Radenska 0,1L", 0.50),
                new Pijaca("Radenska 0,5L", 2.00),
                new Pijaca("Energijska pijača (Red Bull) 0,25L", 3.50),
                new Pijaca("Tonic", 1.00),
                //Alhoholne pijače
                new Pijaca("Pivo 0,33L", 3.00),
                new Pijaca("Pivo 0,5L", 3.50),
                new Pijaca("Vino Belo 0,1L", 1.40),
                new Pijaca("Vino Belo 1L", 14.00),
                new Pijaca("Vino Rdeče 0,1L", 1.60),
                new Pijaca("Vino Rdeče 1L", 16.00),
                new Pijaca("Špricar 0,2L", 2.00),
                new Pijaca("Ora Špricar 0,2L", 2.50),
                new Pijaca("Miš Maš 0,2L", 2.50),
                new Pijaca("Bambus", 2.50),
                //Zgane Shotters
                new Pijaca("Vodka 0,03L", 3.50),
                new Pijaca("Jagermeister 0,03L", 4.00),
                new Pijaca("Jack Daniels 0,03L", 4.00),
                new Pijaca("Borovničevec 0,03L", 3.00),
                new Pijaca("Gin 0,03L", 3.50),
                //Zgano mesano
                new Pijaca("Vodka cola", 4.00),
                new Pijaca("Juice vodka", 4.00),
                new Pijaca("Jager cola", 4.50),
                new Pijaca("Jack cola", 4.50),
                new Pijaca("Gin tonic", 4.00),
                new Pijaca("Party Vodka (2xVodka0,33L+1xRB", 9.00),
                //Boats
                new Pijaca("Vodka RB Boat(1xVodka+6xRedbull)", 95.00),
                new Pijaca("Vodka CO Boat(1xVodka+1,5L Pepsi Cola)", 85.00),
                new Pijaca("Vodka Juice Boat(1xVodka+1L Juice)", 85.00),
                new Pijaca("Jack Boat(1xJack+1,5L Pepsi Cola", 95.00),
                new Pijaca("Jager Boat(1xJager+1,5L Pepsi Cola)", 95.00),
                new Pijaca("Gin Boat(1xGin+1,5L Tonic)", 95.00),
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
