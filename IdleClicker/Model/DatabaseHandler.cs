using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace IdleClicker.Model
{
    class DatabaseHandler
    {
        private string connectionString = "data source=CV-BB-5991;initial catalog=IdleClicker;trusted_connection=true";

        private DataSet Execute(string query)
        {

            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection
            (connectionString))))
            {

                // Open conn, execute query, close conn, wrap result in DataSet:
                adapter.Fill(resultSet);
            }
            return resultSet;
        }

        public Player GetPlayer()
        {
            Player player = new Player();
            string PlayerQuery = "SELECT * FROM Table_Players WHERE Id = 1";

            try
            {
                // Perform query and save result in variable:
                DataSet resultSet = Execute(PlayerQuery);

                // Get the first table of the data set, and save in variable:
                DataTable playersTable = resultSet.Tables[0];

                // Iterate through the rows of the table, and extract the data,
                // and create a new person object each time, and add that to the list of persons.
                foreach (DataRow personRow in playersTable.Rows)
                {
                    player.Money = (int)personRow["Money"];
                    player.MoneyPerClick = (int)personRow["MoneyPerClick"];
                    player.BuyMoneyIncreasePerClickPrice = (int)personRow["MoneyIncreasePrice"];
                    player.BuyAutoClickerPrice = (int)personRow["AutoClickerPrice"];
                    player.BuyUpgradeAutoClickerPrice = (int)personRow["AutoClickerUpgradePrice"];
                    for (int i = 0; i < (int)personRow["AutoClickerCount"]; i++)
                    {
                        AutoClicker autoClicker = new AutoClicker((int)personRow["AutoClickerMoneyPerClick"]);
                        player.AutoClickerList.Add(autoClicker);
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database.", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return player;
        }

        public void UpdatePlayer(Player player)
        {
            string updatePlayerQuery = "";
            if (player.AutoClickerList.Count > 0)
            {
                updatePlayerQuery =
                $"UPDATE Table_Players SET Money = '{player.Money}', MoneyPerClick = '{player.MoneyPerClick}'," +
                $" MoneyIncreasePrice = '{player.BuyMoneyIncreasePerClickPrice}', AutoClickerPrice = '{player.BuyAutoClickerPrice}', AutoClickerUpgradePrice = '{player.BuyUpgradeAutoClickerPrice}'," +
                $" AutoClickerCount = '{player.AutoClickerList.Count}', AutoClickerMoneyPerClick = '{player.AutoClickerList[0].MoneyPerClick}' WHERE Id = {1}";
            }
            else if (player.AutoClickerList.Count == 0)
            {
                updatePlayerQuery =
                $"UPDATE Table_Players SET Money = '{player.Money}', MoneyPerClick = '{player.MoneyPerClick}'," +
                $" MoneyIncreasePrice = '{player.BuyMoneyIncreasePerClickPrice}', AutoClickerPrice = '{player.BuyAutoClickerPrice}', AutoClickerUpgradePrice = '{player.BuyUpgradeAutoClickerPrice}'," +
                $" AutoClickerCount = '{player.AutoClickerList.Count}', AutoClickerMoneyPerClick = '{1}' WHERE Id = {1}";
            }
            try
            {
                Execute(updatePlayerQuery);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database and save data.", "Warning Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
