using HastaneDAL;
using HastaneEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneBLL
{
    public class TeshisBLL
    {
        TeshisDAL _teshisDAL;
        public List<TeshislerEntity> TeshisGetir(string tc)
        {
            _teshisDAL = new TeshisDAL();
            return _teshisDAL.TumTeshisler(tc);

        }
    }
}
