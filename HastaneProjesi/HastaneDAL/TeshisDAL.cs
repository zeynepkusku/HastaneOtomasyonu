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
    public class TeshisDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public TeshisDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }

      
        public int TeshisEkle(TeshislerEntity teshis)
        {
            cmd = new SqlCommand("Insert Into Teshisler Values(@TeshisID,@TeshisAdi)", conn);

            AddParametersToCommand(teshis);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }
       
        public int TeshisGuncelle(TeshislerEntity teshis)
        {
            cmd = new SqlCommand("Update Teshisler Set TeshisID=@TeshisID, TeshisAdi=@TeshisAdi", conn);


            AddParametersToCommand(teshis);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }
       
        public int TeshisSil(int teshisID)
        {

            cmd = new SqlCommand("Delete From Teshisler Where TeshisID=@TeshisID", conn);
            cmd.Parameters.AddWithValue("@TeshisID", teshisID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }

        void AddParametersToCommand(TeshislerEntity teshis)
        {
            cmd.Parameters.AddWithValue("@TeshisID", teshis.TeshisID);
            cmd.Parameters.AddWithValue("@TeshisAdi", teshis.TeshisAdi);


        }

        public List<TeshislerEntity> TumTeshisler(string tc)
        {
            List<TeshislerEntity> teshisler = new List<TeshislerEntity>();
            string sorgu = "select TeshisAdi,DoktorNotu,RandevuDurum,IlacAdi,DigerIlac,HastaTC from Ilaclar i join Receteler re on i.IlacID = re.IlacID  join Randevular randevu  on randevu.RandevuID = re.RandevuID join Muayeneler mu  on mu.RandevuID = randevu.RandevuID join Teshisler t  on t.TeshisID = mu.TeshisID join Hastalar has on has.HastaID = randevu.HastaID where has.HastaTC = @tc";
            cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@tc", tc);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    teshisler.Add(new TeshislerEntity()
                    {
                        TeshisAdi = reader["TeshisAdi"].ToString(),


                    });
                    
                    


                }
                reader.Close();
                return teshisler;
            }
            catch
            {
                return teshisler;
            }
            finally
            {
                conn.Close();
            }
        }

        public TeshislerEntity IDyeGoreTeshisGetir(int teshisID)
        {
            cmd = new SqlCommand("Select * From Teshisler Where TeshisID = @TeshisID", conn);

            cmd.Parameters.AddWithValue("@TeshisID", teshisID);

            TeshislerEntity currentTeshis = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentTeshis = new TeshislerEntity()
                {
                    TeshisID = (int)reader["TeshisID"],
                    TeshisAdi = reader["TeshisAdi"].ToString()

                };
                reader.Close();
                return currentTeshis;
            }
            catch
            {
                return currentTeshis;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
