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
   public class PoliklinikDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public PoliklinikDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }

        public List<PoliklinikEntity> TumPoliklinikler()
        {
            List<PoliklinikEntity> poliklinikler = new List<PoliklinikEntity>();
            cmd = new SqlCommand("Select * From Poliklinikler", conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    poliklinikler.Add(new PoliklinikEntity()
                    {

                        PoliklinikID = (int)reader["PoliklinikID"],
                        PoliklinikAdi = reader["PoliklinikAdi"].ToString(),

                    });


                }
                reader.Close();


                return poliklinikler;

            }
            catch
            {
                return poliklinikler;
            }
            finally
            {
                conn.Close();
            }

        }
        public PoliklinikEntity IDyeGorePoliklinikGetir(int poliklinikID)
        {
            cmd = new SqlCommand("Select * From Poliklinikler Where PoliklinikID = @PoliklinikID", conn);

            cmd.Parameters.AddWithValue("@PoliklinikID", poliklinikID);

            PoliklinikEntity currentPoliklinik = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentPoliklinik = new PoliklinikEntity()
                {
                    PoliklinikID = (int)reader["PoliklinikID"],
                    PoliklinikAdi = reader["PoliklinikAdi"].ToString(),
                    HastaneID = (int)reader["HastaneID"]

                };
                reader.Close();
                return currentPoliklinik;
            }
            catch
            {
                return currentPoliklinik;
            }
            finally
            {
                conn.Close();
            }
        }



        public List<string> HastaneyeGorePoliklinikGetir(int hastaneId)
        {
            List<string> poliklinikler = new List<string>();
            cmd = new SqlCommand("Select PoliklinikAdi From HastanePoliklinik h Join Poliklinikler p On h.PoliklinikID = p.PoliklinikID Where HastaneID =@hastaneId", conn);
            cmd.Parameters.AddWithValue("@hastaneId", hastaneId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    poliklinikler.Add(reader[0].ToString());

                }
                reader.Close();


                return poliklinikler;

            }
            catch
            {
                return poliklinikler;
            }
            finally
            {
                conn.Close();
            }

            
        }

        public int IsmeGorePoliklinikGetir(string poliklinikAd)
        {
            PoliklinikEntity poliklinik = new PoliklinikEntity();
            cmd = new SqlCommand("Select * From Poliklinikler Where PoliklinikAdi=@poliklinikAdi", conn);
            cmd.Parameters.AddWithValue("@poliklinikAdi", poliklinikAd);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            poliklinik.PoliklinikID = reader.GetInt32(0);

            reader.Close();
            return poliklinik.PoliklinikID;


        }


    }
}
