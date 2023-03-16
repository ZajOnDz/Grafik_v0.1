using System;
using System.Windows;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy wPracownicy.xaml
    /// </summary>
    public partial class wPracownicy : Window
	{
		public wPracownicy()
		{
			InitializeComponent();

			lv_Pracownicy.ItemsSource = DB.Workers;
		}


		private void DodajPracownika_Click(object sender, RoutedEventArgs e)
		{
			DodajPracownika();
		}

		private void UsunPracownika_Click(object sender, RoutedEventArgs e)
		{
			if (lv_Pracownicy.SelectedIndex >= 0)
				DB.Workers.RemoveAt(lv_Pracownicy.SelectedIndex);
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

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				this.Close();
		}
	}
}
