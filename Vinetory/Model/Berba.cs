using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Berba
    {
        public DateTime datum { get; set; }
        public int id { get; set; }
        public int id_sorte { get; set; }
        public int id_vin { get; set; }
        public int broj_beraca { get; set; }
        public float utroseni_budzet { get; set; }
        public float sati_branja { get; set; }
        public float obrano_kg { get; set; }
        public float obrana_pov { get; set; }
        public Berba()
        {
            datum = DateTime.Today;
        }
    }

