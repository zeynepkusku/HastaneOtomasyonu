using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneDAL
{

    public class HastaDAL
    {
        SqlConnection conn;
        SqlCommand cmd;
    
        public HastaDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }


        public int HastaEkle(HastaEntity hasta)
        {
            cmd = new SqlCommand("Insert Into Hastalar Values(@HastaTC,@HastaAdi,@HastaSoyadi,@HastaDTarihi,@HastaTelefon,@Cinsiyet,@MedeniHal,@HastaEmail,@HastaSifre)", conn);

            AddParametersToCommand(hasta);
            return ExecuteCommand(); ;


        }
        int ExecuteCommand()
        {
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
  
        public int HastaGuncelle(HastaEntity hasta)
        {
            cmd = new SqlCommand("Update Hastalar Set HastaTC=@HastaTC, HastaAdi=@HastaAdi,HastaSoyadi=@HastaSoyadi, HastaDTarihi=@HastaDTarihi, HastaTelefon=@HastaTelefon, Cinsiyet=@Cinsiyet, MedeniHal=@MedeniHal, HastaEmail=@HastaEmail, HastaSifre=@HastaSifre Where HastaID=@HastaID", conn);


            AddParametersToCommand(hasta);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }
       
        public int HastaSil(int hastaID)
        {

            cmd = new SqlCommand("Delete From Hastalar Where HastaID=@HastaID", conn);
            cmd.Parameters.AddWithValue("@HastaID", hastaID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }

        void AddParametersToCommand(HastaEntity hasta)
        {
            cmd.Parameters.AddWithValue("@HastaID", hasta.HastaID);
            cmd.Parameters.AddWithValue("@HastaTC", hasta.HastaTC);
            cmd.Parameters.AddWithValue("@HastaAdi", hasta.HastaAd);
            cmd.Parameters.AddWithValue("@HastaSoyadi", hasta.HastaSoyad);
            cmd.Parameters.AddWithValue("@HastaDtarihi", hasta.HastaDTarihi);
            cmd.Parameters.AddWithValue("@HastaTelefon", hasta.HastaTelefon);
            cmd.Parameters.AddWithValue("@Cinsiyet", hasta.HastaCinsiyet);
            cmd.Parameters.AddWithValue("@MedeniHal", hasta.MedeniHal);
            cmd.Parameters.AddWithValue("@HastaEmail", hasta.HastaEmail);
            cmd.Parameters.AddWithValue("@HastaSifre", hasta.HastaSifre);

        }

        public List<HastaEntity> TumHastalar()
        {
            List<HastaEntity> hastalar = new List<HastaEntity>();
            cmd = new SqlCommand("Select * From Hastalar", conn);


          
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {

                hastalar.Add(new HastaEntity
                    {
                    HastaID = reader.GetInt32(0),
                    HastaAd=reader.GetString(2),
                    HastaSoyad=reader.GetString(3),
                    HastaCinsiyet=Convert.ToChar(reader["Cinsiyet"]),
                    MedeniHal= Convert.ToChar(reader["MedeniHal"]),
                    HastaDTarihi=reader.GetDateTime(4),
                    HastaTelefon=reader.GetString(5),
                    HastaSifre = reader.GetString(9),
                    HastaTC = reader.GetString(1),
                    HastaEmail = reader.GetString(8)


                });

            }

            reader.Close();
            return hastalar;

        }

  

        public HastaEntity IDyeGoreHastaGetir(int hastaID)
        {
            HastaEntity hasta = new HastaEntity();
            cmd = new SqlCommand("Select * From Hastalar Where HastaID= id", conn);
            cmd.Parameters.AddWithValue("@id", hastaID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            hasta.HastaID = reader.GetInt32(0);
            hasta.HastaTC = reader.GetString(1);
            hasta.HastaAd = reader.GetString(2);
            hasta.HastaSoyad = reader.GetString(3);
            hasta.HastaDTarihi = reader.GetDateTime(4);
            hasta.HastaTelefon = reader.GetString(5);
            hasta.HastaCinsiyet = Convert.ToChar(reader[6]);
            hasta.MedeniHal = Convert.ToChar(reader[7]);

            hasta.HastaEmail = reader.GetString(8);
            hasta.HastaSifre = reader.GetString(9);



            reader.Close();
            return hasta;

        }

        public HastaEntity MaileGoreHastaGetir(string Email)
        {
            HastaEntity hasta = new HastaEntity();
            cmd = new SqlCommand("Select * From Hastalar Where HastaEmail = @mail", conn);
            cmd.Parameters.AddWithValue("@mail", Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();

            hasta.HastaID = reader.GetInt32(0);
            hasta.HastaTC = reader.GetString(1);
            hasta.HastaAd = reader.GetString(2);
            hasta.HastaSoyad = reader.GetString(3);
            hasta.HastaDTarihi = reader.GetDateTime(4);
            hasta.HastaTelefon = reader.GetString(5);
            hasta.HastaCinsiyet = Convert.ToChar(reader[6]);
            hasta.MedeniHal = Convert.ToChar(reader[7]);
   
            hasta.HastaEmail = reader.GetString(8);
            hasta.HastaSifre = reader.GetString(9);


            reader.Close();
            return hasta;

        }

       

        public List<HastaEntity> TariheGoreTumHastalar(DateTime tarih)
        {
            List<HastaEntity> hastalar = new List<HastaEntity>();

            cmd = new SqlCommand("select Hastalar.* from Randevular JOIN Hastalar ON Randevular.HastaID =Hastalar.HastaID  where RandevuTarihi=@tarih", conn);
            cmd.Parameters.AddWithValue("@tarih", tarih);


            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {

                hastalar.Add(new HastaEntity
                {
                    HastaID = reader.GetInt32(0),
                    HastaAd = reader.GetString(2),
                    HastaSoyad = reader.GetString(3),
                    HastaCinsiyet = Convert.ToChar(reader["Cinsiyet"]),
                    MedeniHal = Convert.ToChar(reader["MedeniHal"]),
                    HastaDTarihi = reader.GetDateTime(4),
                    HastaTelefon = reader.GetString(5),
                    HastaTC = reader.GetString(1)



                });
            }

            reader.Close();
            return hastalar;

        }

        public HastaEntity ReceteNoyaHastaGetir(string ReceteNo)
        {
            HastaEntity hasta = new HastaEntity();
            cmd = new SqlCommand("Select HastaTC,HastaAdi,HastaSoyadi,HastaDTarihi,MedeniHal,Cinsiyet From Receteler r join Randevular ra On r.RandevuID=ra.RandevuID Join Hastalar h on h.HastaID=ra.HastaID Where ReceteNo=@ReceteNo", conn);
            cmd.Parameters.AddWithValue("@ReceteNo",ReceteNo);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (reader.Read())
            {
                hasta.HastaTC = reader.GetString(0);
                hasta.HastaAd = reader.GetString(1);
                hasta.HastaSoyad = reader.GetString(2);
                hasta.HastaDTarihi = reader.GetDateTime(3);
                hasta.HastaCinsiyet =Convert.ToChar(reader[5]);
                hasta.MedeniHal = Convert.ToChar(reader[4]);
            }
            reader.Close();
            return hasta;

        }


    }
}
