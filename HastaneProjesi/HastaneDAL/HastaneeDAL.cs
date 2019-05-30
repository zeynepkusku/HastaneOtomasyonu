using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneDAL
{
   public class HastaneeDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public HastaneeDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }


        public List<HastaneeEntity> TumHastaneler()
        {
            List<HastaneeEntity> hastaneler = new List<HastaneeEntity>();
            cmd = new SqlCommand("Select * From Hastaneler", conn);



            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    hastaneler.Add(new HastaneeEntity()
                    {


                        HastaneID = (int)reader["HastaneID"],
                        HastaneAdi = reader["HastaneAdi"].ToString()

                    });


                }
                reader.Close();

             
                return hastaneler;

            }
            catch
            {
                return hastaneler;
            }
            finally
            {
                conn.Close();
            }
           
        }

        public HastaneeEntity IDyeGoreHastaneGetir(int hastaneID)
        {
            cmd = new SqlCommand("Select * From Hastaneler Where HastaneID = @HastaneID", conn);

            cmd.Parameters.AddWithValue("@HastaneID", hastaneID);

            HastaneeEntity currentHastane = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentHastane = new HastaneeEntity()
                {
                    HastaneID = (int)reader["HastaneID"],
                    HastaneAdi = reader["HastaneAdi"].ToString()

                };
                reader.Close();
                return currentHastane;
            }
            catch
            {
                return currentHastane;
            }
            finally
            {
                conn.Close();
            }
        }


        public int IsmeGoreHastaneGetir(string hastaneAd)
        {
            HastaneeEntity hastane = new HastaneeEntity();
            cmd = new SqlCommand("Select * From Hastaneler Where HastaneAdi=@hastaneAdi", conn);
            cmd.Parameters.AddWithValue("@hastaneAdi", hastaneAd);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            hastane.HastaneID = reader.GetInt32(0);

            reader.Close();
            return hastane.HastaneID;

            
        }
    }
}
