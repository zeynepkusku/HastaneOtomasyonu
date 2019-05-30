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
    public class RandevuDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public RandevuDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }


        public int RandevuEkle(RandevuEntity randevu)
        {
            cmd = new SqlCommand("Insert Into Randevular Values(@HastaID,@DoktorID,@PoliklinikID,@RandevuTarihi,@RandevuDurumu,@RandevuSaati)", conn);

            AddParametersToCommand(randevu);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;


        }
        public List<HastaEntity> RandevuHastasi(int doktorID, DateTime randevuTarihi)
        {
            List<HastaEntity> hastalar = new List<HastaEntity>();
            cmd = new SqlCommand("select h.* from Randevular r join Hastalar h on h.HastaID = r.HastaID where DoktorID = @doktorID and RandevuTarihi = @randevuTarihi", conn);

            cmd.Parameters.AddWithValue("@doktorID", doktorID);
            cmd.Parameters.AddWithValue("@randevuTarihi", randevuTarihi);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    hastalar.Add(new HastaEntity()
                    {


                        HastaID = reader.GetInt32(0),
                        HastaTC = reader.GetString(1),
                        HastaAd = reader.GetString(2),
                        HastaSoyad = reader.GetString(3),
                        HastaDTarihi = reader.GetDateTime(4),
                        HastaTelefon = reader.GetString(5),
                        HastaCinsiyet = Convert.ToChar(reader[6]),
                        MedeniHal = Convert.ToChar(reader[7])


                    });


                }
                reader.Close();


                return hastalar;

            }
            catch
            {
                return hastalar;
            }
            finally
            {
                conn.Close();
            }



        }
        void AddParametersToCommand(RandevuEntity randevu)
        {
            cmd.Parameters.AddWithValue("@RandevuID", randevu.RandevuID);
            cmd.Parameters.AddWithValue("@HastaID", randevu.HastaID);
            cmd.Parameters.AddWithValue("@DoktorID", randevu.DoktorID);
            cmd.Parameters.AddWithValue("@PoliklinikID", randevu.PoliklinikID);
            cmd.Parameters.AddWithValue("@RandevuTarihi", randevu.RandevuTarihi);
            cmd.Parameters.AddWithValue("@RandevuDurumu", randevu.RandevuDurumu);
            cmd.Parameters.AddWithValue("@RandevuSaati", randevu.RandevuSaati);

        }
        
         
            public int RandevuGuncelle(RandevuEntity randevu)
        {
            cmd = new SqlCommand("Update Randevular Set HastaID=@HastaID,DoktorID=@DoktorID,PoliklinikID=@PoliklinikID,RandevuTarihi=@RandevuTarihi,RandevuDurum=@RandevuDurumu,RandevuSaati=@RandevuSaati where RandevuID=@RandevuID", conn);

         
            AddParametersToCommand(randevu);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }

      
        public int RandevuSil(int randevuID)
        {

            cmd = new SqlCommand("Delete From Randevular Where RandevuID=@RandevuID", conn);
            cmd.Parameters.AddWithValue("@RandevuID", randevuID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public List<RandevuEntity> DoktorunRandevulari(int doktorID, DateTime randevuTarihi)
        {
            List<RandevuEntity> randevular = new List<RandevuEntity>();
            cmd = new SqlCommand("select * from Randevular where doktorID=@doktorID and RandevuTarihi=@randevuTarihi", conn);
            cmd.Parameters.AddWithValue("@doktorID", doktorID);
            cmd.Parameters.AddWithValue("@randevuTarihi", randevuTarihi);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    randevular.Add(new RandevuEntity()
                    {

                        RandevuID = reader.GetInt32(0),
                        HastaID = reader.GetInt32(1),
                        DoktorID = reader.GetInt32(2),
                        PoliklinikID = reader.GetInt32(3),
                        RandevuTarihi = reader.GetDateTime(4),
                        RandevuDurumu = reader.GetBoolean(5),
                        RandevuSaati = reader.GetString(6)

                    });


                }
                reader.Close();


                return randevular;

            }
            catch
            {
                return randevular;
            }
            finally
            {
                conn.Close();
            }


        }
        public List<HastaRandevuEntity> HastaninRandevulari(int hastaID)
        {
            List<HastaRandevuEntity> randevular = new List<HastaRandevuEntity>();
            cmd = new SqlCommand("select h.HastaAdi +' '+ h.HastaSoyadi [Hasta Bilgisi],hn.HastaneAdi,dp.DepartmanAdi,p.PoliklinikAdi, d.DoktorAdi+' '+d.DoktorSoyadi [Doktor Bigisi],RandevuTarihi,RandevuSaati,RandevuDurum,r.RandevuID,r.HastaID,r.DoktorID,r.PoliklinikID from randevular r join Hastalar h on h.HastaID = r.HastaID join Doktorlar d on d.DoktorID = r.DoktorID join Poliklinikler p on p.PoliklinikID = r.PoliklinikID join Departmanlar dp on dp.DepartmanID = d.DepartmanID join Hastaneler hn on hn.HastaneID = dp.DepartmanID where h.hastaID = @hastaID", conn);

            cmd.Parameters.AddWithValue("@hastaID", hastaID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    randevular.Add(new HastaRandevuEntity()
                    {

                        HastaBilgisi = reader.GetString(0),
                        HastaneAdi = reader.GetString(1),
                        DepartmanAdi = reader.GetString(2),
                        PoliklinikAdi = reader.GetString(3),
                        DoktorBilgisi = reader.GetString(4),
                        RandevuTarihi = reader.GetDateTime(5),
                        RandevuDurumu = reader.GetBoolean(7),
                        RandevuSaati = reader.GetString(6),
                        //Bu 4 ID formda gösterilmiyor Randevulara ulaşabilmek için eklendi
                        RandevuID = reader.GetInt32(8),
                        HastaID = reader.GetInt32(9),
                        DoktorID = reader.GetInt32(10),
                        PoliklinikID = reader.GetInt32(11)


                    });


                }
                reader.Close();


                return randevular;

            }
            catch
            {
                return randevular;
            }
            finally
            {
                conn.Close();
            }



        }
    }
}
