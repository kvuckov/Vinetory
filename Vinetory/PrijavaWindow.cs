using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
namespace Vinetory
{
    public partial class PrijavaWindow : Form
    {
        private Korisnik x = new Korisnik();

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        public PrijavaWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            lozinka_entry.PasswordChar = '*';
        }
        
        private void OnPrijava_korClicked(object sender, EventArgs e)
        {           
            List<Korisnik> korisnici = new List<Korisnik>();
            List<Vinograd> vinogradi = new List<Vinograd>();
            List<Sorta> sorte = new List<Sorta>();
            List<Zastita> zastite = new List<Zastita>();
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            List<Ishrana_i_gnojidba> ishrane = new List<Ishrana_i_gnojidba>();
            List<Berba> berbe = new List<Berba>();
            baza.kon.Open();
            SQLiteCommand sqlNaredba;
            string traziKor = "SELECT * FROM Korisnik";
            
            sqlNaredba = new SQLiteCommand(traziKor, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Korisnik temp = new Korisnik();
                temp.id = Convert.ToInt64(r["KorisnikID"]);
                temp.kor_ime = r["KorisnikKor_ime"].ToString();
                temp.ime = r["KorisnikIme"].ToString();
                temp.prezime = r["KorisnikPrezime"].ToString();
                temp.lozinka =r["KorisnikLozinka"].ToString();
                korisnici.Add(temp);
            }
            
            string traziVin = "SELECT * FROM Vinograd";
            sqlNaredba = new SQLiteCommand(traziVin, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Vinograd temp = new Vinograd();
                temp.id_vin = Convert.ToInt64(r["VinogradID"]);
                temp.id_kor = Convert.ToInt64(r["KorisnikID"]);
                temp.oib =Convert.ToInt64 (r["VinogradOib"]);
                temp.mibpg =Convert.ToInt32 (r["VinogradMibpg"]);
                temp.broj_sorti =Convert.ToInt32( r["VinogradBroj_sorti"]);
                temp.udaljenost_cokota =Convert.ToSingle( r["VinogradCokot"]);
                temp.udaljenost_reda = Convert.ToSingle(r["VinogradRed"]);
                vinogradi.Add(temp);
            }

            string traziSortu = "SELECT * FROM Sorta";
            sqlNaredba = new SQLiteCommand(traziSortu, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Sorta temp = new Sorta();
                temp.id = Convert.ToInt64(r["SortaID"]);
                temp.id_vin = Convert.ToInt64(r["VinogradID"]);
                temp.ime_sorte =Convert.ToString (r["SortaIme"]);
                temp.oplem_oznaka = Convert.ToString(r["SortaOznaka"]);
                temp.povrsina_zemlje = Convert.ToInt32 (r["SortaPovrsina"]);           
                sorte.Add(temp);
            }
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Zastita temp = new Zastita();
                temp.id = Convert.ToInt32(r["ZastitaID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["ZastitaDatum"]);
                temp.arkod_id = Convert.ToInt64(r["ZastitaArkod"]);
                temp.naziv = Convert.ToString(r["ZastitaNaziv"]);
                temp.trgovacki_naziv = Convert.ToString(r["ZastitaTrgovacki_naziv"]);
                temp.zasticena_pov = Convert.ToSingle(r["ZastitaZasticena_povrsina"]);
                temp.pocetak = Convert.ToString(r["ZastitaPocetak"]);
                temp.zavrsetak = Convert.ToString(r["ZastitaZavrsetak"]);
                temp.doza_sredstva = Convert.ToSingle(r["ZastitaDoza"]);
                temp.datum_slijedece_zastite = Convert.ToDateTime(r["ZastitaSljedeca_zastita"]);
                zastite.Add(temp);
            }
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["BerbaDatum"]);
                temp.broj_beraca = Convert.ToInt32(r["BerbaBroj_beraca"]);
                temp.utroseni_budzet = Convert.ToSingle(r["BerbaUtroseni_budzet"]);
                temp.sati_branja = Convert.ToSingle(r["BerbaSati_branja"]);
                temp.obrano_kg = Convert.ToSingle(r["BerbaObrano_kg"]);
                temp.obrana_pov = Convert.ToSingle(r["BerbaObrana_povrsina"]);              
                berbe.Add(temp);
            }
            string traziIshranu = "SELECT * FROM Ishrana";
            sqlNaredba = new SQLiteCommand(traziIshranu, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Ishrana_i_gnojidba temp = new Ishrana_i_gnojidba();
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["IshranaDatum"]);
                temp.nacin_ishrane = Convert.ToString(r["IshranaNaziv"]);
                temp.opis_ishrane = Convert.ToString(r["IshranaOpis"]);               
                ishrane.Add(temp);
            }
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            sqlNaredba.Dispose();
            r.Close();
            baza.kon.Close();
            int br = 0;
            foreach (Korisnik x in korisnici)
            {
                if (x.kor_ime == kor_ime_entry.Text && x.lozinka == lozinka_entry.Text)
                {
                    br++;
                }
            }
            if (br == 0)
            {
                MessageBox.Show("Krivo ste uvijeli korisničko ime ili lozinku", "Pozor!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                var prijava = new PrijavaWindow();
                prijava.Show();
                this.Close();
            }
            else
            {
                foreach (Korisnik x in korisnici)
                {
                    if (x.kor_ime == kor_ime_entry.Text && x.lozinka == lozinka_entry.Text)
                    {
                        X.id = x.id;
                        X.ime = x.ime;
                        X.prezime = x.prezime;
                        X.kor_ime = x.kor_ime;
                        X.lozinka = x.lozinka;
                        foreach (Vinograd y in vinogradi)
                        {
                            if (y.id_kor == X.id)
                            {
                                X.vinograd = new Vinograd();
                                X.vinograd.id_vin = y.id_vin;
                                X.vinograd.oib = y.oib;
                                X.vinograd.mibpg = y.mibpg;
                                X.vinograd.broj_sorti = y.broj_sorti;
                                X.vinograd.udaljenost_cokota = y.udaljenost_cokota;
                                X.vinograd.udaljenost_reda = y.udaljenost_reda;
                                foreach (Sorta z in sorte)
                                {
                                    if (z.id_vin == X.vinograd.id_vin)
                                    {
                                        Sorta a = new Sorta();
                                        a.id = z.id;
                                        a.id_vin = z.id_vin;
                                        a.ime_sorte = z.ime_sorte;
                                        a.oplem_oznaka = z.oplem_oznaka;
                                        a.povrsina_zemlje = z.povrsina_zemlje;
                                        X.vinograd.Sorte.Add(a);
                                    }
                                }
                            }
                        }
                        
                        foreach (Sorta c in X.vinograd.Sorte)
                        {
                            foreach (Zastita z in zastite)
                            {
                                if (z.id_sorte == c.id && z.id_vin == X.vinograd.id_vin)
                                {
                                    Zastita a = new Zastita();
                                    a.id = z.id;
                                    a.id_sorte = z.id_sorte;
                                    a.id_vin = z.id_vin;
                                    a.datum = z.datum;
                                    a.naziv = z.naziv;
                                    a.trgovacki_naziv = z.trgovacki_naziv;
                                    a.zasticena_pov = z.zasticena_pov;
                                    a.pocetak = z.pocetak;
                                    a.zavrsetak = z.zavrsetak;
                                    a.doza_sredstva = z.doza_sredstva;
                                    a.datum_slijedece_zastite = z.datum_slijedece_zastite;
                                    c.zastite.Add(a);
                                }
                            }
                        }
                        foreach (Sorta c in X.vinograd.Sorte)
                        {
                            foreach (Berba z in berbe)
                            {
                                if (z.id_sorte == c.id && z.id_vin == X.vinograd.id_vin)
                                {
                                    Berba a = new Berba();
                                    a.id = z.id;
                                    a.id_sorte = z.id_sorte;
                                    a.id_vin = z.id_vin;
                                    a.datum = z.datum;
                                    a.broj_beraca = z.broj_beraca;
                                    a.utroseni_budzet = z.utroseni_budzet;
                                    a.sati_branja = z.sati_branja;
                                    a.obrano_kg = z.obrano_kg;
                                    a.obrana_pov = z.obrana_pov;
                                    c.berbe.Add(a);
                                }
                            }
                        }
                        foreach (Sorta c in X.vinograd.Sorte)
                        {
                            foreach (Ishrana_i_gnojidba z in ishrane)
                            {
                                if (z.id_sorte == c.id && z.id_vin == X.vinograd.id_vin)
                                {
                                    Ishrana_i_gnojidba a = new Ishrana_i_gnojidba();
                                    a.id = z.id;
                                    a.id_sorte = z.id_sorte;
                                    a.id_vin = z.id_vin;
                                    a.datum = z.datum;
                                    a.nacin_ishrane = z.nacin_ishrane;
                                    a.opis_ishrane = z.opis_ishrane;
                                    c.ishrane_i_gnojidbe.Add(a);
                                }
                            }
                        }
                        foreach (Sorta c in X.vinograd.Sorte)
                        {
                            foreach (Odrzavanje z in odrzavanja)
                            {
                                if (z.id_sorte == c.id && z.id_vin == X.vinograd.id_vin)
                                {
                                    Odrzavanje a = new Odrzavanje();
                                    a.id = z.id;
                                    a.id_sorte = z.id_sorte;
                                    a.id_vin = z.id_vin;
                                    a.datum = z.datum;
                                    a.vrsta_posla = z.vrsta_posla;
                                    a.opis_posla = z.opis_posla;
                                    c.odrzavanja.Add(a);
                                }
                            }
                        }
                        var glavni = new GlavniWindow();
                        glavni.X = X;
                        glavni.Show();
                        glavni.postaviLAbel();
                        this.Hide();


                    }

                }
            }
             
                
        }
           
    }
        
        
}

