using Hastane.DTO;
using HastaneBLL;
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
    public partial class frmAnaForm : Form
    {
        EczacıGiris _eczaciGiris;
        GirisKontrol _girisKontrol;
        DoktorGiris _doktorGiris;

        public frmAnaForm()
        {
            InitializeComponent();
            _eczaciGiris = new EczacıGiris();
            _girisKontrol = new GirisKontrol();
            _doktorGiris = new DoktorGiris();
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {

            LoginDTO login = new LoginDTO();
            login.Email = txtEmail.Text;
            login.Sifre = txtSifre.Text;

            string result = _girisKontrol.isLoginSuccess(login);
            int hastaID;
            if (int.TryParse(result, out hastaID))
            {
                frmHastaEkrani frm = new frmHastaEkrani(hastaID);
                frm.Owner = this;
                frm.Email = txtEmail.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnEczane_Click(object sender, EventArgs e)
        {

            LoginDTO login = new LoginDTO();
            login.Email = txtEmail.Text;
            login.Sifre = txtSifre.Text;

            string result = _eczaciGiris.isLoginSuccess(login);
            int eczaciID;
            if (int.TryParse(result, out eczaciID))
            {
                frmEczaneEkrani frm = new frmEczaneEkrani(eczaciID);
                frm.Owner = this;
                frm.Email = txtEmail.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {

            LoginDTO login = new LoginDTO();
            login.Email = txtEmail.Text;
            login.Sifre = txtSifre.Text;

            string result = _doktorGiris.isLoginSuccess(login);
            int doktorID;
            if (int.TryParse(result, out doktorID))
            {
                frmDoktorEkrani frm = new frmDoktorEkrani();
                frm.Owner = this;
                frm.Email = txtEmail.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frm_UyeOl register = new frm_UyeOl();
            register.Show();
            register.Owner = this;
            this.Hide();
        }

        private void frmAnaForm_Load(object sender, EventArgs e)
        {

        }

        private void cBSifre_CheckedChanged(object sender, EventArgs e)
        {
            if (cBSifre.Checked)
            {
                txtSifre.PasswordChar = '\0';
            }
            else
            {
                txtSifre.PasswordChar = '*';
            }
        }
    }
}
