using iText.IO.Util;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Element;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using iText.Layout;
using Org.BouncyCastle.Tls;

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
        public static DataTable UstvariKomoraSumiranaDataTable(DataTable komora)
        {

            DataTable newDataTable = new DataTable();

            newDataTable.Columns.Add("Pijaca", typeof(string));
            newDataTable.Columns.Add("Prodana kolicina", typeof(decimal));

            for (int i = 0; i < komora.Rows.Count; i++)
            {
                DataRow komoraRow = komora.Rows[i];
                DataRow DiffRow = newDataTable.NewRow();

                DiffRow[0] = komoraRow[0];

                double komoraZac = komoraRow[2] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[2].ToString()) ? 0 : Convert.ToDouble(komoraRow[2]);

                string[] komoraValues = komoraRow[3].ToString().Split(',');
                double sumKomora = 0;
                foreach (string value in komoraValues)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        // Parse each value using TryParse to handle decimal numbers correctly
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                            sumKomora += parsedValue;
                    }
                }

                double komoraKoncna = komoraRow[4] == DBNull.Value || string.IsNullOrWhiteSpace(komoraRow[4].ToString()) ? 0 : double.TryParse(komoraRow[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result) ? result : 0;

                double ProdanaKolicinaPijace = (komoraZac + sumKomora) - komoraKoncna;

                DiffRow[1] = ProdanaKolicinaPijace;

                newDataTable.Rows.Add(DiffRow);

            }

            return newDataTable;
        }
        public static DataTable UstvariBlagajnoZaVstavljanje()
        {
            DataTable newDataTable = new DataTable();
            List<Pijaca> PijacaCenik = Utilities.StalnaPijaca();

            newDataTable.Columns.Add("Ime Pijace");
            newDataTable.Columns.Add("Kolicina");

            for (int i = 0; i < PijacaCenik.Count; i++)
            {
                DataRow newRow = newDataTable.NewRow();

                newRow[0] = PijacaCenik[i].ime;
                newRow[1] = 0;

                newDataTable.Rows.Add(newRow);
            }
            newDataTable.Columns[0].ReadOnly = true;
            return newDataTable;
        }
        public static DataTable VrniPrimerjavoKomoreInBlagajne(DataTable Komora, DataTable Blagajna)
        {
            DataTable NewKomoraDataTable = Komora;

            DataRow row0 = NewKomoraDataTable.Rows[0];
            DataRow row1 = NewKomoraDataTable.Rows[1];
            DataRow row2 = NewKomoraDataTable.Rows[2];
            DataRow row3 = NewKomoraDataTable.Rows[3];
            DataRow row4 = NewKomoraDataTable.Rows[4];
            DataRow row5 = NewKomoraDataTable.Rows[5];
            DataRow row6 = NewKomoraDataTable.Rows[6];
            DataRow row7 = NewKomoraDataTable.Rows[7];
            DataRow row8 = NewKomoraDataTable.Rows[8];
            DataRow row9 = NewKomoraDataTable.Rows[9];
            DataRow row10 = NewKomoraDataTable.Rows[10];
            DataRow row11 = NewKomoraDataTable.Rows[11];
            DataRow row12 = NewKomoraDataTable.Rows[12];
            DataRow row13 = NewKomoraDataTable.Rows[13];
            DataRow row14 = NewKomoraDataTable.Rows[14];
            DataRow row15 = NewKomoraDataTable.Rows[15];
            DataRow row16 = NewKomoraDataTable.Rows[16];
            DataRow row17 = NewKomoraDataTable.Rows[17];
            DataRow row18 = NewKomoraDataTable.Rows[18];
            DataRow row19 = NewKomoraDataTable.Rows[19];
            DataRow row20 = NewKomoraDataTable.Rows[20];

            //Pretvorba v enote za primerjavo
            double KomoraCola = (Convert.ToDouble(row0[1]) * 15) + (Convert.ToDouble(row1[1]) * 5);
            double KomoraSchweps = (Convert.ToDouble(row2[1]) * 15);
            double KomoraOra = (Convert.ToDouble(row3[1]) * 15) + (Convert.ToDouble(row4[1]) * 5);
            double KomoraLedeniCaj = (Convert.ToDouble(row5[1]) * 5);
            double KomoraVodaOkus = (Convert.ToDouble(row6[1]) * 5);
            double KomoraVodaBrez = (Convert.ToDouble(row7[1]) * 5);
            double KomoraJuice = (Convert.ToDouble(row8[1]) * 10);
            double KomoraRadenska = (Convert.ToDouble(row9[1]) * 5);
            double KomoraEnergy = (Convert.ToDouble(row10[1]) * 2.5);
            double KomoraLasko = (Convert.ToDouble(row11[1]) * 1);
            double KomoraUnion = (Convert.ToDouble(row12[1]) * 1);
            double KomoraUniBrez = (Convert.ToDouble(row13[1]) * 1);
            double KomoraVinoBelo = (Convert.ToDouble(row14[1]) * 10);
            double KomoraVinoRdeče = (Convert.ToDouble(row15[1]) * 10);
            double KomoraVodka = (Convert.ToDouble(row16[1]) * 33);
            double KomoraBorovnicke = (Convert.ToDouble(row17[1]) * 33);
            double KomoraJagermaiset = (Convert.ToDouble(row18[1]) * 33);
            double KomoraJackDaniels = (Convert.ToDouble(row19[1]) * 33);
            double KomoraGin = (Convert.ToDouble(row20[1]) * 33);

            //Enote zaj mam semizdi, zaj bi pa mogo simulirat cenik pa ga vkup dat pm...

            DataTable newBlagajnaDataTable = Blagajna;

            if (Utilities.ValidateDataTableBlagajna(newBlagajnaDataTable))
            {
                DataRow Brow0 = newBlagajnaDataTable.Rows[0];
                DataRow Brow1 = newBlagajnaDataTable.Rows[1];
                DataRow Brow2 = newBlagajnaDataTable.Rows[2];
                DataRow Brow3 = newBlagajnaDataTable.Rows[3];
                DataRow Brow4 = newBlagajnaDataTable.Rows[4];
                DataRow Brow5 = newBlagajnaDataTable.Rows[5];
                DataRow Brow6 = newBlagajnaDataTable.Rows[6];
                DataRow Brow7 = newBlagajnaDataTable.Rows[7];
                DataRow Brow8 = newBlagajnaDataTable.Rows[8];
                DataRow Brow9 = newBlagajnaDataTable.Rows[9];
                DataRow Brow10 = newBlagajnaDataTable.Rows[10];
                DataRow Brow11 = newBlagajnaDataTable.Rows[11];
                DataRow Brow12 = newBlagajnaDataTable.Rows[12];
                DataRow Brow13 = newBlagajnaDataTable.Rows[13];
                DataRow Brow14 = newBlagajnaDataTable.Rows[14];
                DataRow Brow15 = newBlagajnaDataTable.Rows[15];
                DataRow Brow16 = newBlagajnaDataTable.Rows[16];
                DataRow Brow17 = newBlagajnaDataTable.Rows[17];
                DataRow Brow18 = newBlagajnaDataTable.Rows[18];
                DataRow Brow19 = newBlagajnaDataTable.Rows[19];
                DataRow Brow20 = newBlagajnaDataTable.Rows[20];
                DataRow Brow21 = newBlagajnaDataTable.Rows[21];
                DataRow Brow22 = newBlagajnaDataTable.Rows[22];
                DataRow Brow23 = newBlagajnaDataTable.Rows[23];
                DataRow Brow24 = newBlagajnaDataTable.Rows[24];
                DataRow Brow25 = newBlagajnaDataTable.Rows[25];
                DataRow Brow26 = newBlagajnaDataTable.Rows[26];
                DataRow Brow27 = newBlagajnaDataTable.Rows[27];
                DataRow Brow28 = newBlagajnaDataTable.Rows[28];
                DataRow Brow29 = newBlagajnaDataTable.Rows[29];
                DataRow Brow30 = newBlagajnaDataTable.Rows[30];
                DataRow Brow31 = newBlagajnaDataTable.Rows[31];
                DataRow Brow32 = newBlagajnaDataTable.Rows[32];
                DataRow Brow33 = newBlagajnaDataTable.Rows[33];
                DataRow Brow34 = newBlagajnaDataTable.Rows[34];
                DataRow Brow35 = newBlagajnaDataTable.Rows[35];
                DataRow Brow36 = newBlagajnaDataTable.Rows[36];
                DataRow Brow37 = newBlagajnaDataTable.Rows[37];
                DataRow Brow38 = newBlagajnaDataTable.Rows[38];

                double BlagajnaCola = (Convert.ToDouble(Brow0[1]) * 1) + (Convert.ToDouble(Brow1[1]) * 5) + (Convert.ToDouble(Brow21[1]) * 1) + (Convert.ToDouble(Brow27[1]) * 1) + (Convert.ToDouble(Brow29[1]) * 1) + (Convert.ToDouble(Brow30[1]) * 1) + (Convert.ToDouble(Brow34[1]) * 15) + (Convert.ToDouble(Brow36[1]) * 15) + (Convert.ToDouble(Brow37[1]) * 15);
                double BlagajnaSchweps = (Convert.ToDouble(Brow11[1]) * 1) + (Convert.ToDouble(Brow31[1]) * 1) + (Convert.ToDouble(Brow38[1]) * 15);
                double BlagajnaOra = (Convert.ToDouble(Brow2[1]) * 1) + (Convert.ToDouble(Brow3[1]) * 5) + (Convert.ToDouble(Brow19[1]) * 1) + (Convert.ToDouble(Brow20[1]) * 1) + (Convert.ToDouble(Brow36[1]) * 15);
                double BlagajnaLedeniCaj = (Convert.ToDouble(Brow5[1]) * 5);
                double BlagajnaVodaOkus = (Convert.ToDouble(Brow6[1]) * 5);
                double BlagajnaBrezOkus = (Convert.ToDouble(Brow7[1]) * 5);
                double BlagajnaJuice = (Convert.ToDouble(Brow4[1]) * 1) + (Convert.ToDouble(Brow28[1]) * 1) + (Convert.ToDouble(Brow35[1]) * 10);
                double BlagajnaRadenska = (Convert.ToDouble(Brow8[1]) * 1) + (Convert.ToDouble(Brow9[1]) * 5) + (Convert.ToDouble(Brow18[1]) * 1);
                double BlagajnaEnergijskaPijaca = (Convert.ToDouble(Brow10[1]) * 2.5) + (Convert.ToDouble(Brow32[1]) * 2.5) + (Convert.ToDouble(Brow33[1]) * 15);
                //Pitaj če je v blagajni posebaj lasko in union al je skupaj???
                double BlagajnaLasko = (Convert.ToDouble(Brow12[1]) * 1);
                double BlagajnaUnion = (Convert.ToDouble(Brow12[1]) * 1);
                double BlagajnaUniBrez = (Convert.ToDouble(Brow13[1]) * 1);
                double BlagajnaVinoBelo = (Convert.ToDouble(Brow14[1]) * 1) + (Convert.ToDouble(Brow15[1]) * 10) + (Convert.ToDouble(Brow18[1]) * 1) + (Convert.ToDouble(Brow19[1]) * 1);
                double BlagajnaVinoRdece = (Convert.ToDouble(Brow16[1]) * 1) + (Convert.ToDouble(Brow17[1]) * 10) + (Convert.ToDouble(Brow20[1]) * 1) + (Convert.ToDouble(Brow21[1]) * 1);
                double BlagajnaVodka = (Convert.ToDouble(Brow22[1]) * 1) + (Convert.ToDouble(Brow27[1]) * 1) + (Convert.ToDouble(Brow28[1]) * 1) + (Convert.ToDouble(Brow32[1]) * 2) + (Convert.ToDouble(Brow33[1]) * 33) + (Convert.ToDouble(Brow34[1]) * 33) + (Convert.ToDouble(Brow35[1]) * 33);
                double BlagajnaBorovnicke = (Convert.ToDouble(Brow25[1]) * 1);
                double BlagajnaJagermaister = (Convert.ToDouble(Brow23[1]) * 1) + (Convert.ToDouble(Brow29[1]) * 1) + (Convert.ToDouble(Brow37[1]) * 33);
                double BlagajnaJackDaniels = (Convert.ToDouble(Brow24[1]) * 1) + (Convert.ToDouble(Brow30[1]) * 1) + (Convert.ToDouble(Brow36[1]) * 33);
                double BlagajnaGin = (Convert.ToDouble(Brow26[1]) * 1) + (Convert.ToDouble(Brow31[1]) * 1) + (Convert.ToDouble(Brow38[1]) * 33);

                //Konca primerjava
                // + pomeni da se je izdalo več pijače kot se je prodalo ( neki ne stima )
                // - pomeni da se je prodalo več pijače kot se je prineslo? ( neki ne stima )
                // mislim da bi mogo lih kontra nardit, kao od blagajne odstet da bi vedle če je minus prave i think kao da se je vec prineslo kak prodalo
                double RezultatCola = BlagajnaCola - KomoraCola;
                double RezultatSchweps = BlagajnaSchweps - KomoraSchweps;
                double RezultatOra = BlagajnaOra - KomoraOra;
                double RezultatLedeniCaj = BlagajnaLedeniCaj - KomoraLedeniCaj;
                double RezultatVodaOkus = BlagajnaVodaOkus - KomoraVodaOkus;
                double RezultatVodaBrez = BlagajnaBrezOkus - KomoraVodaBrez;
                double RezultatJuice = BlagajnaJuice - KomoraJuice;
                double RezultatRadenska = BlagajnaRadenska - KomoraRadenska;
                double RezultatEnergijski = BlagajnaEnergijskaPijaca - KomoraEnergy;
                double RezultatLasko = BlagajnaLasko - KomoraLasko;
                double RezultatUnion = BlagajnaUnion - KomoraUnion;
                double RezultatUnibrez = BlagajnaUniBrez - KomoraUniBrez;
                double RezultatVinoBelo = BlagajnaVinoBelo - KomoraVinoBelo;
                double RezultatVinoRdece = BlagajnaVinoRdece - KomoraVinoRdeče;
                double RezultatVodka = BlagajnaVodka - KomoraVodka;
                double RezultatBorovnicke = BlagajnaBorovnicke - KomoraBorovnicke;
                double RezultatJagermeister = BlagajnaJagermaister - KomoraJagermaiset;
                double RezultatJackDaniels = BlagajnaJackDaniels - KomoraJackDaniels;
                double RezultatGin = BlagajnaGin - KomoraGin;

                //TODO ustvari tabelo 3 pa pripiši vrednosti also neke vrednosti bi se dale pomoje zračunat ne :)
                DataTable DiffDT = new DataTable();
                DiffDT.Columns.Add("Pijaca");
                DiffDT.Columns.Add("Vrednost po enotah");

                DiffDT.Rows.Add("Pepsi Cola (enote oziroma 1dcl)", RezultatCola);
                DiffDT.Rows.Add("Schweps (enote oziroma 1dcl", RezultatSchweps);
                DiffDT.Rows.Add("Ora (enote oziroma 1dcl)", RezultatOra);
                DiffDT.Rows.Add("Ledeni Caj (enote oziroma 1dcl)", RezultatLedeniCaj);
                DiffDT.Rows.Add("Voda z okusom (enote oziroma 1dcl)", RezultatVodaOkus);
                DiffDT.Rows.Add("Voda brez okusa (enote oziroma 1dcl)", RezultatVodaBrez);
                DiffDT.Rows.Add("Juice (enote oziroma 1dcl)", RezultatJuice);
                DiffDT.Rows.Add("Radenska (enote oziroma 1dcl)", RezultatRadenska);
                DiffDT.Rows.Add("Energijski (enote oziroma 2.5dcl)", RezultatEnergijski);
                DiffDT.Rows.Add("Pivo Lasko (enote oziroma 1 Pivo)", RezultatLasko);
                DiffDT.Rows.Add("Pivo Union (enote oziroma 1 Pivo)", RezultatUnion);
                DiffDT.Rows.Add("Pivo Brez Alkh. (enote oziroma 1 Pivo)", RezultatUnibrez);
                DiffDT.Rows.Add("Belo vino (enote oziroma 1dcl)", RezultatVinoBelo);
                DiffDT.Rows.Add("Rdeče vino (enote oziroma 1dcl)", RezultatVinoRdece);
                DiffDT.Rows.Add("Vodka (enote oziroma 1l)", RezultatVodka / 33);
                DiffDT.Rows.Add("Borovnicke (enote oziroma 1l)", RezultatBorovnicke / 33);
                DiffDT.Rows.Add("Jagermeister (enote oziroma 1l)", RezultatJagermeister / 33);
                DiffDT.Rows.Add("Jack Daniels (enote oziroma 1l)", RezultatJackDaniels / 33);
                DiffDT.Rows.Add("Gin (enote oziroma 1l)", RezultatGin / 33);

                MessageBox.Show("POMOČ: \n Če je končno število pri 3 tabeli negativno, to pomeni, da se je iz komore odneslo več ven kot pa se je prodalo! \n " +
                    "Če je končno število pri 3 tabeli pozitivno, to pomeni, da se je prodalo več artiklov, kot jih je bilo nesenih iz komore! \n " +
                    "Če je število 0 pomeni, da se je enako število artiklov odneslo iz komore in prodalo.");

                return DiffDT;

            }
            else
            {
                MessageBox.Show("Vneseno more biti število!");
                return null;
            }
        }



        /*
            Metode za VnosPodatkovPijace Page
         */
        public static DataTable ustvariPraznoTabelo(List<Pijaca> pijacaDataTable)
        {

            /// <summary> Ustvari prazno Podatkovno tabelo, s 5 vrsticami, uporablja se za vnos podatkov pijace iz komore in nosaca </summary
            DataTable datatable = new DataTable();
            for (int i = 1; i <= 5; i++)
            {
                datatable.Columns.Add($"Column{i}", typeof(string));
            }
            datatable.Columns[0].ColumnName = "Pijaca";
            datatable.Columns[1].ColumnName = "Kolicina";
            datatable.Columns[2].ColumnName = "Zacetno";
            datatable.Columns[3].ColumnName = "Vmesno";
            datatable.Columns[4].ColumnName = "Koncno";

            // Set the first and second columns as read-only
            datatable.Columns[0].ReadOnly = true;
            datatable.Columns[1].ReadOnly = true;

            for (int i = 0; i < pijacaDataTable.Count; i++)
            {
                DataRow row = datatable.NewRow();
                row[0] = pijacaDataTable[i].ime;
                row[1] = "Kol.";

                datatable.Rows.Add(row);
            }
            return datatable;
        }


        /*
         Metode za generiranje PDF poročila 
        */

        public static void UstvariPDF(DataTable komora, DataTable nosac, DataTable rezultat, DataTable blagajna, DataTable primerjavaKomoraBlagajna,
                                      string naziv, string nosacime, string sank, List<Oseba> natakarji)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF document (*.pdf)|*.pdf";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                DataTable Komora = komora;
                DataTable Nosac = nosac;
                DataTable Rezultat = rezultat;
                DataTable Blagajna = blagajna;
                DataTable KomoraBlagajnaPrimerjava = primerjavaKomoraBlagajna;

                string nazivPrireditve = naziv;


                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        var writer = new iText.Kernel.Pdf.PdfWriter(fs);
                        var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                        var document = new Document(pdf);

                        // string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        // string relativeFontPath = System.IO.Path.Combine("Resources", "Roboto-Medium.ttf");
                        // string fullFontPath = System.IO.Path.Combine (baseDirectory, relativeFontPath);

                        // PdfFont font = PdfFontFactory.CreateFont(fullFontPath, PdfEncodings.IDENTITY_H);

                        // document.SetFont(font);

                        //string imeSanka = (string)ImeSanka.Content;
                        //List<Oseba> seznamNatakarjev = (List<Oseba>)KelnarjiListView.ItemsSource;
                        string imeNosaca = nosacime;
                        string imeSanka = sank;
                        List<Oseba> seznamNatakarjev = natakarji;
                        DateTime datumNastanka = DateTime.Now;

                        iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("Poročilo o prodani pijači")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(20);
                        document.Add(header);

                        iText.Layout.Element.Paragraph header1 = new iText.Layout.Element.Paragraph("Fest.si")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(header1);

                        iText.Layout.Element.Paragraph naslov = new iText.Layout.Element.Paragraph("Rajšpova ulica 16")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(naslov);

                        iText.Layout.Element.Paragraph posta = new iText.Layout.Element.Paragraph("2250 Ptuj")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetFontSize(14);
                        document.Add(posta);

                        LineSeparator lineSeparator = new LineSeparator(new SolidLine());
                        document.Add(lineSeparator);

                        iText.Layout.Element.Paragraph imePrireditveParagraph = new iText.Layout.Element.Paragraph("Ime prireditve: " + nazivPrireditve);
                        document.Add(imePrireditveParagraph);

                        iText.Layout.Element.Paragraph imeSankaParagraph = new iText.Layout.Element.Paragraph("Ime šanka: " + imeSanka);
                        document.Add(imeSankaParagraph);

                        iText.Layout.Element.Paragraph datumNastankaParagraph = new iText.Layout.Element.Paragraph("Datum nastanka poročila: " + datumNastanka.ToString());
                        document.Add(datumNastankaParagraph);

                        iText.Layout.Element.Paragraph imeNosacaParagraph = new iText.Layout.Element.Paragraph("Ime nosača: " + imeNosaca);
                        document.Add(imeNosacaParagraph);

                        iText.Layout.Element.Paragraph natakarjiParagraph = new iText.Layout.Element.Paragraph("Natakarji: ");
                        document.Add(natakarjiParagraph);

                        foreach (var item in seznamNatakarjev)
                        {
                            iText.Layout.Element.Paragraph imeNatakarjaParagraph = new iText.Layout.Element.Paragraph(item.ime + " " + item.priimek);
                            document.Add(imeNatakarjaParagraph);

                        }

                        Utilities.IncludeDataTableInPdf(document, "Komora", Komora);
                        Utilities.IncludeDataTableInPdf(document, "Nosac", Nosac);
                        Utilities.IncludeDataTableInPdf(document, "Blagajna", blagajna);
                        Utilities.IncludeDataTableInPdf(document, "Primerjava komore in blagajne", primerjavaKomoraBlagajna);


                        document.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message);
            }
        }
        public static void IncludeDataTableInPdf(Document document, string tableName, DataTable dataTable)
        {
            iText.Layout.Element.Paragraph tableTitle = new iText.Layout.Element.Paragraph(tableName)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(tableTitle);

            // Create a table
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(dataTable.Columns.Count);

            // Add table headers
            foreach (DataColumn column in dataTable.Columns)
            {
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(column.ColumnName)));
            }

            // Add table rows
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.ToString())));
                }
            }

            // Add the table to the document
            document.Add(table);

            // Add space between tables
            document.Add(new AreaBreak());
        }

        /*
         * Metode za ustvarjanje map "FestPrireditve" in vse konkretne prireditve znotraj nje
        */
        public static void UstvariMapoPrireditve(string ImePrireditve, string LetoPrireditve)
        {
            try
            {
                string osnovnaPot = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string GlavnaPrireditevFolderPot = Path.Combine(osnovnaPot, "FestPrireditve");

                string KonkretnaPrireditevFolderPot = Path.Combine(GlavnaPrireditevFolderPot, ImePrireditve + LetoPrireditve);

                if (!Directory.Exists(GlavnaPrireditevFolderPot))
                {
                    Directory.CreateDirectory(GlavnaPrireditevFolderPot);
                    MessageBox.Show("Na namizju je bila ustvrajena mapa FestPrireditve");
                }

                if (!Directory.Exists(KonkretnaPrireditevFolderPot))
                {
                    Directory.CreateDirectory(KonkretnaPrireditevFolderPot);
                    MessageBox.Show($"Mapa za prireditev {ImePrireditve} {LetoPrireditve} je bila ustvarjena.");
                }
                else
                {
                    MessageBox.Show($"Mapa za prireditev {ImePrireditve} {LetoPrireditve} že obstaja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri ustvarjanju mape: {ex.Message}");
            }
        }


        public static string PridobiMapoPrireditve(string ImePrireditve, string LetoPrireditve)
        {
            string osnovnaPot = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string prireditevFolderPot = System.IO.Path.Combine(osnovnaPot, "FestPrireditve", ImePrireditve + LetoPrireditve);

            return prireditevFolderPot;
        }

}
}
