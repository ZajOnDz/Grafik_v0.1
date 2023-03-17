using System.Windows;

namespace Grafik_v0._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Przełącza tryb wybierania godzin pracy lub kolorowania
        public bool Coloring { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            DB.ReadDB();

            grafikControlsList.ItemsSource = RWFile.grafikList;
        }
    }
}
