using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                new Pijaca("Pepsi Cola 0,1L", 1.00, 0.1),
                new Pijaca("Pepsi Cola 0,5L", 3.00, 0.5),
                new Pijaca("Ora 0,1L", 1.00, 0.1),
                new Pijaca("Ora 0,5L", 3.00, 0.5),
                new Pijaca("Juice 0,1L", 1.00, 0.1),
                new Pijaca("Ledeni čaj 0,5L", 3.00, 0.5),
                new Pijaca("Voda z okusom 0,5L", 3.00, 0.5),
                new Pijaca("Voda brez okusa 0,5L", 2.50, 0.5),
                new Pijaca("Radenska 0,1L", 0.50, 0.1),
                new Pijaca("Radenska 0,5L", 2.00, 0.5),
                new Pijaca("Energijska pijača (Red Bull) 0,25L", 3.50, 0.25),
                new Pijaca("Tonic", 1.00, 0.1),
                //Alhoholne pijače
                new Pijaca("Pivo 0,33L", 3.00, 0.33),
                new Pijaca("Pivo 0,5L", 3.50, 0.5),
                new Pijaca("Vino Belo 0,1L", 1.40, 0.1),
                new Pijaca("Vino Belo 1L", 14.00, 1),
                new Pijaca("Vino Rdeče 0,1L", 1.60, 0.1),
                new Pijaca("Vino Rdeče 1L", 16.00, 1),
                new Pijaca("Špricar 0,2L", 2.00, 0.2),
                new Pijaca("Ora Špricar 0,2L", 2.50, 0.2),
                new Pijaca("Miš Maš 0,2L", 2.50,0.2),
                new Pijaca("Bambus", 2.50, 0.2),
                //Zgane Shotters
                new Pijaca("Vodka 0,03L", 3.50,0.03),
                new Pijaca("Jagermeister 0,03L", 4.00, 0.03),
                new Pijaca("Jack Daniels 0,03L", 4.00, 0.03),
                new Pijaca("Borovničevec 0,03L", 3.00, 0.03),
                new Pijaca("Gin 0,03L", 3.50, 0.03),
                //Zgano mesano
                new Pijaca("Vodka cola", 4.00, 2),
                new Pijaca("Juice vodka", 4.00,2),
                new Pijaca("Jager cola", 4.50,2),
                new Pijaca("Jack cola", 4.50, 2),
                new Pijaca("Gin tonic", 4.00, 2),
                new Pijaca("Party Vodka (2xVodka0,33L+1xRB", 9.00, 2),
                //Boats
                new Pijaca("Vodka RB Boat(1xVodka+6xRedbull)", 95.00,0),
                new Pijaca("Vodka CO Boat(1xVodka+1,5L Pepsi Cola)", 85.00,0),
                new Pijaca("Vodka Juice Boat(1xVodka+1L Juice)", 85.00, 0),
                new Pijaca("Jack Boat(1xJack+1,5L Pepsi Cola", 95.00, 0),
                new Pijaca("Jager Boat(1xJager+1,5L Pepsi Cola)", 95.00, 0),
                new Pijaca("Gin Boat(1xGin+1,5L Tonic)", 95.00, 0),
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

        public static bool ValidateDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 2; i < 5; i++) // Columns 3, 4, and 5
                {
                    if (!IsPositiveNumberWithCommaOrPeriod(row[i].ToString().Trim()))
                    {
                        MessageBox.Show($"Napaka! Napaka pri vrstici {dataTable.Rows.IndexOf(row) + 1}, stolpcu {i + 1}");
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ValidateDataTableBlagajna(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                // Check only the second column (index 1)
                if (!IsPositiveNumberWithCommaOrPeriod(row[1].ToString().Trim()))
                {
                    MessageBox.Show($"Napaka! Napaka v vrstici {dataTable.Rows.IndexOf(row) + 1}, stolpec 2");
                    return false;
                }
            }
            return true;
        }

        static bool IsPositiveNumberWithCommaOrPeriod(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return true; // Allow empty values

            // Splitting by both comma and period, and checking each part
            string[] parts = input.Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                double number;
                if (!double.TryParse(part, out number) || number < 0)
                    return false;
            }
            return true;
        }
    }
}
