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
using System.Windows.Threading;

namespace IdleClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller.Controller controller = new Controller.Controller();
        TimeSpan AlwaysSpeed = new TimeSpan(10000);
        TimeSpan EverySecond = new TimeSpan(0, 0, 1);
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer everySecondTimer = new DispatcherTimer();

        public MainWindow()
        {
            controller.GetStartInfo();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = AlwaysSpeed;
            everySecondTimer.Tick += new EventHandler(TimerEverySecond_Tick);
            everySecondTimer.Interval = EverySecond;
            timer.Start();
            everySecondTimer.Start();
            InitializeComponent();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            lblMoney.Content = controller.CurrentPlayer.Money;
            lblMoneyPerSec.Content = $"{controller.CurrentPlayer.AutoClickerList.Count * controller.CurrentPlayer.AutoClickerList[0].MoneyPerClick}/s";
            lblIncreaseMoneyPerClickPrice.Content = $"Price: {controller.CurrentPlayer.BuyMoneyIncreasePerClickPrice}";
            lblAutoClickerPrice.Content = $"Price: {controller.CurrentPlayer.BuyAutoClickerPrice}";
            lblUpgradeAutoClickerPrice.Content = $"Price: {controller.CurrentPlayer.BuyUpgradeAutoClickerPrice}";
        }
        private void TimerEverySecond_Tick(object sender, EventArgs e)
        {
            controller.CurrentPlayer.AutoClickerClick();
            controller.UpdatePlayer();
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            controller.CurrentPlayer.PlayerClick();
        }

        private void btnCloseShop_Click(object sender, RoutedEventArgs e)
        {
            gridShop.Visibility = Visibility.Collapsed;
            gridGame.Visibility = Visibility.Visible;
        }

        private void btnBuyMoneyIncreasePerClick_Click(object sender, RoutedEventArgs e)
        {
            controller.CurrentPlayer.BuyMoneyIncreasePerClick();
        }

        private void btnOpenShop_Click(object sender, RoutedEventArgs e)
        {
            gridGame.Visibility = Visibility.Collapsed;
            gridShop.Visibility = Visibility.Visible;
        }

        private void btnBuyAutoClicker_Click(object sender, RoutedEventArgs e)
        {
            controller.CurrentPlayer.BuyAutoClicker();
        }

        private void btnBuyUpgradeAutoClicker_Click(object sender, RoutedEventArgs e)
        {
            controller.CurrentPlayer.UpgradeAutoClicker();
        }

        private void btnResetGame_Click(object sender, RoutedEventArgs e)
        {
            controller.ResetPlayer();
        }
    }
}
