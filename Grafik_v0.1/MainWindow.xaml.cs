using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
