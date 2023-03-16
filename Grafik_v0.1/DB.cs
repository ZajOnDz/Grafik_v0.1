using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Klasa przechowująca, zapisująca i odczytująca ogólne dane programu w pliku Data.db
    /// Plik zawiera dane o wszystkich pracownikach w firmie oraz standardowy schemat menu opcji pracy.
    /// </summary>
    public static class DB
    {
        // Zmienna pomocnicza. Przechowuje obecnie wybrany kolor w trakcie kolorowania grafiku.
        public static Brush Color { get; set; } = new SolidColorBrush();

        // Stały schemat menu, wczytywany zawsze przy uruchumieniu programu, oraz przy tworzeniu nowego pliku z grafikiem.
        public static ObservableCollection<MenuItem> schematMenu = new ObservableCollection<MenuItem>();

        // Lista wszystkich pracowników.
        public static ObservableCollection<string> Workers = new ObservableCollection<string>();

        /// <summary>
        /// Zapisywanie schematu menu i listy pracowników
        /// </summary>
        public static void SaveDB()
        {
            MemoryStream MS = new MemoryStream();

            BinaryWriter BW = new BinaryWriter(MS);

            // Zapisywanie listy pracowników

            byte workersNumbers = (byte)Workers.Count;

            BW.Write(workersNumbers);

            foreach (var worker in Workers)
            {
                BW.Write(worker);
            }

            // Zapisywanie Schematu Menu

            byte menuCount = (byte)schematMenu.Count;

            BW.Write(menuCount);

            foreach (var item in schematMenu)
            {
                BW.Write(item.Content);
                BW.Write(item.Hour);
            }

            byte[] binary = MS.ToArray();

            File.WriteAllBytes("Data.db", binary);

        }

        /// <summary>
        /// Wczytywanie schematu menu i listy pracowników
        /// </summary>
        public static void ReadDB()
        {
            if (File.Exists("Data.db"))
            {
                MemoryStream MS = new MemoryStream(File.ReadAllBytes("Data.db"));
                BinaryReader BR = new BinaryReader(MS);

                // Wczytywanie listy pracowników 

                byte workers = BR.ReadByte();

                for (int i = 0; i < workers; i++)
                {
                    Workers.Add(BR.ReadString());
                }

                // Wczytywanie schematu menu

                byte menuCount = BR.ReadByte();

                for (int i = 0; i < menuCount; i++)
                {
                    MenuItem menu = new MenuItem();

                    menu.Content = BR.ReadString();
                    menu.Hour = BR.ReadSingle();

                    schematMenu.Add(menu);
                }


                // Kopiowanie standardowego schematu do obiektu ze schematem używanym obecnie w grafiku,
                // aby doraźne zmiany w menu wprowadzane na potrzeby obecnego grafiku nie wpływały na listę stałego schematu menu
                foreach (var item in schematMenu)
                {
                    RWFile.schematMenu.Add((MenuItem)item.Clone());
                }
            }
            else // W przypadku braku pliku Data.db ustawiane są przykładowe opcje menu
            {
                schematMenu.Add(new MenuItem() { Content = "6:00\n14:00", Hour = 8 });
                schematMenu.Add(new MenuItem() { Content = "14:00\n22:00", Hour = 8 });
                schematMenu.Add(new MenuItem() { Content = "--:--", Hour = 0 });

                foreach (var item in schematMenu)
                {
                    RWFile.schematMenu.Add((MenuItem)item.Clone());
                }
            }
        }
    }
}
