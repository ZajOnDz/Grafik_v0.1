using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Logika interakcji dla klasy Grafik.xaml
    /// </summary>
    public partial class Grafik : UserControl, INotifyPropertyChanged
	{

		// ------------------ Event PropertyChanged ------------------ //
		#region
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
		#endregion

		// ------------------ Deklaracja Właściwości ------------------ //

		#region
		// Lista kontrolek GrafikMenu
		public ObservableCollection<GrafikMenu> menusList { get; set; } = new ObservableCollection<GrafikMenu>();

		// Imie pracownika
		private string _workerName = "";
		public string WorkerName
		{
			get
			{
				return _workerName;
			}
			set
			{
				_workerName = value;
				OnPropertyChanged("WorkerName");
			}
		}

		// Suma godzin w miesiącu
		private float _totalHour = 0;
		public float TotalHour
		{
			get
			{
				return _totalHour;
			}
			set
			{
				_totalHour = value;
				OnPropertyChanged("TotalHour");
			}
		}
		#endregion

		// ------------------ Konstruktor ------------------ //

		public Grafik()
		{
			InitializeComponent();

			this.DataContext = this;

			Init();
		}




		/// <summary>
		///  Uzupełnianie kontroli Grafik listą kontrolek GrafikMenu. 
		///  Oznaczanie niedziel kolorem czerwonym
		/// </summary>
        private void Init()
        {
			for (int i = 1; i <= DateTime.DaysInMonth(RWFile.Data.Year, RWFile.Data.Month); i++)
			{
				if (new DateTime(RWFile.Data.Year, RWFile.Data.Month, i).DayOfWeek == DayOfWeek.Sunday)
				{
					GrafikMenu gMenu = new GrafikMenu(new Action(RefreshHours));
					gMenu.Background = Brushes.IndianRed;
					menusList.Add(gMenu);
				}
				else
				{
					menusList.Add(new GrafikMenu(new Action(RefreshHours)));
				}
			}
		}

		/// <summary>
		/// Przelicza sumę godzin w miesiącu po każdej wprowadzonej zmianie w grafiku.
		/// </summary>
        public void RefreshHours()
		{
			TotalHour = 0;

			foreach (var menu in menusList)
			{
				TotalHour += menu.SelectedHour;
			}
		}

		/// <summary>
		/// Odświeża GrafikMenu.SelectedContent i SelectedHour po wprowadzeniu zmian w schemacie GrafikMenu w oknie wOpcjePracy
		/// </summary>
		public void RefreshControl()
		{
			foreach (var item in menusList)
			{
				item.SelectedContent = RWFile.schematMenu[item.SelectedIndex].Content;
				item.SelectedHour = RWFile.schematMenu[item.SelectedIndex].Hour;
			}

			RefreshHours();
		}

		/// <summary>
		/// Funkcja odpowiedzialna za rozwijanie wszystkich GrafikMenu w obrębie jednego pracownika
		/// </summary>
		private void Show_Checked(object sender, RoutedEventArgs e)
		{
			foreach (var item in menusList)
			{
				item.isOpen = true;
				item.ShowAll = true;
			}
		}

		/// <summary>
		/// Funkcja odpowiedzialna za zwijanie wszystkich GrafikMenu w obrębie jednego pracownika
		/// </summary>
		private void Show_Unchecked(object sender, RoutedEventArgs e)
		{
			foreach (var item in menusList)
			{
				item.isOpen = false;
				item.ShowAll = false;
			}
		}

		/// <summary>
		/// Blokowanie wszystkich GrafikMenu zawartych w Grafik w trakcje kolorowania
		/// </summary>
		public void Coloring(bool isEnabled)
		{
			foreach (var item in menusList)
			{
				item.Coloring = isEnabled;
			}
		}
	}
}
