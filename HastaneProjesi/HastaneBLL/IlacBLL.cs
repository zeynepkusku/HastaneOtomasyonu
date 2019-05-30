using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
    public class IlacBLL
    {
        IlacDAL _ilacDAL;
       
        public  List<IlacEntity> IlacGetir()
        {
            _ilacDAL = new IlacDAL();
            return _ilacDAL.TumIlaclar();
        }
    }
}
