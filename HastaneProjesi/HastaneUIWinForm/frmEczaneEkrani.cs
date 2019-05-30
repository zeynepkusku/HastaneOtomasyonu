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
    public partial class frmEczaneEkrani : Form
    {
        EczaneDAL _eczaneDAL;
        HastaDAL _hastaDAL;
        IlacDAL _ilacDAL;
        public frmEczaneEkrani(int eczaciID)
        {
            InitializeComponent();
            _eczaneDAL = new EczaneDAL();
            _hastaDAL = new HastaDAL();
            _ilacDAL = new IlacDAL();
        }

        public string Email;
        private void frmEczaneEkrani_Load(object sender, EventArgs e)
        {
            EczaneEntity eczaneEntity = new EczaneEntity();
            eczaneEntity = _eczaneDAL.MaileGoreEczaneGetir(Email);
            lblEczane.Text = eczaneEntity.EczaneAdi;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HastaEntity hasta = new HastaEntity();

            hasta = _hastaDAL.ReceteNoyaHastaGetir(txtReceteNo.Text);
            lblAdSoyad.Text = hasta.HastaAd + " " + hasta.HastaSoyad;
            lblCinsiyet.Text = hasta.HastaCinsiyet.ToString();
            lblDtarihi.Text = hasta.HastaDTarihi.ToShortDateString();
            lblMHal.Text = hasta.MedeniHal.ToString();
            lblTc.Text = hasta.HastaTC;

            IlacEntity ilac = new IlacEntity();

            ilac = _ilacDAL.ReceteIlaclari(txtReceteNo.Text);
            lstIlac.Items.Clear();
            lstIlac.Items.Add(ilac.IlacAdi);
        }
    }
}
