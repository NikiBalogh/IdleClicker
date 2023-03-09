using System;
using System.Collections.Generic;
using System.Text;

namespace IdleClicker.Model
{
    class Player
    {
        int money = 0;
        int moneyPerClick = 1;
        int buyMoneyIncreasePerClickPrice = 5;
        int buyAutoClickerPrice = 15;
        int buyUpgradeAutoClickerPrice = 16;
        List<AutoClicker> autoClickerList = new List<AutoClicker>();

        public Player(int money, int moneyPerClick, int buyMoneyIncreasePerClickPrice, int buyAutoClickerPrice, int buyUpgradeAutoClickerPrice, List<AutoClicker> autoClickerList)
        {
            Money = money;
            MoneyPerClick = moneyPerClick;
            BuyMoneyIncreasePerClickPrice = buyMoneyIncreasePerClickPrice;
            BuyAutoClickerPrice = buyAutoClickerPrice;
            BuyUpgradeAutoClickerPrice = buyUpgradeAutoClickerPrice;
            AutoClickerList = autoClickerList;
        }

        public Player()
        {

        }

        public int Money { get => money; set => money = value; }
        public int MoneyPerClick { get => moneyPerClick; set => moneyPerClick = value; }
        public int BuyMoneyIncreasePerClickPrice { get => buyMoneyIncreasePerClickPrice; set => buyMoneyIncreasePerClickPrice = value; }
        public int BuyAutoClickerPrice { get => buyAutoClickerPrice; set => buyAutoClickerPrice = value; }
        public int BuyUpgradeAutoClickerPrice { get => buyUpgradeAutoClickerPrice; set => buyUpgradeAutoClickerPrice = value; }
        internal List<AutoClicker> AutoClickerList { get => autoClickerList; set => autoClickerList = value; }

        public void PlayerClick()
        {
            money += MoneyPerClick;
        }

        public void AutoClickerClick()
        {
            foreach (AutoClicker autoClicker in AutoClickerList)
            {
                money += autoClicker.MoneyPerClick;
            }
        }

        public void BuyMoneyIncreasePerClick()
        {
            if (money >= buyMoneyIncreasePerClickPrice)
            {
                money -= buyMoneyIncreasePerClickPrice;
                MoneyPerClick *= 2;
                buyMoneyIncreasePerClickPrice *= 4;
            }
        }

        public void BuyAutoClicker()
        {
            if (money >= buyAutoClickerPrice)
            {
                money -= buyAutoClickerPrice;
                AutoClicker autoclicker = AutoClickerList.Count < 1 ? new AutoClicker(1) : new AutoClicker(AutoClickerList[0].MoneyPerClick);
                AutoClickerList.Add(autoclicker);
                BuyAutoClickerPrice *= 2;
            }
        }

        public void UpgradeAutoClicker()
        {
            if (money >= BuyUpgradeAutoClickerPrice && AutoClickerList.Count >= 1)
            {
                money -= BuyUpgradeAutoClickerPrice;
                foreach (AutoClicker autoClicker in AutoClickerList)
                {
                    autoClicker.MoneyPerClick *= 2;
                }
                buyUpgradeAutoClickerPrice *= 6;
            }
        }
    }
}
