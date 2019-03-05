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

namespace GwentBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bot gBot;

        public MainWindow()
        {
            InitializeComponent();
            gBot = new Bot();
        }

        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            gBot.StartWorkAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var visability = gBot.IsGameWindowFullVisible();
            if (visability)
                btCheckVisability.Background = new SolidColorBrush(Colors.Green);
            else
                btCheckVisability.Background = new SolidColorBrush(Colors.Red);
        }
    }
}
