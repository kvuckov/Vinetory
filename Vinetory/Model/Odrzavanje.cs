using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Odrzavanje
    {
        public DateTime datum { get; set; }
        public int id { get; set; }
        public int id_sorte { get; set; }
        public int id_vin { get; set; }
        public string vrsta_posla { get; set; }
        public string opis_posla { get; set; }
        public Odrzavanje()
        {
            datum = DateTime.Today;
        }
    }

