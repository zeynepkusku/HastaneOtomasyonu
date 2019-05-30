using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneEntity
{
    public class HastaRandevuEntity
    {
        public string  HastaBilgisi { get; set; }
        public string  HastaneAdi { get; set; }
        public string  DepartmanAdi { get; set; }
        public string  PoliklinikAdi { get; set; }
        public string  DoktorBilgisi { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string RandevuSaati { get; set; }
        public bool RandevuDurumu { get; set; }
        public int RandevuID { get; set; }
        public int HastaID { get; set; }
        public int DoktorID { get; set; }
        public int PoliklinikID { get; set; }

    }
}
