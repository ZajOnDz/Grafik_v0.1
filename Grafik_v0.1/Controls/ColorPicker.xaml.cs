using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Grafik_v0._1
{
    /// <summary>
    /// Logika interakcji dla klasy ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
	{
		// Lista kolorow do wyboru
		ObservableCollection<SolidColorBrush> ColorList = new ObservableCollection<SolidColorBrush>()
		{
			//new SolidColorBrush(Color.FromRgb(51, 0, 0)),
			new SolidColorBrush(Color.FromRgb(102, 0, 0)),
			new SolidColorBrush(Color.FromRgb(153, 0, 0)),
			new SolidColorBrush(Color.FromRgb(204, 0, 0)),
			new SolidColorBrush(Color.FromRgb(255, 0, 0)),
			new SolidColorBrush(Color.FromRgb(255, 51, 51)),
			new SolidColorBrush(Color.FromRgb(255, 102, 102)),
			new SolidColorBrush(Color.FromRgb(255, 153, 153)),
			//new SolidColorBrush(Color.FromRgb(255, 204, 204)),

			//new SolidColorBrush(Color.FromRgb(51, 25, 0)),
			new SolidColorBrush(Color.FromRgb(102, 51, 0)),
			new SolidColorBrush(Color.FromRgb(153, 76, 0)),
			new SolidColorBrush(Color.FromRgb(204, 102, 0)),
			new SolidColorBrush(Color.FromRgb(255, 128, 0)),
			new SolidColorBrush(Color.FromRgb(255, 153, 51)),
			new SolidColorBrush(Color.FromRgb(255, 178, 102)),
			new SolidColorBrush(Color.FromRgb(255, 204, 153)),
			//new SolidColorBrush(Color.FromRgb(255, 229, 204)),
			
			//new SolidColorBrush(Color.FromRgb(51, 51, 0)),
			new SolidColorBrush(Color.FromRgb(102, 102, 0)),
			new SolidColorBrush(Color.FromRgb(153, 153, 0)),
			new SolidColorBrush(Color.FromRgb(204, 204, 0)),
			new SolidColorBrush(Color.FromRgb(255, 255, 0)),
			new SolidColorBrush(Color.FromRgb(255, 255, 51)),
			new SolidColorBrush(Color.FromRgb(255, 255, 102)),
			new SolidColorBrush(Color.FromRgb(255, 255, 153)),
			//new SolidColorBrush(Color.FromRgb(255, 255, 204)),

			//new SolidColorBrush(Color.FromRgb(25, 51, 0)),
			new SolidColorBrush(Color.FromRgb(51, 102, 0)),
			new SolidColorBrush(Color.FromRgb(76, 153, 0)),
			new SolidColorBrush(Color.FromRgb(102, 204, 0)),
			new SolidColorBrush(Color.FromRgb(128, 255, 0)),
			new SolidColorBrush(Color.FromRgb(153, 255, 51)),
			new SolidColorBrush(Color.FromRgb(178, 255, 102)),
			new SolidColorBrush(Color.FromRgb(204, 255, 153)),
			//new SolidColorBrush(Color.FromRgb(229, 255, 204)),

			//new SolidColorBrush(Color.FromRgb(0, 51, 0)),
			new SolidColorBrush(Color.FromRgb(0, 102, 0)),
			new SolidColorBrush(Color.FromRgb(0, 153, 0)),
			new SolidColorBrush(Color.FromRgb(0, 204, 0)),
			new SolidColorBrush(Color.FromRgb(0, 255, 0)),
			new SolidColorBrush(Color.FromRgb(51, 255, 51)),
			new SolidColorBrush(Color.FromRgb(102, 255, 102)),
			new SolidColorBrush(Color.FromRgb(153, 255, 153)),
			//new SolidColorBrush(Color.FromRgb(204, 255, 204)),

			//new SolidColorBrush(Color.FromRgb(0, 51, 25)),
			new SolidColorBrush(Color.FromRgb(0, 102, 51)),
			new SolidColorBrush(Color.FromRgb(0, 153, 76)),
			new SolidColorBrush(Color.FromRgb(0, 204, 102)),
			new SolidColorBrush(Color.FromRgb(0, 255, 128)),
			new SolidColorBrush(Color.FromRgb(51, 255, 153)),
			new SolidColorBrush(Color.FromRgb(102, 255, 178)),
			new SolidColorBrush(Color.FromRgb(153, 255, 204)),
			//new SolidColorBrush(Color.FromRgb(204, 255, 229)),

			//new SolidColorBrush(Color.FromRgb(0, 51, 51)),
			new SolidColorBrush(Color.FromRgb(0, 102, 102)),
			new SolidColorBrush(Color.FromRgb(0, 153, 153)),
			new SolidColorBrush(Color.FromRgb(0, 204, 204)),
			new SolidColorBrush(Color.FromRgb(0, 255, 255)),
			new SolidColorBrush(Color.FromRgb(51, 255, 255)),
			new SolidColorBrush(Color.FromRgb(102, 255, 255)),
			new SolidColorBrush(Color.FromRgb(153, 255, 255)),
			//new SolidColorBrush(Color.FromRgb(204, 255, 255)),

			//new SolidColorBrush(Color.FromRgb(0, 25, 51)),
			new SolidColorBrush(Color.FromRgb(0, 51, 102)),
			new SolidColorBrush(Color.FromRgb(0, 76, 153)),
			new SolidColorBrush(Color.FromRgb(0, 102, 204)),
			new SolidColorBrush(Color.FromRgb(0, 128, 255)),
			new SolidColorBrush(Color.FromRgb(51, 153, 255)),
			new SolidColorBrush(Color.FromRgb(102, 178, 255)),
			new SolidColorBrush(Color.FromRgb(153, 204, 255)),
			//new SolidColorBrush(Color.FromRgb(204, 229, 255)),

			//new SolidColorBrush(Color.FromRgb(0, 0, 51)),
			new SolidColorBrush(Color.FromRgb(0, 0, 102)),
			new SolidColorBrush(Color.FromRgb(0, 0, 153)),
			new SolidColorBrush(Color.FromRgb(0, 0, 204)),
			new SolidColorBrush(Color.FromRgb(0, 0, 255)),
			new SolidColorBrush(Color.FromRgb(51, 51, 255)),
			new SolidColorBrush(Color.FromRgb(102, 102, 255)),
			new SolidColorBrush(Color.FromRgb(153, 153, 255)),
			//new SolidColorBrush(Color.FromRgb(204, 204, 255)),

			//new SolidColorBrush(Color.FromRgb(25, 0, 51)),
			new SolidColorBrush(Color.FromRgb(51, 0, 102)),
			new SolidColorBrush(Color.FromRgb(76, 0, 153)),
			new SolidColorBrush(Color.FromRgb(102, 0, 204)),
			new SolidColorBrush(Color.FromRgb(128, 0, 255)),
			new SolidColorBrush(Color.FromRgb(153, 51, 255)),
			new SolidColorBrush(Color.FromRgb(178, 102, 255)),
			new SolidColorBrush(Color.FromRgb(204, 153, 255)),
			//new SolidColorBrush(Color.FromRgb(229, 204, 255)),

			//new SolidColorBrush(Color.FromRgb( 51, 0, 51)),
			new SolidColorBrush(Color.FromRgb(102, 0, 102)),
			new SolidColorBrush(Color.FromRgb(153, 0, 153)),
			new SolidColorBrush(Color.FromRgb(204, 0, 204)),
			new SolidColorBrush(Color.FromRgb(255, 0, 255)),
			new SolidColorBrush(Color.FromRgb(255, 51, 255)),
			new SolidColorBrush(Color.FromRgb(255, 102, 255)),
			new SolidColorBrush(Color.FromRgb(255, 153, 255)),
			//new SolidColorBrush(Color.FromRgb(255, 204, 255)),

			//new SolidColorBrush(Color.FromRgb(51, 0, 25)),
			new SolidColorBrush(Color.FromRgb(102, 0, 51)),
			new SolidColorBrush(Color.FromRgb(153, 0, 76)),
			new SolidColorBrush(Color.FromRgb(204, 0, 102)),
			new SolidColorBrush(Color.FromRgb(255, 0, 128)),
			new SolidColorBrush(Color.FromRgb(255, 51, 153)),
			new SolidColorBrush(Color.FromRgb(255, 102, 178)),
			new SolidColorBrush(Color.FromRgb(255, 153, 204)),
			//new SolidColorBrush(Color.FromRgb(255, 204, 229)),

			//new SolidColorBrush(Color.FromRgb(0, 0, 0)),
			//new SolidColorBrush(Color.FromRgb(32, 32, 32)),
			new SolidColorBrush(Color.FromRgb(64, 64, 64)),
			new SolidColorBrush(Color.FromRgb(96, 96, 96)),
			new SolidColorBrush(Color.FromRgb(128, 128, 128)),
			new SolidColorBrush(Color.FromRgb(160, 160, 160)),
			new SolidColorBrush(Color.FromRgb(192, 192, 192)),
			new SolidColorBrush(Color.FromRgb(224, 224, 224)),
			new SolidColorBrush(Color.FromRgb(255, 255, 255)),
		};

		public ColorPicker()
		{
			InitializeComponent();

			ColorPalletControl.ItemsSource = ColorList;
		}

		/// <summary>
		/// Funkcja przekazująca wybrany kolor do DB.Color skąd jest ogólno dostępna
		/// </summary>
		private void ColorLabel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DB.Color = ((Label)sender).Background;
		}
	}
}


