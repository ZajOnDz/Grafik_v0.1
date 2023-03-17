using Grafik_v0._1.Windows;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Grafik_v0._1
{
    /// <summary>
    /// Część MainWindow obsługująca wszystkie eventy głównego okna. Głównie obsługa górnego menu.
    /// </summary>
    public partial class MainWindow
	{
		/// <summary>
		///  Event - Tworzenie nowego pliku grafiku
		/// </summary>
		private void Nowy_Click(object sender, RoutedEventArgs e)
		{
			// Otwarcie okna wNowy
			wNowy oknoNowegoPliku = new wNowy(this);
			oknoNowegoPliku.Show();
		}

		/// <summary>
		///  Event - Otwieranie pliku
		/// </summary>
		private void Otworz_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();

			openDialog.Filter = "Grafik File|*.zgr";
			openDialog.Title = "Otwieranie pliku grafiku...";

			openDialog.ShowDialog();

			if (openDialog.FileName != "")
			{
				calendarPanel.Children.Clear();
				RWFile.grafikList.Clear();
				RWFile.schematMenu.Clear();

				RWFile.Path = openDialog.FileName;

				RWFile.ReadFile();

				Kalendarz kalendarz = new Kalendarz();
				calendarPanel.Children.Add(kalendarz);

				colorButton.IsEnabled = true;

				menuDodajUsunPracownika.IsEnabled = true;
				menuZapisz.IsEnabled = true;
				menuZapiszJako.IsEnabled = true;
			}
			else
			{
				return;
			}
		}

		/// <summary>
		///  Event - Zapisywanie pliku
		/// </summary>
		private void Zapisz_Click(object sender, RoutedEventArgs e)
		{
			if (RWFile.Path == "")
			{
				OpenSaveDialog();
			}
			else
			{
				RWFile.SaveFile();
			}
		}

		/// <summary>
		///  Event - Zapisywanie kopii pliku
		/// </summary>
		private void ZapiszJako_Click(object sender, RoutedEventArgs e)
		{
			OpenSaveDialog();
		}

		private void OpenSaveDialog()
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.DefaultExt = "zgr";
			saveDialog.Filter = "Grafik File|*.zgr";
			saveDialog.Title = "Zapisywanie pliku grafiku...";
			saveDialog.ShowDialog();

			if (saveDialog.FileName != "")
			{
				RWFile.Path = saveDialog.FileName;
				RWFile.SaveFile();
			}
		}

		/// <summary>
		///  Event - Zamykanie aplikacji poprzez opcje "Wyjście" w menu
		/// </summary>
		private void Wyjscie_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		/// <summary>
		///  Event - Otwarcie okna odpowiedzialnego za dodawanie i usuwanie pracowników w obecnie otwartym grafiku
		/// </summary>
		private void DodajPracownikaDoGrafiku_Click(object sender, RoutedEventArgs e)
		{
			wDodajPracownika oknoDodajPracownika = new wDodajPracownika();
			oknoDodajPracownika.Show();
		}

		/// <summary>
		///  Event - Zarządzanie listą wszystkich pracowników w firmie
		/// </summary>
		private void ZarzadzajPracownikami_Click(object sender, RoutedEventArgs e)
		{
			wPracownicy oknoZarzadzaniaPracownikami = new wPracownicy();
			oknoZarzadzaniaPracownikami.Show();
		}

		/// <summary>
		///  Event - Konfiguracja menu z opcjami pracy. Zarządzanie godzinami rozpoczecia i zakończenia pracy
		/// </summary>
		private void KonfiguracjaOpcjiPracy_Click(object sender, RoutedEventArgs e)
		{
			wOpcjePracy oknoOpcjiPracy = new wOpcjePracy();
			oknoOpcjiPracy.Show();
		}

		/// <summary>
		///  Event - Aktywacja opcji kolorowania grafiku
		/// </summary>
		private void ColorButton_Click(object sender, RoutedEventArgs e)
		{
			Coloring = !Coloring;

			colorButton.Content = Coloring ? "Zakończ" : "Koloruj";

			foreach (var item in RWFile.grafikList)
			{
				item.Coloring(Coloring);
			}
		}

		/// <summary>
		///  Event wyjścia z aplikacji. Zapisywanie danych.
		/// </summary>
		private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!RWFile.Saved)
			{
				MessageBoxResult result = MessageBox.Show("Posiadasz nie zapisane zmiany.\n Czy chcesz je zapisać?", "Opuszczanie programu...", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

				if (result == MessageBoxResult.Yes)
				{
					if (RWFile.Path == "")
						OpenSaveDialog();
					else
						RWFile.SaveFile();
				}
				else if (result == MessageBoxResult.Cancel)
					e.Cancel = true;
			}

			DB.SaveDB();
		}
	}
}
