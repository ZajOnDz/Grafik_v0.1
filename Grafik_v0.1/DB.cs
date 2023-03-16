using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace Grafik_v0._1
{
    public static class DB
    {
        public static Brush Color { get; set; } = new SolidColorBrush();

        public static ObservableCollection<MenuItem> schematMenu = new ObservableCollection<MenuItem>();

        public static ObservableCollection<string> Workers = new ObservableCollection<string>();


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

                foreach (var item in schematMenu)
                {
                    RWFile.schematMenu.Add((MenuItem)item.Clone());
                }
            }
            else
            {
                schematMenu.Add(new MenuItem() { Content = "8:00\n16:00", Hour = 8 });
                schematMenu.Add(new MenuItem() { Content = "--:--", Hour = 0 });

                foreach (var item in schematMenu)
                {
                    RWFile.schematMenu.Add((MenuItem)item.Clone());
                }
            }
        }
    }
}
