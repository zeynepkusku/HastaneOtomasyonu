using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
   
    public class DepartmanBLL
    {
        DepartmanDAL _departmanDAL;
        public DepartmanBLL()
        {
            _departmanDAL = new DepartmanDAL();
        }


        public List<string> DepartmanAdlariGetir()
        {
            List<DepartmanEntity> departmanlar = _departmanDAL.TumDepartmanlar();
            List<string> departmanAdlari = new List<string>();
            foreach (DepartmanEntity item in departmanlar)
            {
                departmanAdlari.Add(item.DepartmanAdi);

            }
            return departmanAdlari;

        }
    }
}
