using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Zastita
    {
        public long arkod_id { get; set; }
        public int id { get; set; }
        public int id_sorte { get; set; }
        public int id_vin { get; set; }
        public string naziv { get; set; }
        public string trgovacki_naziv { get; set; }
        public float zasticena_pov { get; set; }
        public DateTime datum { get; set; }
        public string pocetak { get; set; }
        public string zavrsetak { get; set; }
        public float doza_sredstva { get; set; }
        public DateTime datum_slijedece_zastite { get; set; }
        public Zastita()
        {
            datum = DateTime.Today;
        }
    }

