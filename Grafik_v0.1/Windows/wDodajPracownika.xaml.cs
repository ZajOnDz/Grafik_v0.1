
using System.Windows;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Okno służące do usuwania i dodawania pracowników do grafiku w trakcie wykonywania go
    /// </summary>
    public partial class wDodajPracownika : Window
    {

        /// <summary>
        /// Konstruktor
        /// </summary>
        public wDodajPracownika()
        {
            InitializeComponent();

            workersControl.ItemsSource = RWFile.Workers;

            allWorkersControl.ItemsSource = DB.Workers;
        }

        /// <summary>
        /// Pobieranie pracownika z listy wszystkich pracowników i dodawanie pracownika do grafiku 
        /// </summary>
        private void AllWorkwers_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!RWFile.Workers.Contains(DB.Workers[allWorkersControl.SelectedIndex]))
            {
                RWFile.Workers.Add(DB.Workers[allWorkersControl.SelectedIndex]);
                RWFile.grafikList.Add(new Grafik() { WorkerName = DB.Workers[allWorkersControl.SelectedIndex] });
            }

            RWFile.Saved = false;
        }

        /// <summary>
        /// Usuwanie pracownika z grafiku
        /// </summary>
        private void Workers_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string worker = RWFile.Workers[workersControl.SelectedIndex];

            foreach (var item in RWFile.grafikList)
            {
                if (item.WorkerName == worker)
                {
                    RWFile.grafikList.Remove(item);
                    RWFile.Workers.Remove(worker);
                    return;
                }
            }

            RWFile.Saved = false;
        }
    }
}
