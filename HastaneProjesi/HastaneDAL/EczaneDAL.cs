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
    public class EczaneDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public EczaneDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }

      

        void AddParametersToCommand(EczaneEntity eczane)
        {
            cmd.Parameters.AddWithValue("@EczaneID", eczane.EczaneID);
            cmd.Parameters.AddWithValue("@EczaneAdi", eczane.EczaneAdi);
            cmd.Parameters.AddWithValue("@EczaneMail", eczane.EczaneEmail);
            cmd.Parameters.AddWithValue("@EczaneSifre", eczane.EczaneSifre);


        }

        public List<EczaneEntity> TumEczaneler()
        {
            List<EczaneEntity> eczaneler = new List<EczaneEntity>();
            cmd = new SqlCommand("Select * From Eczaneler", conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    eczaneler.Add(new EczaneEntity()
                    {


                        EczaneID = (int)reader["EczaneID"],
                        EczaneAdi = reader["EczaneAdi"].ToString(),
                        EczaneEmail = reader["EczaneEmail"].ToString(),
                        EczaneSifre = reader["EczaneSifre"].ToString()
                       

                    });
                    
                }
                reader.Close();
                return eczaneler;
            }
            catch
            {
                return eczaneler;
            }
            finally
            {
                conn.Close();
            }
        }

        public EczaneEntity IDyeGoreEczaneGetir(int eczaneID)
        {
            cmd = new SqlCommand("Select * From Eczaneler Where EczaneID = @EczaneID", conn);

            cmd.Parameters.AddWithValue("@EczaneID", eczaneID);

            EczaneEntity currentEczane = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentEczane = new EczaneEntity()
                {
                    EczaneID = (int)reader["EczaneID"],
                    EczaneAdi = reader["EczaneAdi"].ToString(),
                    EczaneEmail = reader["EczaneEmail"].ToString(),
                    EczaneSifre = reader["EczaneSifre"].ToString()

                };
                reader.Close();
                return currentEczane;
            }
            catch
            {
                return currentEczane;
            }
            finally
            {
                conn.Close();
            }
        }

        public EczaneEntity MaileGoreEczaneGetir(string Email)
        {
            EczaneEntity eczane = new EczaneEntity();
            cmd = new SqlCommand("Select * From Eczaneler Where EczaneEmail = @mail", conn);
            cmd.Parameters.AddWithValue("@mail", Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            eczane.EczaneID = reader.GetInt32(0);
            eczane.EczaneAdi = reader.GetString(1);
            eczane.EczaneEmail = reader.GetString(2);
            eczane.EczaneSifre = reader.GetString(3);
         
            reader.Close();
            return eczane;

        }
    }
}
