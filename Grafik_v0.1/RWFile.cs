using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Klasa odpowiedzialna za odczytywanie, przechowywanie i zapisywanie danych o grafiku do pliku
    /// </summary>
    public static class RWFile
    {
        // Ścieżka do zapisanego/otwartego pliku
        public static string Path { get; set; } = "";
        // Data której dotyczy grafik
        public static DateTime Data { get; set; } = DateTime.Now;
        // Lista pracowników w grafiku
        public static ObservableCollection<string> Workers { get; set; } = new ObservableCollection<string>();
        // Schemat opcji menu użytych w obecnie otwartym grafiku
        public static ObservableCollection<MenuItem> schematMenu { get; set; } = new ObservableCollection<MenuItem>();
        // Lista grafików pracowników
        public static ObservableCollection<Grafik> grafikList { get; set; } = new ObservableCollection<Grafik>();
        // Zawiera informację czy istnieją jakiejś nie zapisane zmiany
        public static bool Saved { get; set; } = true;

        /// <summary>
        /// Zapisywanie pliku z grafikiem.
        /// </summary>
        public static void SaveFile()
        {
            MemoryStream MS = new MemoryStream();

            BinaryWriter BW = new BinaryWriter(MS);

            // magic
            BW.Write("zgr");

            // data
            short year = (short)Data.Year;
            byte month = (byte)Data.Month;

            BW.Write(year);
            BW.Write(month);

            // ilosc pracownikow
            byte workersCount = (byte)grafikList.Count;
            BW.Write(workersCount);

            // pracownicy

            foreach (var worker in grafikList)
            {
                BW.Write(worker.WorkerName);
            }

            // ilosc opcji menu
            byte menuCount = (byte)schematMenu.Count;

            BW.Write(menuCount);

            // opcje menu
            foreach (var menu in schematMenu)
            {
                BW.Write(menu.Content);
                BW.Write(menu.Hour);
            }

            // grafiki pracowników
            foreach (var grafik in grafikList)
            {
                BW.Write(grafik.WorkerName);

                foreach (var menu in grafik.menusList)
                {
                    BW.Write(menu.SelectedIndex);

                    Color color = ((SolidColorBrush)menu.Background).Color;

                    BW.Write(color.R);
                    BW.Write(color.G);
                    BW.Write(color.B);
                }
            }

            byte[] binary = MS.ToArray();

            File.WriteAllBytes(Path, binary);

            Saved = true;
        }

        /// <summary>
        /// Wczytywanie pliku grafiku.
        /// </summary>
        public static void ReadFile()
        {
            MemoryStream MS = new MemoryStream(File.ReadAllBytes(Path));

            BinaryReader BR = new BinaryReader(MS);

            // magic
            if (BR.ReadString() != "zgr")
                return;

            // data
            short year = BR.ReadInt16();

            byte month = BR.ReadByte();

            Data = new DateTime(year, month, 1);

            // ilosc pracownikow
            byte workersCount = BR.ReadByte();

            // pracownicy
            for (int i = 0; i < workersCount; i++)
            {
                Workers.Add(BR.ReadString());
            }

            // ilosc opcji menu
            byte menuCount = BR.ReadByte();

            for (int i = 0; i < menuCount; i++)
            {
                MenuItem schemat = new MenuItem();
                schemat.Content = BR.ReadString();
                schemat.Hour = BR.ReadSingle();

                schematMenu.Add(schemat);
            }

            // grafiki pracowników
            for (int i = 0; i < workersCount; i++)
            {
                Grafik grafik = new Grafik();

                grafik.WorkerName = BR.ReadString();

                for (int j = 0; j < DateTime.DaysInMonth(Data.Year, Data.Month); j++)
                {
                    grafik.menusList[j].SelectedIndex = BR.ReadByte();

                    byte R = BR.ReadByte();
                    byte G = BR.ReadByte();
                    byte B = BR.ReadByte();

                    grafik.menusList[j].Background = new SolidColorBrush(Color.FromRgb(R, G, B));
                }

                grafikList.Add(grafik);

                grafik.RefreshHours();
            }

            Saved = true;
        }
    }
}
