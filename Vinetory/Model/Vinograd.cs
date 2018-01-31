using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Vinograd
    {
        DateTime datum;
        public long id_vin { get; set; }
        public long id_kor { get; set; }
        public long oib { get; set; }
        public int mibpg { get; set; }
        public int broj_sorti { get; set; }
        public float udaljenost_cokota { get; set; }
        public float udaljenost_reda { get; set; }
        public List<Sorta> Sorte = new List<Sorta>();
        public Vinograd()
        {
            this.datum = DateTime.Today;
        }
    }

