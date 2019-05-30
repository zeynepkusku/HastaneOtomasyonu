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
   public class IlacDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public IlacDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }

        public int IlacEkle(IlacEntity ilac)
        {
            cmd = new SqlCommand("Insert Into Ilaclar Values(@IlacID,@IlacAdi)", conn);

            AddParametersToCommand(ilac);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }

        public int IlacGuncelle(IlacEntity ilac)
        {
            cmd = new SqlCommand("Update Ilaclar Set IlacID=@IlacID, IlacAdi=@IlacAdi", conn);


            AddParametersToCommand(ilac);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }
   
        public int IlacSil(int ilacID)
        {

            cmd = new SqlCommand("Delete From Ilaclar Where IlacID=@IlacID", conn);
            cmd.Parameters.AddWithValue("@IlacID", ilacID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }

        void AddParametersToCommand(IlacEntity ilac)
        {
            cmd.Parameters.AddWithValue("@IlacID", ilac.IlacID);
            cmd.Parameters.AddWithValue("@IlacAdi", ilac.IlacAdi);
            

        }

        public List<IlacEntity> TumIlaclar()
        {
            List<IlacEntity> ilaclar = new List<IlacEntity>();
            cmd = new SqlCommand("Select * From Ilaclar", conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    ilaclar.Add(new IlacEntity()
                    {


                        IlacID = (int)reader["IlacID"],
                        IlacAdi = reader["IlacAdi"].ToString()
                       
                    });


                }
                reader.Close();
                return ilaclar;
            }
            catch
            {
                return ilaclar;
            }
            finally
            {
                conn.Close();
            }
        }

        public IlacEntity IDyeGoreIlacGetir(int ilacID)
        {
            cmd = new SqlCommand("Select * From Ilaclar Where IlacID = @IlacID", conn);

            cmd.Parameters.AddWithValue("@IlacID", ilacID);

            IlacEntity currentIlac = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentIlac = new IlacEntity()
                {
                    IlacID = (int)reader["IlacID"],
                    IlacAdi = reader["IlacAdi"].ToString()
                

                };
                reader.Close();
                return currentIlac;
            }
            catch
            {
                return currentIlac;
            }
            finally
            {
                conn.Close();
            }
        }
        public IlacEntity ReceteIlaclari(string receteNo)
        {
            IlacEntity receteIlaclari = new IlacEntity();
            cmd = new SqlCommand("Select i.IlacAdi from Ilaclar i Join Receteler re on i.IlacID=re.IlacID Where re.ReceteNo=@receteNo", conn);
            cmd.Parameters.AddWithValue("@receteNo", receteNo);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (reader.Read())
            {
                receteIlaclari.IlacAdi = reader.GetString(0);
            }
            reader.Close();
            return receteIlaclari;

            
        }

    }
}
