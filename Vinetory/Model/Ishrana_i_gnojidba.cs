using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Ishrana_i_gnojidba
    {
        public DateTime datum { get; set; }
        public int id { get; set; }
        public int id_sorte { get; set; }
        public int id_vin { get; set; }
        public string nacin_ishrane { get; set; }
        public string opis_ishrane { get; set; }
        public Ishrana_i_gnojidba()
        {
            datum = DateTime.Today;
        }
    }

