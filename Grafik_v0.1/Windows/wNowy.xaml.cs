
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy wNowy.xaml
    /// </summary>
    public partial class wNowy : Window
    {
        private MainWindow mainWindow;

        public wNowy(MainWindow main)
        {
            InitializeComponent();

            mainWindow = main;

            lv_ListaPracownikow.ItemsSource = DB.Workers;
            lv_WybraniPracownicy.ItemsSource = RWFile.Workers;

        }

        /// <summary>
        /// Przesuwanie pracowników z listy wszystkich pracowników do listy wybranych pracowników
        /// </summary>
        private void listaWszystkichPracownikow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!RWFile.Workers.Contains(DB.Workers[lv_ListaPracownikow.SelectedIndex]))
            {
                RWFile.Workers.Add(DB.Workers[lv_ListaPracownikow.SelectedIndex]);
            }
        }

        /// <summary>
        /// Usuwanie pracowników z listy wybranych pracowników
        /// </summary>
        private void listaWybranychPracownikow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            RWFile.Workers.RemoveAt(lv_WybraniPracownicy.SelectedIndex);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        /// <summary>
        /// Tworzenie pustego grafiku z pracownikami, lecz bez wybranych godzin pracy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            // wyczyść obecny stan

            mainWindow.calendarPanel.Children.Clear();
            RWFile.grafikList.Clear();

            // Ustaw Datę

            RWFile.Data = new DateTime(Convert.ToInt32(((ComboBoxItem)rok_Combo.SelectedValue).Content.ToString()), (miesiac_Combo.SelectedIndex + 1), 1);

            // Przenieś schemat menu z SB.listy schematów menu do RWFile.SchematMenu

            // Utwórz nowe kontrolki grafiku i dodaj je do listy RWFile.grafikList
            foreach (var item in RWFile.Workers)
            {
                RWFile.grafikList.Add(new Grafik() { WorkerName = item });
            }

            Kalendarz kalendarz = new Kalendarz();

            kalendarz.Name = "calendar";
            kalendarz.SetValue(Grid.RowProperty, 1);

            mainWindow.calendarPanel.Children.Add(kalendarz);
            mainWindow.colorButton.IsEnabled = true;

            mainWindow.menuDodajUsunPracownika.IsEnabled = true;
            mainWindow.menuZapisz.IsEnabled = true;
            mainWindow.menuZapiszJako.IsEnabled = true;

            RWFile.Saved = false;

            this.Close();
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
