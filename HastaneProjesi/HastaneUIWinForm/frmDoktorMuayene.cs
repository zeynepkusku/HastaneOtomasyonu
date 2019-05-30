using HastaneBLL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneUIWinForm
{
    public partial class frmDoktorMuayene : Form
    {
        TeshisBLL _teshisBLL;
        IlacBLL _ilacBLL;

        public frmDoktorMuayene()
        {
            InitializeComponent();
            _teshisBLL = new TeshisBLL();
            _ilacBLL = new IlacBLL();
        }
        public string tc;
        public string Ad;
        public string Soyad;
        public string Cinsiyet;
        public string MedeniHal;
        public string Telefon;
        public string DogumTarihi;

        private void frmDoktorMuayene_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            lblAd.Text = Ad;
            lblSoyad.Text = Soyad;
            lblCinsiyet.Text = Cinsiyet;
            lblMedeniHal.Text = MedeniHal;
            lblTelefon.Text = Telefon;
            lblDtarihi.Text = DogumTarihi;
            Teshisler();
            Ilaclar();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            frmDoktorEkrani frmDoktorEkrani = new frmDoktorEkrani();
            frmDoktorEkrani.Owner = this;
            if (comboBox1.Text == "Geldi")
            {
                frmDoktorEkrani.durum = true;
            }
            frmDoktorEkrani.Show();
            this.Hide();
        }
        void Teshisler()
        {
            List<TeshislerEntity> teshisler = _teshisBLL.TeshisGetir(lblTc.Text);

            foreach (var item in teshisler)
            {

                cmbTeshis.Items.Add(item.TeshisAdi);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void Ilaclar()
        {
            List<IlacEntity> ilaclar = _ilacBLL.IlacGetir();
            foreach (var item in ilaclar)
            {
                cmbIlac.Items.Add(item.IlacAdi);
            }
        }

        private void İŞnde_Click(object sender, EventArgs e)
        {
            int hastaID=2;
            frmHastaEkrani frmHasta = new frmHastaEkrani(hastaID);
            frmHasta.Owner = this;
            frmHasta.Show();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
