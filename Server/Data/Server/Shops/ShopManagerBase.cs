﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Text;
using PMU.DatabaseConnector.MySql;
using PMU.DatabaseConnector;
using Server.Database;

namespace Server.Shops
{
    public class ShopManagerBase
    {
        static ShopCollection shops;

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        public static ShopCollection Shops {
            get { return shops; }
        }

        public static void Initialize() {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(num) FROM shop";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();
                shops = new ShopCollection(count);
            }
        }

        #region Loading

        public static void LoadShops(object object1) {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                try
                {
                    for (int i = 1; i <= shops.MaxShops; i++)
                    {
                        LoadShop(i, dbConnection.Database);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, shops.MaxShops));
                    }
                    if (LoadComplete != null)
                        LoadComplete(null, null);
                }
                catch (Exception ex)
                {
                    Exceptions.ErrorLogger.WriteToErrorLog(ex);
                }
            }
        }

        public static void LoadShop(int shopNum, MySql database)
        {
            if (shops.Shops.ContainsKey(shopNum) == false)
                shops.Shops.Add(shopNum, new Shop());

            string query = "SELECT name, " +
                "greeting, " +
                "farewell " +
                "FROM shop WHERE shop.num = \'" + shopNum + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                shops[shopNum].Name = row["name"].ValueString;
                shops[shopNum].JoinSay = row["greeting"].ValueString;
                shops[shopNum].LeaveSay = row["farewell"].ValueString;
            }

            query = "SELECT trade_num, " +
                "item, " +
                "cost_num, " +
                "cost_val " +
                "FROM shop_trade WHERE shop_trade.num = \'" + shopNum + "\'";

            foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
            {
                int tradeNum = columnCollection["trade_num"].ValueString.ToInt();

                shops[shopNum].Items[tradeNum].GetItem = columnCollection["item"].ValueString.ToInt();
                shops[shopNum].Items[tradeNum].GiveItem = columnCollection["cost_num"].ValueString.ToInt();
                shops[shopNum].Items[tradeNum].GiveValue = columnCollection["cost_val"].ValueString.ToInt();
            }

        }
        
        
        #endregion

        #region Saving


        public static void SaveShop(int shopNum)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                MySql database = dbConnection.Database;
                database.BeginTransaction();

                database.ExecuteNonQuery("DELETE FROM shop WHERE num = \'" + shopNum + "\'");
                database.ExecuteNonQuery("DELETE FROM shop_trade WHERE num = \'" + shopNum + "\'");

                database.UpdateOrInsert("shop", new IDataColumn[] {
                database.CreateColumn(false, "num", shopNum.ToString()),
                database.CreateColumn(false, "name", shops[shopNum].Name),
                database.CreateColumn(false, "greeting", shops[shopNum].JoinSay),
                database.CreateColumn(false, "farewell", shops[shopNum].LeaveSay)
            });

                for (int i = 0; i < Constants.MAX_TRADES; i++)
                {
                    database.UpdateOrInsert("shop_trade", new IDataColumn[] {
                    database.CreateColumn(false, "num", shopNum.ToString()),
                    database.CreateColumn(false, "trade_num", i.ToString()),
                    database.CreateColumn(false, "item", shops[shopNum].Items[i].GetItem.ToString()),
                    database.CreateColumn(false, "cost_num", shops[shopNum].Items[i].GiveItem.ToString()),
                    database.CreateColumn(false, "cost_val", shops[shopNum].Items[i].GiveValue.ToString())
                });
                }
                database.EndTransaction();
            }
        }

        

        #endregion
    }
}
