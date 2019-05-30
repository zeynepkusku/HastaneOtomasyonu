using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
   
    public class HastaneeBLL
    {
        HastaneeDAL _hastaneDAL;
        public HastaneeBLL()
        {
            _hastaneDAL = new HastaneeDAL();
        }


        public List<string> HastaneAdlariGetir()
        {
            List<HastaneeEntity> hastaneler = _hastaneDAL.TumHastaneler();
            List<string> hastaneAdlari = new List<string>();
            foreach   (HastaneeEntity item in hastaneler)
            {
                hastaneAdlari.Add(item.HastaneAdi);

            }
            return hastaneAdlari;

        }
    }
}
