using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneEntity
{
    public class HastaEntity
    {
        
        public int HastaID { get; set; }
        public string HastaTC { get; set; }
        public string HastaAd { get; set; }
        public string HastaSoyad { get; set; }
        public DateTime HastaDTarihi { get; set; }
        public string HastaTelefon { get; set; }
        public char HastaCinsiyet { get; set; }
        public char MedeniHal { get; set; }
        public string HastaEmail { get; set; }
        public string HastaSifre { get; set; }
    }
}
