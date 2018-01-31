using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Sorta
    {
        public DateTime datum { get; set; }
        public long id { get; set; }
        public long id_vin { get; set; }
        public string ime_sorte { get; set; }
        public string oplem_oznaka { get; set; }
        public int povrsina_zemlje { get; set; }
        public List<Zastita> zastite = new List<Zastita>();
        public List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
        public List<Ishrana_i_gnojidba> ishrane_i_gnojidbe = new List<Ishrana_i_gnojidba>();
        public List<Berba> berbe = new List<Berba>();
        public Sorta()
        {
            this.datum = DateTime.Today;
        }
    }

