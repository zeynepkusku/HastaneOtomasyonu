using HastaneBLL;
using HastaneDAL;
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
    public partial class frmDoktorEkrani : Form
    {
      
        GirisKontrol _hasta;
        DoktorEntity _doktor;
        DoktorDAL _doktorDAL;
        RandevuDAL _randevuDAL;
        
  
        public frmDoktorEkrani()
        {
            InitializeComponent();
            _doktor = new DoktorEntity();
            _hasta = new GirisKontrol();
            _randevuDAL = new RandevuDAL();
            _doktorDAL = new DoktorDAL();
            
        }

        private void frmDoktorEkrani_Load(object sender, EventArgs e)
        {
            
            Frm_Listele();

        }

        public bool durum;
        public string Email;
        int doktorID; 
         
        private void Frm_Listele()
        {
            lvHastaListe.Items.Clear();
            DateTime tarih = dtTarih.Value;
            _doktor = _doktorDAL.DoktorGetirmece();
            doktorID = _doktor.DoktorID;


            List<HastaEntity> hastalar = _randevuDAL.RandevuHastasi(doktorID,tarih);
            ListViewItem lvi;
            foreach (var item in hastalar)
            {
                lvi = new ListViewItem();
                lvi.Text = item.HastaTC;
                lvi.SubItems.Add(item.HastaAd);
                lvi.SubItems.Add(item.HastaSoyad);
                lvi.SubItems.Add(item.HastaCinsiyet.ToString());
                lvi.SubItems.Add(item.MedeniHal.ToString());
                lvi.SubItems.Add(item.HastaTelefon);
                lvi.SubItems.Add(item.HastaDTarihi.ToString("MM /dd / yyyy"));

                lvi.Tag = item;
                lvHastaListe.Items.Add(lvi);

            }

        }
        private void dtTarih_ValueChanged(object sender, EventArgs e)
        {
            Frm_Listele();

        }
        frmDoktorMuayene frmDoktorMuayene;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lvHastaListe_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            frmDoktorMuayene = new frmDoktorMuayene();
            frmDoktorMuayene.Owner = this;
            frmDoktorMuayene.tc = lvHastaListe.SelectedItems[0].Text;
            frmDoktorMuayene.Ad = lvHastaListe.SelectedItems[0].SubItems[1].Text;
            frmDoktorMuayene.Soyad = lvHastaListe.SelectedItems[0].SubItems[2].Text;
            frmDoktorMuayene.Cinsiyet = lvHastaListe.SelectedItems[0].SubItems[3].Text;
            frmDoktorMuayene.MedeniHal = lvHastaListe.SelectedItems[0].SubItems[4].Text;
            frmDoktorMuayene.Telefon = lvHastaListe.SelectedItems[0].SubItems[5].Text;
            frmDoktorMuayene.DogumTarihi = lvHastaListe.SelectedItems[0].SubItems[6].Text;

            frmDoktorMuayene.Show();
            this.Hide();
        }
    }
}
