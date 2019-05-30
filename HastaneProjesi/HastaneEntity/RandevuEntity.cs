using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneEntity
{
    public class RandevuEntity
    {
        public int RandevuID { get; set; }
        public int HastaID { get; set; }
        public int DoktorID { get; set; }
       
        public int PoliklinikID { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public bool RandevuDurumu { get; set; }
        public string RandevuSaati { get; set; }
   
    }
}
