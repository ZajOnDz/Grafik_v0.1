using System;
using System.Windows;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Okno dodawania pracowników do listy wszystkich pracujących w firmie
    /// </summary>
    public partial class wPracownicy : Window
	{
		/// <summary>
		/// Konstruktor
		/// </summary>
		public wPracownicy()
		{
			InitializeComponent();

			lv_Pracownicy.ItemsSource = DB.Workers;
		}

		/// <summary>
		/// Dodawanie pracownika do listy wszystkich pracowników w firmie
		/// </summary>
		private void DodajPracownika_Click(object sender, RoutedEventArgs e)
		{
			DodajPracownika();
		}

		private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				DodajPracownika();
		}

		private void DodajPracownika()
		{
			if (tbox_ImieNowegoPracownika.Text.Length > 2)
				DB.Workers.Add(tbox_ImieNowegoPracownika.Text);

			tbox_ImieNowegoPracownika.Text = String.Empty;
		}

		/// <summary>
		/// Usuwanie pracownika
		/// </summary>
		private void UsunPracownika_Click(object sender, RoutedEventArgs e)
		{
			if (lv_Pracownicy.SelectedIndex >= 0)
				DB.Workers.RemoveAt(lv_Pracownicy.SelectedIndex);
		}

		/// <summary>
		/// Zamykanie okno po wciśnięciu klawisza Esc
		/// </summary>
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				this.Close();
		}
	}
}
