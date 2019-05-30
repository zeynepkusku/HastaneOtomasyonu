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
    public class DepartmanDAL
    {
        SqlConnection conn;
        SqlCommand cmd;

        public DepartmanDAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.HST);
        }


        public List<DepartmanEntity> TumDepartmanlar()
        {
            List<DepartmanEntity> departmanlar = new List<DepartmanEntity>();
            cmd = new SqlCommand("Select * From Departmanlar", conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    departmanlar.Add(new DepartmanEntity()
                    {


                        DepartmanID = (int)reader["DepartmanID"],
                        DepartmanAdi = reader["DepartmanAdi"].ToString()

                    });


                }
                reader.Close();
                return departmanlar;
            }
            catch
            {
                return departmanlar;
            }
            finally
            {
                conn.Close();
            }
        }

        public DepartmanEntity IDyeGoreDepartmanGetir(int departmanID)
        {
            cmd = new SqlCommand("Select * From Departmanlar Where DepartmanID = @DepartmanID", conn);

            cmd.Parameters.AddWithValue("@DepartmanID", departmanID);

            DepartmanEntity currentDepartman = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();



                currentDepartman = new DepartmanEntity()
                {
                    DepartmanID = (int)reader["DepartmanID"],
                    DepartmanAdi = reader["DepartmanAdi"].ToString()

                };
                reader.Close();
                return currentDepartman;
            }
            catch
            {
                return currentDepartman;
            }
            finally
            {
                conn.Close();
            }
        }



        public List<DepartmanEntity> PoliklinigeGoreDepartmanGetir(string poliklinikAdi)
        {
            List<DepartmanEntity> departmanlar = new List<DepartmanEntity>();
            cmd = new SqlCommand(@"SELECT DepartmanAdi
                                     FROM Departmanlar join Poliklinikler
                                           ON Departmanlar.PoliklinikID = Poliklinikler.PoliklinikID
                                                 where PoliklinikAdi = @poliklinikAdi", conn);

            cmd.Parameters.AddWithValue("@poliklinikAdi", poliklinikAdi);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    departmanlar.Add(new DepartmanEntity()
                    {
                        DepartmanID = reader.GetInt32(0),
                        DepartmanAdi = reader.GetString(1),



                    });


                }
                reader.Close();


                return departmanlar;

            }
            catch
            {
                return departmanlar;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<string> PoliklinigeGoreDepartmanGetir(int poliklinikID)
        {
            List<string> departmanlar = new List<string>();
            cmd = new SqlCommand("select DepartmanAdi from PoliklinikDepartman p JOIN Departmanlar d ON p.DepartmanID=d.DepartmanID where p.PoliklinikID=@poliklinikID", conn);
            cmd.Parameters.AddWithValue("@poliklinikID", poliklinikID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {

                    departmanlar.Add(reader[0].ToString());

                }
                reader.Close();


                return departmanlar;

            }
            catch
            {
                return departmanlar;
            }
            finally
            {
                conn.Close();
            }


        }

        public int IsmeGoreDepartmanGetir(string departmanAd)
        {
            DepartmanEntity departman = new DepartmanEntity();
            cmd = new SqlCommand("Select * From Departmanlar Where DepartmanAdi=@departmanAdi", conn);
            cmd.Parameters.AddWithValue("@departmanAdi", departmanAd);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            reader.Read();
            departman.DepartmanID = reader.GetInt32(0);

            reader.Close();
            return departman.DepartmanID;


        }

    }
}
