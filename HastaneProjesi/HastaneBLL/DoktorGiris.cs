using Hastane.DTO;
using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
    public class DoktorGiris
    {
        DoktorDAL _doktorDAL;

        public DoktorGiris()
        {
            _doktorDAL = new DoktorDAL();
        }

        bool DoktorMailKontrol(string Email)
        {
            List<DoktorEntity> doktorlar = _doktorDAL.TumDoktorlar();
            foreach (DoktorEntity item in doktorlar)
            {
                if (item.DoktorEmail == Email)
                {
                    return true;
                }
            }
            return false;
        }
        public List<DoktorEntity> DoktorlariGetir()
        {
            return _doktorDAL.TumDoktorlar();
        }
        public DoktorEntity DoktorGetir(int doktorID)
        {
            return _doktorDAL.IDyeGoreDoktorGetir(doktorID);
        }
        public string isLoginSuccess(LoginDTO login)
        {
            List<DoktorEntity> doktorlar = DoktorlariGetir();
            foreach (DoktorEntity item in doktorlar)
            {
                if (item.DoktorEmail == login.Email)
                {
                    if (item.DoktorSifre == login.Sifre)
                    {
                        return item.DoktorID.ToString();
                    }
                    else
                    {
                        return "Şifre yanlış!";
                    }
                }
            }
            return "Email yanlış!";
        }
    }
}
