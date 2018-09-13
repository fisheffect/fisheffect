using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using fisheffect_api.Models;
using MySql.Data.MySqlClient;

namespace fisheffect_api.Dao
{
    public class FishForSaleDao
    {
        private static T Connect<T>(Func<MySqlConnection, T> function)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=fisheffect;port=3306;password=root");
            T resp = default(T);

            try
            {
                conn.Open();
                resp = function(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return resp;
        }

        public IEnumerable<FishForSale> GetFishFishForSales(int page = 0)
        {
            return Connect((conn) => conn.Query<FishForSale>("SELECT hashFish, hashReef, value FROM fish_for_sale LIMIT @page, 10 ", new { page = page * 10 }));
        }

        public void AddFishForSale(FishForSale fish)
        {
            Connect((conn) => conn.Execute(@"INSERT fish_for_sale (hashFish, hashReef, value) VALUES (@hashFish, @hashReef, @value)", fish));
        }
    }
}