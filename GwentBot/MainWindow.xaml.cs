using System.Windows;

namespace GwentBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bot gBot;

        public MainWindow()
        {
            InitializeComponent();
            gBot = new Bot();
            gBot.GlobalGameStatusChanged += (string msg) =>
            {
                Dispatcher.Invoke(() =>
                {
                    tbGlobalStateList.Text = msg;
                });
            };
        }

        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            gBot.StartWorkAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gBot.StopWork();
        }
    }
}
