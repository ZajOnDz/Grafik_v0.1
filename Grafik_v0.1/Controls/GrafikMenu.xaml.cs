using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Logika interakcji dla klasy GrafikMenu.xaml
    /// </summary>
    public partial class GrafikMenu : UserControl, INotifyPropertyChanged
    {
        // ------------------ Event PropertyChanged ------------------ //
        #region
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string Property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
        }
        #endregion

        // ------------------ Deklaracja Właściwości ------------------ //

        #region
        // Delegat funkcji odświerzającej sumę godzin pracy w miesiącu
        Action RefreshHours_Delegate;

        // Aktualnie wybrany zakres godzin pracy. Wyświetlany w SelectedMenuItemLabel
        private string _selectedContent = "--:--";
        public string SelectedContent
        {
            get
            {
                return _selectedContent;
            }
            set
            {
                _selectedContent = value;
                RWFile.Saved = false;
                OnPropertyChanged("SelectedContent");
            }
        }

        // Index aktualnie wybranej opcji z GrafikMenu
        public byte _selectedIndex = (byte)(RWFile.schematMenu.Count - 1);
        public byte SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                SelectedContent = RWFile.schematMenu[SelectedIndex].Content;
                SelectedHour = RWFile.schematMenu[SelectedIndex].Hour;
                RWFile.Saved = false;
            }
        }
        // Ilość godzin przypisana do aktualnie wybranej opcji, pomagająca w podliczaniu sumy godzin w miesiącu
        public float SelectedHour { get; set; } = 0;

        // Ustawianie koloru tła wybranej opcji SelectedMenuItemLabel
        public new Brush Background
        {
            get
            {
                return SelectedMenuItemLabel.Background;
            }
            set
            {
                SelectedMenuItemLabel.Background = value;
                RWFile.Saved = false;
            }
        } 

        // Status rozwinięcia menu. 
        public bool isOpen
        {
            get
            {
                return popupControl.IsOpen;
            }
            set
            {
                popupControl.IsOpen = value;
            }
        }

        // Status kolorowania - Aktywne / nie aktywne
        public bool Coloring { get; set; } = false;

        // Status rozwinięcia wszystkich menu. Blokuje funkcje ukrywającą menu w przypadku MouseLeave
        public bool ShowAll { get; set; } = false;

        // Lista dostępnych opcji w Menu, pobierana z szablonu zawartego w RWFile.SchematMenu
        public ObservableCollection<MenuItem> MenuItems { get; set; } = RWFile.schematMenu;
        #endregion




        // ------------------ Konstruktor ------------------ //

        public GrafikMenu(Action RefreshHours_Delegate)
        {
            InitializeComponent();

            this.DataContext = this;
            this.RefreshHours_Delegate = RefreshHours_Delegate;

            Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }





        // ------------------ Mouse Events ------------------ //


        /// <summary>
        /// Event odpowiada za rozwijanie menu
        /// </summary>
        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Coloring)
                popupControl.IsOpen = !popupControl.IsOpen;
            else
            {
                Background = DB.Color;
            }
        }

        /// <summary>
        /// Event ustawia SelectedItem na kliknięty MenuItem
        /// </summary>
        private void MenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string content = (string)((Label)sender).Content;

            SelectedIndex = (byte)MenuItems.IndexOf(MenuItems.First(x => x.Content == content));

            SelectedContent = MenuItems[SelectedIndex].Content;

            SelectedHour = MenuItems[SelectedIndex].Hour;

            RefreshHours_Delegate();
        }

        /// <summary>
        /// Event odpowiedzialny za auto ukrywanie Menu, gdy mysz opuści jego obszar
        /// Funkcja nie działa, gdy aktywna jest opcja ShowAll.
        /// </summary>
        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!ShowAll)
                popupControl.IsOpen = false;
        }
    }
}
