
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Okno tworzenia nowego pliku grafiku.
    /// </summary>
    public partial class wNowy : Window
    {
        private MainWindow mainWindow;


        /// <summary>
        /// Konstruktor
        /// </summary>
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
            DodajPracownika();
        }

        private void DodajPracownika_Click(object sender, RoutedEventArgs e)
        {
            DodajPracownika();
        }

        private void DodajPracownika()
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
            UsunPracownika();
        }

        private void UsunPracownika_Click(object sender, RoutedEventArgs e)
        {
            UsunPracownika();
        }

        private void UsunPracownika()
        {
            RWFile.Workers.RemoveAt(lv_WybraniPracownicy.SelectedIndex);
        }

        private void Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            // wyczyść obecny stan

            mainWindow.calendarPanel.Children.Clear();
            RWFile.grafikList.Clear();

            // Ustaw Datę

            RWFile.Data = new DateTime(Convert.ToInt32(((ComboBoxItem)rok_Combo.SelectedValue).Content.ToString()), (miesiac_Combo.SelectedIndex + 1), 1);

            // Utwórz nowe kontrolki grafiku i dodaj je do listy RWFile.grafikList
            foreach (var item in RWFile.Workers)
            {
                RWFile.grafikList.Add(new Grafik() { WorkerName = item });
            }

            // Dodawanie kalendarza
            Kalendarz kalendarz = new Kalendarz();

            kalendarz.Name = "calendar";
            kalendarz.SetValue(Grid.RowProperty, 1);

            mainWindow.calendarPanel.Children.Add(kalendarz);

            // Aktywacja opcji Zapisu pliku oraz opcji kolorowania grafiku
            mainWindow.colorButton.IsEnabled = true;

            mainWindow.menuDodajUsunPracownika.IsEnabled = true;
            mainWindow.menuZapisz.IsEnabled = true;
            mainWindow.menuZapiszJako.IsEnabled = true;


            // Informacja, że dokonano nie zapisanych zmian w pliku. 
            RWFile.Saved = false;

            this.Close();
        }

        /// <summary>
        /// Zamykanie okna przy wciśnięciu przycisku Anuluj
        /// </summary>
        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Zamykanie okna przy wciśnięciu Esc
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
