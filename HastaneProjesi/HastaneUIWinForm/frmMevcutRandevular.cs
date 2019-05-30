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
    public partial class frmMevcutRandevular : Form
    {
        RandevuDAL _randevuDAL;
        GirisKontrol _hasta;
        RandevuEntity randevu;
        int hastaID;

        public frmMevcutRandevular(int hastaID)
        {
            InitializeComponent();
            _randevuDAL = new RandevuDAL();
            _hasta = new GirisKontrol();
            randevu = new RandevuEntity();
            this.hastaID = hastaID;

        }
        List<HastaRandevuEntity> hastaRandevulari;
        private void frmMevcutRandevular_Load(object sender, EventArgs e)
        {
            lvHastaListe.Items.Clear();
           // hastaRandevulari = _randevuDAL.HastaninRandevulari(hastaID);

            RandevularimiListele();

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {


            if (lvHastaListe.SelectedItems[0] == null)
            {
                return;
            }

            if (lvHastaListe.SelectedItems[0].SubItems[6].Text == "Aktif")
            {
                foreach (HastaRandevuEntity item in hastaRandevulari)
                {
                    if (lvHastaListe.SelectedItems[0].SubItems[7].Text == item.RandevuID.ToString())
                    {
                        randevu.RandevuID = item.RandevuID;
                        randevu.HastaID = item.HastaID;
                        randevu.DoktorID = item.DoktorID;
                        randevu.PoliklinikID = item.PoliklinikID;
                        randevu.RandevuTarihi = item.RandevuTarihi;
                        randevu.RandevuDurumu = false;
                        randevu.RandevuSaati = item.RandevuSaati;

                    }

                }
                _randevuDAL.RandevuGuncelle(randevu);
                MessageBox.Show("Randevu İptal Edildi");
               

                RandevularimiListele();

            }
        }
        void RandevularimiListele()
        {
            hastaRandevulari = _randevuDAL.HastaninRandevulari(hastaID);
            lvHastaListe.Items.Clear();
            string durum;

            ListViewItem lvi;
            foreach (var item in hastaRandevulari)
            {
                lvi = new ListViewItem();
                lvi.Text = item.HastaneAdi;
                lvi.SubItems.Add(item.DepartmanAdi);
                lvi.SubItems.Add(item.PoliklinikAdi);
                lvi.SubItems.Add(item.DoktorBilgisi);
                lvi.SubItems.Add(item.RandevuTarihi.ToString());
                lvi.SubItems.Add(item.RandevuSaati);
                if (item.RandevuDurumu == true)
                {
                    durum = "Aktif";
                }
                else
                {
                    durum = "Pasif";
                }
                lvi.SubItems.Add(durum);
                lvi.SubItems.Add(item.RandevuID.ToString());

                lvi.Tag = item;
                lvHastaListe.Items.Add(lvi);

            }
        }
        private void lvHastaListe_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
