using System;
using System.Collections.Generic;
using System.Text;
using IdleClicker.Model;

namespace IdleClicker.Controller
{
    class Controller
    {
        Player currentPlayer = new Player();
        DatabaseHandler databaseHandler = new DatabaseHandler();

        internal Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public void GetStartInfo()
        {
            currentPlayer = databaseHandler.GetPlayer();
        }

        public void UpdatePlayer()
        {
            databaseHandler.UpdatePlayer(currentPlayer);
        }

        public void ResetPlayer()
        {
            currentPlayer = new Player();
            databaseHandler.UpdatePlayer(currentPlayer);
        }
    }
}
