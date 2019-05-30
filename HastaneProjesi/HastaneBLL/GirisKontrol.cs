using Hastane.DTO;
using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBLL
{
   public class GirisKontrol
    {
        HastaDAL _hastaDal;
       
        public GirisKontrol()
        {
            _hastaDal = new HastaDAL();
        }

        bool HastaMailKontrol(string Email)
        {
            List<HastaEntity> hastalar = _hastaDal.TumHastalar();
            foreach (HastaEntity item in hastalar)
            {
                if (item.HastaEmail == Email)
                {
                    return true;
                }

            }
            return false;
        }

        public bool Ekle(HastaEntity hasta)
        {
            if (HastaMailKontrol(hasta.HastaEmail))
            {
                throw new Exception("Bu mail sistemde kayıtlı olduğundan tekrar eklenemez!.");
            }
            int result = _hastaDal.HastaEkle(hasta);
            return result > 0;
        }

        public bool Guncelle(HastaEntity hasta)
        {
            HastaEntity oHasta = _hastaDal.IDyeGoreHastaGetir(hasta.HastaID);
            oHasta.HastaAd = hasta.HastaAd;
            oHasta.HastaSoyad = hasta.HastaSoyad;
            oHasta.HastaTC = hasta.HastaTC;
            oHasta.HastaDTarihi = hasta.HastaDTarihi;
            oHasta.HastaCinsiyet = hasta.HastaCinsiyet;
            oHasta.HastaTelefon = hasta.HastaTelefon;
            oHasta.MedeniHal = hasta.MedeniHal;
            oHasta.HastaEmail = hasta.HastaEmail;
            oHasta.HastaSifre = hasta.HastaSifre;

            int result = _hastaDal.HastaGuncelle(oHasta);
            return result > 0;
        }
        public bool Delete(HastaEntity hasta)
        {
            int result = _hastaDal.HastaSil(hasta.HastaID);
            return result > 0;
        }
        public List<HastaEntity> HastalariGetir() 
        {
            return _hastaDal.TumHastalar();
        }
        public HastaEntity HastaGetir(int hastaID)
        {
            return _hastaDal.IDyeGoreHastaGetir(hastaID);
        }

       

        public string isLoginSuccess(LoginDTO login)
        {
            List<HastaEntity> hastalar = HastalariGetir();
            foreach (HastaEntity item in hastalar)
            {
                if (item.HastaEmail == login.Email)
                {
                    if (item.HastaSifre == login.Sifre)
                    {
                        return item.HastaID.ToString();
                    }
                    else
                    {
                        return "Şifre yanlış!";
                    }
                }                
            }
            return "Email yanlış!";
        }

       
        public List<HastaEntity> tariheGoreTumHastalar(DateTime tarih)
        {
            return _hastaDal.TariheGoreTumHastalar(tarih);
        }
        public HastaEntity ReceteNoyaHastaGetirKontrol(string receteNo)
        {
            return _hastaDal.ReceteNoyaHastaGetir(receteNo);
        }
    }
}
