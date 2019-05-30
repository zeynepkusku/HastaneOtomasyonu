using HastaneBLL;
using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneUIWinForm
{

    public partial class frmHastaEkrani : Form
    {
        SqlConnection conn;
        DoktorDAL _doktorDAL;
        HastaneeDAL _hastaneeDAL;
        DepartmanDAL _departmanDAL;
        PoliklinikDAL _poliklinikDAL;
        HastaDAL _hastaDAL;
        HastaEntity hasta;
        HastaneeBLL hastaneeBLL;
        DepartmanBLL _departmanBLL;
        PoliklinikBLL _poliklinikBLL;
        RandevuDAL _randevuDAL;

        int hastaID;
        public frmHastaEkrani(int hastaID)
        {
            InitializeComponent();
            conn = new SqlConnection(Properties.Settings.Default.HST);
            _doktorDAL = new DoktorDAL();
            _hastaneeDAL = new HastaneeDAL();
            _departmanDAL = new DepartmanDAL();
            _poliklinikDAL = new PoliklinikDAL();
            _hastaDAL = new HastaDAL();
            hasta = new HastaEntity();
            hastaneeBLL = new HastaneeBLL();
            _departmanBLL = new DepartmanBLL();
            _poliklinikBLL = new PoliklinikBLL();
            _randevuDAL = new RandevuDAL();
            this.hastaID = hastaID;
        }
        Button btn;
        public string Email;
        private void frmHastaEkrani_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Today;
            hasta = _hastaDAL.MaileGoreHastaGetir(Email);
            lblAdSoyad.Text = hasta.HastaAd + " " + hasta.HastaSoyad;
            lblCinsiyet.Text = hasta.HastaCinsiyet.ToString();
            lblDtarihi.Text = hasta.HastaDTarihi.ToShortDateString();
            lblMHal.Text = hasta.MedeniHal.ToString();
            lblTc.Text = hasta.HastaTC;
            AlinmisRandevular = new List<RandevuEntity>();


            Yukle();

        }

        private void btn_Click(object sender, EventArgs e)
        {
            lblSeans.Text = (sender as Button).Text;
        }



        private void cmbHastane_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbHastane.SelectedItem == null)
            {
                return;
            }
            int hastaneID = _hastaneeDAL.IsmeGoreHastaneGetir(cmbHastane.SelectedItem.ToString());

            if (hastaneID > 0)
            {
                List<string> polikliniks = new List<string>();
                polikliniks = _poliklinikDAL.HastaneyeGorePoliklinikGetir(hastaneID);

                cmbPoliklinik.Items.Clear();
                foreach (string item in polikliniks)
                {
                    cmbPoliklinik.Items.Add(item);
                }

            }


        }

        private void cmbPoliklinik_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPoliklinik.SelectedItem == null)
            {
                return;
            }

            int poliklinikID = _poliklinikDAL.IsmeGorePoliklinikGetir(cmbPoliklinik.SelectedItem.ToString());

            if (poliklinikID > 0)
            {
                List<string> departmanlar = new List<string>();
                departmanlar = _departmanDAL.PoliklinigeGoreDepartmanGetir(poliklinikID);

                cmbDepartman.Items.Clear();
                foreach (string item in departmanlar)
                {
                    cmbDepartman.Items.Add(item);
                }

            }



        }



        void Yukle()
        {
            cmbHastane.DataSource = hastaneeBLL.HastaneAdlariGetir();
            cmbHastane.SelectedIndex = -1;
            cmbDoktor.DataSource = null;
            dateTimePicker1.Text = DateTime.Today.ToShortDateString();
        }
        int doktorID;
        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDoktor.SelectedItem == null)
            {
                return;
            }
            foreach (DoktorEntity item in _doktorDAL.TumDoktorlar())
            {
                if (item.DoktorAdi == cmbDoktor.Text)
                {
                    doktorID = item.DoktorID;

                }

            }

             AlinmisRandevular = _randevuDAL.DoktorunRandevulari(doktorID, dateTimePicker1.Value);

            RandevuSaatleri(AlinmisRandevular);

        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartman.SelectedItem == null)
            {
                return;
            }

            int departmanID = _departmanDAL.IsmeGoreDepartmanGetir(cmbDepartman.SelectedItem.ToString());

            if (departmanID > 0)
            {
                List<string> doktorlar = new List<string>();
                doktorlar = _doktorDAL.DepartmanaGoreDoktorGetir(departmanID);

                cmbDoktor.Items.Clear();
                foreach (string item in doktorlar)
                {
                    cmbDoktor.Items.Add(item);
                }

            }
        }
        List<RandevuEntity> AlinmisRandevular;
        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            RandevuEntity randevuEntity = new RandevuEntity();
            randevuEntity.RandevuTarihi = dateTimePicker1.Value;
            randevuEntity.HastaID = hasta.HastaID;
            randevuEntity.RandevuSaati = lblSeans.Text;

            foreach (DoktorEntity item in _doktorDAL.TumDoktorlar())
            {
                if (item.DoktorAdi == cmbDoktor.Text)
                {
                    randevuEntity.DoktorID = item.DoktorID;

                }

            }
            
            foreach (PoliklinikEntity item in _poliklinikDAL.TumPoliklinikler())
            {
                if (item.PoliklinikAdi == cmbPoliklinik.Text)
                {
                    randevuEntity.PoliklinikID = item.PoliklinikID;
                }

            }
            randevuEntity.RandevuDurumu = true;

            _randevuDAL.RandevuEkle(randevuEntity);
            RandevuSaatleri(AlinmisRandevular);

            MessageBox.Show("Randevu Kaydedildi");
        }

        void RandevuSaatleri(List<RandevuEntity> AlinmisRandevular)
        {
            this.AlinmisRandevular = AlinmisRandevular;
            AlinmisRandevular = _randevuDAL.DoktorunRandevulari(doktorID,dateTimePicker1.Value);
           

            flowLayoutPanel1.Controls.Clear();
            int check = 0;
            string saat = "9";
            string dk = "00";
            for (int i = 0; i < 17; i++)
            {

                if (int.Parse(saat) != 12)
                {
                    btn = new Button();
                    btn.BackColor = Color.LightBlue;
                    btn.Height = 25;
                    btn.Width = 50;
                    btn.Text = saat + '.' + dk;
                    btn.Click += new EventHandler(btn_Click);
                    if (AlinmisRandevular != null)
                    {
                        foreach (RandevuEntity item in AlinmisRandevular)
                        {
                            if (item.RandevuDurumu == true)
                            {
                                if (item.RandevuSaati == btn.Text)
                                {
                                    btn.Enabled = false;
                                    break;
                                }
                            }

                        }
                    }

                    flowLayoutPanel1.Controls.Add(btn);

                    if (i % 2 == 1)
                    {
                        saat = (int.Parse(saat) + 1).ToString();
                    }

                    if (i % 2 == 0)
                    {
                        dk = (30).ToString();

                    }
                    else
                    {
                        dk = (0).ToString();
                    }

                }
                else
                {
                    check += 1;
                    if (check == 2)
                    {
                        saat = (int.Parse(saat) + 1).ToString();
                    }
                }
            }
        }


        private void mevcutRandevularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMevcutRandevular frmMevcut = new frmMevcutRandevular(hastaID);
            frmMevcut.Owner = this;
            frmMevcut.Show();
        }
    }
}
