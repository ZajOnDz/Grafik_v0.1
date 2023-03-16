using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Kontrolka wyświetla dni miesiąca i tygodnia 
    /// </summary>
    public partial class Kalendarz : UserControl
    {
        // Lista label zawierająca numery dni miesiąca
        public ObservableCollection<Label> labelList = new ObservableCollection<Label>();


        public Kalendarz()
        {
            InitializeComponent();

            ShowControl();
        }

        /// <summary>
        /// Uzupełnia kontrolkę listą label wypełnionych numerem dnia miesiąca oraz skróconą nazwą dnia tygodnia.
        /// Ustawia kolor na czerwony dla każdej niedzieli
        /// </summary>
        private void ShowControl()
        {
            SetMonth();

            for (int i = 1; i <= DateTime.DaysInMonth(RWFile.Data.Year, RWFile.Data.Month); i++)
            {
                DateTime data = new DateTime(RWFile.Data.Year, RWFile.Data.Month, i);
                Label label = new Label();

                label.Content = i.ToString() + "\n" + GetDayOfWeek(data);

                if (data.DayOfWeek == DayOfWeek.Sunday)
                    label.Background = Brushes.IndianRed;

                labelList.Add(label);
            }

            panelKalendarza.ItemsSource = labelList;
        }

        /// <summary>
        ///  Zwraca skróconą nazwę dnia tygodnia
        /// </summary>
        private string GetDayOfWeek(DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Pn";
                case DayOfWeek.Tuesday:
                    return "Wt";
                case DayOfWeek.Wednesday:
                    return "Śr";
                case DayOfWeek.Thursday:
                    return "Cz";
                case DayOfWeek.Friday:
                    return "Pt";
                case DayOfWeek.Saturday:
                    return "Sb";
                case DayOfWeek.Sunday:
                    return "Nd";
            }
            return "";
        }

        /// <summary>
        /// Zwraca nazwę miesiąca
        /// </summary>
        private void SetMonth()
        {
            switch (RWFile.Data.Month)
            {
                case 1:
                    nazwaMiesiaca.Content = "Styczeń";
                    break;
                case 2:
                    nazwaMiesiaca.Content = "Luty";
                    break;
                case 3:
                    nazwaMiesiaca.Content = "Marzec";
                    break;
                case 4:
                    nazwaMiesiaca.Content = "Kwiecień";
                    break;
                case 5:
                    nazwaMiesiaca.Content = "Maj";
                    break;
                case 6:
                    nazwaMiesiaca.Content = "Czerwiec";
                    break;
                case 7:
                    nazwaMiesiaca.Content = "Lipiec";
                    break;
                case 8:
                    nazwaMiesiaca.Content = "Sierpień";
                    break;
                case 9:
                    nazwaMiesiaca.Content = "Wrzesień";
                    break;
                case 10:
                    nazwaMiesiaca.Content = "Październik";
                    break;
                case 11:
                    nazwaMiesiaca.Content = "Listopad";
                    break;
                case 12:
                    nazwaMiesiaca.Content = "Grudzień";
                    break;
            }
        }
    }
}
