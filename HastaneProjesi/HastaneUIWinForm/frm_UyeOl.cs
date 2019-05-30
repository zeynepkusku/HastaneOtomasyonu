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
    public partial class frm_UyeOl : Form
    {
        UyelikKontrol _uyeKontrol;
        public frm_UyeOl()
        {
            InitializeComponent();
            _uyeKontrol = new UyelikKontrol();
        }

        HastaEntity hasta;
        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            hasta = new HastaEntity();
            if (string.IsNullOrEmpty(txtTc.Text))
            {
                MessageBox.Show("TC kimlik no boş geçilemez");
                return;
            }
            if (txtTc.Text.Length < 11 || txtTc.Text.Length > 11)
            {
                MessageBox.Show("TC kimlik no 11 karakter içermeli");
                return;

            }
            if (string.IsNullOrEmpty(txtAd.Text))
            {
                MessageBox.Show("Ad boş geçilemez");
                return;
            }
            if (string.IsNullOrEmpty(txtSoyad.Text))
            {
                MessageBox.Show("Soyad boş geçilemez");
                return;
            }
            if (string.IsNullOrEmpty(txtTel.Text))
            {
                MessageBox.Show("Telefon boş geçilemez");
                return;
            }
            if (txtTel.Text.Length < 10 || txtTel.Text.Length > 22)
            {
                MessageBox.Show("Telefon numarası hatalı");
                return;
            }

            if (cmbMedeni.SelectedItem == null)
            {
                MessageBox.Show("Medeni hal bilgisi boş geçilemez");
                return;

            }
            if (cmbCinsiyet.SelectedItem == null)
            {
                MessageBox.Show("Cinsiyet bilgisi boş geçilemez");
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("E-mail boş geçilemez");
                return;
            }
            if (string.IsNullOrEmpty(txtSifre.Text))
            {
                MessageBox.Show("Şifre boş geçilemez");
                return;
            }
            if (txtSifre.Text.Length < 8 || txtSifre.Text.Length > 16)
            {

                MessageBox.Show("Şifre en az 8 en fazla 16 karakterden oluşabilir");
                return;
            }


            if (dtDtarihi.Value > DateTime.Now)
            {
                MessageBox.Show("Doğum tarihi hatalı");
                return;

            }

            hasta.HastaTC = txtTc.Text;
            hasta.HastaAd = txtAd.Text;
            hasta.HastaSoyad = txtSoyad.Text;
            hasta.HastaTelefon = txtTel.Text;
            hasta.HastaDTarihi = dtDtarihi.Value;
            hasta.HastaEmail = txtEmail.Text;
            hasta.HastaSifre = txtSifre.Text;
            hasta.MedeniHal = Convert.ToChar(cmbMedeni.SelectedItem.ToString().Substring(0, 1));
            hasta.HastaCinsiyet = Convert.ToChar(cmbCinsiyet.SelectedItem.ToString().Substring(0, 1));

            bool result= _uyeKontrol.Add(hasta);
            MessageBox.Show(result ? "Hasta sisteme kaydedildi" : "Kayıt Başarısız");

          
            frmAnaForm frm = new frmAnaForm();
            frm.Owner = this;
            frm.Show();
            this.Hide();
        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
    }
}
