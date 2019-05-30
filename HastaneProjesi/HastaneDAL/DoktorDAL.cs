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
   public class DoktorDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public DoktorDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }


        public List<DoktorEntity> TumDoktorlar()
        {
            List<DoktorEntity> doktorlar = new List<DoktorEntity>();
            cmd = new SqlCommand("Select * From Doktorlar", conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    doktorlar.Add(new DoktorEntity()
                    {


                        DoktorID = (int)reader["DoktorID"],
                        DoktorTC = reader["DoktorTC"].ToString(),
                        DoktorAdi = reader["DoktorAdi"].ToString(),
                        DoktorSoyadi = reader["DoktorSoyadi"].ToString(),
                        DoktorDTarihi = (DateTime)reader["DoktorDtarihi"],
                        DoktorTelefon = reader["DoktorTelefon"].ToString(),
                        DoktorCinsiyet = Convert.ToChar(reader["DoktorCinsiyet"]),
                        DoktorEmail = reader["DoktorEmail"].ToString(),
                        DoktorSifre = reader["DoktorSifre"].ToString()


                    });

                }
                reader.Close();
                return doktorlar;
            }
            catch
            {
                return doktorlar;
            }
            finally
            {
                conn.Close();
            }
        }


        public DoktorEntity IDyeGoreDoktorGetir(int doktorID)
        {
            cmd = new SqlCommand("Select * From Doktorlar Where DoktorID = @DoktorID", conn);

            cmd.Parameters.AddWithValue("@DoktorID", doktorID);

            DoktorEntity currentDoktor = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentDoktor = new DoktorEntity()
                {

                    DoktorID = (int)reader["DoktorID"],
                    DoktorTC = reader["DoktorTC"].ToString(),
                    DoktorAdi = reader["DoktorAdi"].ToString(),
                    DoktorSoyadi = reader["DoktorSoyadi"].ToString(),
                    DoktorDTarihi = (DateTime)reader["DoktorDtarihi"],
                    DoktorTelefon = reader["DoktorTelefon"].ToString(),
                    DoktorCinsiyet = Convert.ToChar(reader["DoktorCinsiyet"]),
                    DoktorEmail = reader["DoktorEmail"].ToString(),
                    DoktorSifre = reader["DoktorSifre"].ToString()

                };
                reader.Close();
                return currentDoktor;
            }
            catch
            {
                return currentDoktor;
            }
            finally
            {
                conn.Close();
            }
        }

        public DoktorEntity MaileGoreDoktorGetir(string Email)
        {
            DoktorEntity doktor = new DoktorEntity();
            cmd = new SqlCommand("Select * From Doktorlar Where DoktorEmail = @mail", conn);
            cmd.Parameters.AddWithValue("@mail", Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            doktor.DoktorID = reader.GetInt32(0);
            doktor.DoktorAdi = reader.GetString(2);
            doktor.DoktorSoyadi = reader.GetString(3);
            


            reader.Close();
            return doktor;

        }

        public DoktorEntity DoktorGetirmece()
        {
            DoktorEntity doktor = new DoktorEntity();
            cmd = new SqlCommand("Select * From Doktorlar ", conn);
            
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            doktor.DoktorID = reader.GetInt32(0);
          



            reader.Close();
            return doktor;

        }

        void AddParametersToCommand(DoktorEntity doktor)
        {
            cmd.Parameters.AddWithValue("@DoktorID", doktor.DoktorID);
            cmd.Parameters.AddWithValue("@DoktorTC", doktor.DoktorTC);
            cmd.Parameters.AddWithValue("@DoktorAdi", doktor.DoktorAdi);
            cmd.Parameters.AddWithValue("@DoktorSoyadi", doktor.DoktorSoyadi);
            cmd.Parameters.AddWithValue("@DoktorDtarihi", doktor.DoktorDTarihi);
            cmd.Parameters.AddWithValue("@DoktorTelefon", doktor.DoktorTelefon);
            cmd.Parameters.AddWithValue("@DoktorCinsiyet", doktor.DoktorCinsiyet);
            cmd.Parameters.AddWithValue("@DoktorEmail", doktor.DoktorEmail);
            cmd.Parameters.AddWithValue("@DoktorSifre", doktor.DoktorSifre);


        }
        public List<string> DepartmanaGoreDoktorGetir(int departmanId)
        {
            List<string> doktorlar = new List<string>();
            cmd = new SqlCommand("select DoktorAdi from Doktorlar where DepartmanID=@departmanId", conn);
            cmd.Parameters.AddWithValue("@departmanId", departmanId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    doktorlar.Add(reader[0].ToString());

                }
                reader.Close();


                return doktorlar;

            }
            catch
            {
                return doktorlar;
            }
            finally
            {
                conn.Close();
            }


        }

    }
}
