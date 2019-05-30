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
   public class TahlilDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public TahlilDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }

      
        public int TahlilEkle(TahlilEntity tahlil)
        {
            cmd = new SqlCommand("Insert Into Tahliller Values(@TahlilID,@TahlilAdi)", conn);

            AddParametersToCommand(tahlil);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }
       
        public int TahlilGuncelle(TahlilEntity tahlil)
        {
            cmd = new SqlCommand("Update Tahliller Set TahlilID=@TahlilID, TahlilAdi=@TahlilAdi", conn);


            AddParametersToCommand(tahlil);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }
      
        public int TahlilSil(int tahlilID)
        {

            cmd = new SqlCommand("Delete From Tahliller Where TahlilID=@TahlilID", conn);
            cmd.Parameters.AddWithValue("@TahlilID", tahlilID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }

        void AddParametersToCommand(TahlilEntity tahlil)
        {
            cmd.Parameters.AddWithValue("@TahlilID", tahlil.TahlilID);
            cmd.Parameters.AddWithValue("@TahlilAdi", tahlil.TahlilAdi);


        }

        public List<TahlilEntity> TumTahliller()
        {
            List<TahlilEntity> tahliller = new List<TahlilEntity>();
            cmd = new SqlCommand("Select * From Tahliller", conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    tahliller.Add(new TahlilEntity()
                    {


                        TahlilID = (int)reader["TahlilID"],
                        TahlilAdi = reader["TahlilAdi"].ToString()

                    });


                }
                reader.Close();
                return tahliller;
            }
            catch
            {
                return tahliller;
            }
            finally
            {
                conn.Close();
            }
        }

        public TahlilEntity IDyeGoreTahlilGetir(int tahlilID)
        {
            cmd = new SqlCommand("Select * From Tahliller Where TahlilID = @TahlilID", conn);

            cmd.Parameters.AddWithValue("@TahlilID", tahlilID);

            TahlilEntity currentTahlil = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentTahlil = new TahlilEntity()
                {
                    TahlilID = (int)reader["TahlilID"],
                    TahlilAdi = reader["TahlilAdi"].ToString()

                };
                reader.Close();
                return currentTahlil;
            }
            catch
            {
                return currentTahlil;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
