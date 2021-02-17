using System.Windows;

namespace GwentBot.GUI
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
            Topmost = false;  //不置顶
            gBot = new Bot();
            gBot.GameStatusChanged += (string msg) =>
            {
                Dispatcher.Invoke(() =>
                {
                    tbGlobalStateList.Text = msg;
                });
            };
        }
        /// <summary>
        /// 启用游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            // 异步开始工作
            gBot.StartWorkAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gBot.StopWork();
        }

        private void btSetTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            this.btSetTopMost.Content = this.Topmost?"取消置顶":"置顶";
        }
    }
}