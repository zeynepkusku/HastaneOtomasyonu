using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
    public class PoliklinikBLL
    {
        PoliklinikDAL _poliklinikDAL;
        public PoliklinikBLL()
        {
            _poliklinikDAL = new PoliklinikDAL();
        }

        public List<string> PoliklinikAdlariGetir()
        {
            List<PoliklinikEntity> poliklinikler = _poliklinikDAL.TumPoliklinikler();
            List<string> polikliniklerAdlari = new List<string>();
            foreach (PoliklinikEntity item in poliklinikler)
            {
                polikliniklerAdlari.Add(item.PoliklinikAdi);

            }
            return polikliniklerAdlari;

        }
    }
}
