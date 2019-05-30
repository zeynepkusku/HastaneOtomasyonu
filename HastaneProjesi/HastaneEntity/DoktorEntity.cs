using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneEntity
{
    public class DoktorEntity
    {
        public int DoktorID { get; set; }
        public string DoktorTC { get; set; }
        public string  DoktorAdi { get; set; }
        public string  DoktorSoyadi { get; set; }
        public DateTime DoktorDTarihi { get; set; }
        public string DoktorTelefon { get; set; }
        public char DoktorCinsiyet { get; set; }
        public int HastaneID { get; set; }
        public int DepartmanID { get; set; }
        public string DoktorEmail { get; set; }
        public string DoktorSifre { get; set; }
    }
}
