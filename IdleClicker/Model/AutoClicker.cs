using System;
using System.Collections.Generic;
using System.Text;

namespace IdleClicker.Model
{
    class AutoClicker
    {
        int moneyPerClick;
        public AutoClicker(int moneyPerClick)
        {
            MoneyPerClick = moneyPerClick;
        }

        public int MoneyPerClick { get => moneyPerClick; set => moneyPerClick = value; }

    }
}
