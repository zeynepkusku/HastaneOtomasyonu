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
   public class EczacıGiris
    {
        EczaneDAL _eczaneDAL;
        public EczacıGiris()
        {
            _eczaneDAL = new EczaneDAL();
        }

        bool EczaneMailKontrol(string Email)
        {
            List<EczaneEntity> eczaneler = _eczaneDAL.TumEczaneler();
            foreach (EczaneEntity item in eczaneler)
            {
                if (item.EczaneEmail == Email)
                {
                    return true;
                }

            }
            return false;
        }

       
        public List<EczaneEntity> EczaneleriGetir()
        {
            return _eczaneDAL.TumEczaneler();
        }
        public EczaneEntity EczaneGetir(int eczaneID)
        {
            return _eczaneDAL.IDyeGoreEczaneGetir(eczaneID);
        }

        public string isLoginSuccess(LoginDTO login)
        {
            List<EczaneEntity> eczaneler = EczaneleriGetir();
            foreach (EczaneEntity item in eczaneler)
            {
                if (item.EczaneEmail == login.Email)
                {
                    if (item.EczaneSifre == login.Sifre)
                    {
                        return item.EczaneID.ToString();
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
