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
    public class UyelikKontrol
    {
        HastaDAL _hastaDal;

        public UyelikKontrol()
        {
            _hastaDal = new HastaDAL();
        }

        public bool CheckUserByMail(string mail)
        {
            List<HastaEntity> hastalar = _hastaDal.TumHastalar();
            foreach (HastaEntity item in hastalar)
            {
                if (item.HastaEmail == mail)
                {
                    return true;
                }

            }
            return false;
        }
        public bool IsExistTc(string tc)
        {
            List<HastaEntity> hastalar = _hastaDal.TumHastalar();
            foreach (HastaEntity item in hastalar)
            {
                if (item.HastaTC == tc)
                {
                    return true;
                }
            }
            return false;
        }



        public bool Add(HastaEntity hasta)
        {
        

                if (CheckUserByMail(hasta.HastaEmail))
                {
                    throw new Exception("Bu mail sistemde kayıtlı olduğundan tekrar eklenemez");
                }

            int result = _hastaDal.HastaEkle(hasta);
            return result > 0;
        }

    }
}
