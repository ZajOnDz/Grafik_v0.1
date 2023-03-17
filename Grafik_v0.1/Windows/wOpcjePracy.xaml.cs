using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Grafik_v0._1.Windows
{
    /// <summary>
    /// Okno edycji menu opcji pracy
    /// </summary>
    public partial class wOpcjePracy : Window
	{
		/// <summary>
		/// Konstruktor
		/// </summary>
		public wOpcjePracy()
		{
			InitializeComponent();
			SchemeListViewControl.ItemsSource = RWFile.schematMenu;
		}

		/// <summary>
		/// Aktywacja lub dezaktywacja przycisków Góra/Dół 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SchemeListViewControl_Click(object sender, SelectionChangedEventArgs e)
		{
			if (SchemeListViewControl.SelectedIndex == RWFile.schematMenu.Count - 1)
			{
				upButton.IsEnabled = false;
				downButton.IsEnabled = false;

				selectedItemContent.IsEnabled = false;
				selectedItemHour.IsEnabled = false;
			}
			else if (SchemeListViewControl.SelectedIndex == RWFile.schematMenu.Count - 2)
			{
				upButton.IsEnabled = true;
				downButton.IsEnabled = false;

				selectedItemContent.IsEnabled = true;
				selectedItemHour.IsEnabled = true;
			}
			else if (SchemeListViewControl.SelectedIndex == 0)
			{
				upButton.IsEnabled = false;
				downButton.IsEnabled = true;

				selectedItemContent.IsEnabled = true;
				selectedItemHour.IsEnabled = true;
			}
			else
			{
				upButton.IsEnabled = true;
				downButton.IsEnabled = true;

				selectedItemContent.IsEnabled = true;
				selectedItemHour.IsEnabled = true;
			}

			selectedItemContent.Text = RWFile.schematMenu[SchemeListViewControl.SelectedIndex].Content;
			selectedItemHour.Text = RWFile.schematMenu[SchemeListViewControl.SelectedIndex].Hour.ToString();
		}

		/// <summary>
		/// Dodawanie nowej opcji do menu
		/// </summary>
		private void Dodaj_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (selectedItemContent.Text != "" && selectedItemHour.Text != "")
					RWFile.schematMenu.Insert(
						RWFile.schematMenu.Count - 1,
						new MenuItem()
						{
							Content = selectedItemContent.Text,
							Hour = Convert.ToSingle(selectedItemHour.Text.Replace(".", ","))
						}
						);

				byte lastIndex = (byte)(RWFile.schematMenu.Count - 1);

				foreach (var item in RWFile.grafikList)
				{
					foreach (var menu in item.menusList)
					{
						if (menu.SelectedIndex == lastIndex - 1)
							menu.SelectedIndex = lastIndex;
					}
				}

				RWFile.Saved = false;
			}
			catch (Exception exc)
			{
				MessageBox.Show("Wystąpił błąd");
			}
		}

		/// <summary>
		/// Usuwanie opcji z menu
		/// </summary>
		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			int index = SchemeListViewControl.SelectedIndex;

			

			if (index != RWFile.schematMenu.Count - 1 && index != -1)
			{
				SchemeListViewControl.SelectedIndex = SchemeListViewControl.SelectedIndex + 1;

				try
				{
					RWFile.schematMenu.RemoveAt(index);

					foreach (var grafik in RWFile.grafikList)
					{
						foreach (var menu in grafik.menusList)
						{
							if (menu.SelectedIndex == index)
								menu.SelectedIndex = (byte)(RWFile.schematMenu.Count - 1);
							else if (menu.SelectedIndex > index && menu.SelectedIndex != (byte)(RWFile.schematMenu.Count - 1))
								menu.SelectedIndex--;
						}
					}



					RWFile.Saved = false;
				}
				catch (Exception exc)
				{
					MessageBox.Show("Wystąpił błąd");
				}
			}
		}

		/// <summary>
		/// Zapisywanie danych opcji po edytowaniu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Zapisz_Click(object sender, RoutedEventArgs e)
		{

			try
			{
				RWFile.schematMenu[SchemeListViewControl.SelectedIndex].Content = selectedItemContent.Text;
				RWFile.schematMenu[SchemeListViewControl.SelectedIndex].Hour = Convert.ToSingle(selectedItemHour.Text.Replace(".", ","));

				foreach (var item in RWFile.grafikList)
					item.RefreshControl();

				RWFile.Saved = false;
			}
			catch (Exception)
			{

			}
		}

		/// <summary>
		/// Przesówanie opcji menu w górę
		/// </summary>
		private void PrzesunWGore_Click(object sender, RoutedEventArgs e)
		{
			if (SchemeListViewControl.SelectedIndex != RWFile.schematMenu.Count - 1)
				if (SchemeListViewControl.SelectedIndex > 0)
				{
					byte index = (byte)SchemeListViewControl.SelectedIndex;

					string contentTemp = RWFile.schematMenu[index - 1].Content;
					float hourTemp = RWFile.schematMenu[index - 1].Hour;

					RWFile.schematMenu[index - 1].Content = RWFile.schematMenu[index].Content;
					RWFile.schematMenu[index - 1].Hour = RWFile.schematMenu[index].Hour;

					RWFile.schematMenu[index].Content = contentTemp;
					RWFile.schematMenu[index].Hour = hourTemp;

					SchemeListViewControl.SelectedIndex = index - 1;

					foreach (var item in RWFile.grafikList)
					{
						foreach (var menu in item.menusList)
						{

							if (menu.SelectedIndex == index)
							{
								menu.SelectedIndex = (byte)(index - 1);
							}
							else if (menu.SelectedIndex == (index - 1))
							{
								menu.SelectedIndex = index;
							}

						}
					}
				}

			RWFile.Saved = false;
		}

		/// <summary>
		/// Przesówanie opcji menu w dół
		/// </summary>
		private void PrzesunWDol_Click(object sender, RoutedEventArgs e)
		{
			if (SchemeListViewControl.SelectedIndex != RWFile.schematMenu.Count - 1)
				if (SchemeListViewControl.SelectedIndex < RWFile.schematMenu.Count - 2)
				{
					byte index = (byte)SchemeListViewControl.SelectedIndex;

					string contentTemp = RWFile.schematMenu[index + 1].Content;
					float hourTemp = RWFile.schematMenu[index + 1].Hour;

					RWFile.schematMenu[index + 1].Content = RWFile.schematMenu[index].Content;
					RWFile.schematMenu[index + 1].Hour = RWFile.schematMenu[index].Hour;

					RWFile.schematMenu[index].Content = contentTemp;
					RWFile.schematMenu[index].Hour = hourTemp;

					SchemeListViewControl.SelectedIndex = index + 1;

					foreach (var item in RWFile.grafikList)
					{
						foreach (var menu in item.menusList)
						{
							if (menu.SelectedIndex == index)
							{
								menu.SelectedIndex = (byte)(index + 1);
							}
							else if (menu.SelectedIndex == (index + 1))
							{
								menu.SelectedIndex = index;
							}
						}
					}
				}

			RWFile.Saved = false;
		}

		/// <summary>
		/// Zapisywanie obecnego menu jako stałego schematu który wczytuje się na starcie programu.
		/// </summary>
		private void ZapiszSchemat_Click(object sender, RoutedEventArgs e)
		{
			DB.schematMenu = new ObservableCollection<MenuItem>();

			foreach (var item in RWFile.schematMenu)
			{
				DB.schematMenu.Add((MenuItem)item.Clone());
			}
		}

		/// <summary>
		/// Zamykanie okno po wciśnięciu Esc
		/// </summary>
		private void Window_OnkeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				this.Close();
		}
	}
}
