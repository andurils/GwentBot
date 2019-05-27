// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
using GwentBot;

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
            gBot = new Bot();
            gBot.GameStatusChanged += (string msg) =>
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
