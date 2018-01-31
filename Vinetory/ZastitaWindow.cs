using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
namespace Vinetory
{
    public partial class ZastitaWindow : Form
    {
        private Korisnik x =new Korisnik();

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        
        public ZastitaWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;          
        }       
        
        public void postaviLAbel()
        {
            ime_izmjena_entry.Text = X.ime;
            prezime_izmjena_entry.Text = X.prezime;
            kor_ime_label.Text = X.kor_ime;
            oib_izmjena_entry.Text = Convert.ToString(X.vinograd.oib);
            mibpg_izmjena_entry.Text = Convert.ToString(X.vinograd.mibpg);
            br_sorti_entry.Text = Convert.ToString(X.vinograd.broj_sorti);
            red_izmjena_entry.Text = Convert.ToString(X.vinograd.udaljenost_reda);
            cokot_izmjena_entry.Text = Convert.ToString(X.vinograd.udaljenost_cokota);
            prijavljeni_korisnik.Text = x.ime + " " + x.prezime;
            sorta1.Hide();
            sorta2.Hide();
            sorta3.Hide();
            sorta4.Hide();
            sorta5.Hide();
            sorta6.Hide();
            foreach (Sorta x in X.vinograd.Sorte)
            {
                if (x.id == 1)
                {
                    sorta1.Show();
                    sorta1_label.Text = x.ime_sorte;
                    sorta1.Text = x.ime_sorte;
                }
                if (x.id == 2)
                {
                    sorta2.Show();
                    sorta2_label.Text = x.ime_sorte;
                    sorta2.Text = x.ime_sorte;
                }
                if (x.id == 3)
                {
                    sorta3.Show();
                    sorta3_label.Text = x.ime_sorte;
                    sorta3.Text = x.ime_sorte;
                }
                if (x.id == 4)
                {
                    sorta4.Show();
                    sorta4_label.Text = x.ime_sorte;
                    sorta4.Text = x.ime_sorte;
                }
                if (x.id == 5)
                {
                    sorta5.Show();
                    sorta5_label.Text = x.ime_sorte;
                    sorta5.Text = x.ime_sorte;
                }
                if (x.id == 6)
                {
                    sorta6.Show();
                    sorta6_label.Text = x.ime_sorte;
                    sorta6.Text = x.ime_sorte;
                }
            }
            float prosjZastita=0;
            int br = 0;
            foreach(Sorta x in X.vinograd.Sorte)
            {
                foreach(Zastita y in x.zastite)
                {
                    br++;
                    prosjZastita += y.zasticena_pov;                   
                }
            }
            double prosjek = Kalkulator.ProsjecnoTrajanjeZastite(prosjZastita, br);
            prosj_zastite_label.Text = prosjek.ToString();
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum",typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach(Zastita x in zastite)
            {
                if(x.id_sorte==1 && x.id_vin==X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum,x.naziv,x.arkod_id,x.trgovacki_naziv,x.pocetak,x.zavrsetak,x.doza_sredstva);
                }
            }
            foreach(DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }
        private void OnDodajSortuClicked(object sender, EventArgs e)
        {
            X.vinograd.broj_sorti++;
            br_sorti_entry.Text = X.vinograd.broj_sorti.ToString();
            Sorta a = new Sorta();
            a.ime_sorte = ime_sorte_dodajnovi_entry.Text;
            a.oplem_oznaka = oplem_oznaka_dodajnovu_entry.Text;
            a.povrsina_zemlje = Int32.Parse(pov_zemlje_dodajnovi_entry.Text);
            List<Vinograd> vinogradi = new List<Vinograd>();
            baza.kon.Open();
            string traziVin = "SELECT * FROM Vinograd";
            sqlNaredba = new SQLiteCommand(traziVin, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Vinograd y = new Vinograd();
                y.id_vin = Convert.ToInt64(r["VinogradID"]);
                y.id_kor = Convert.ToInt64(r["KorisnikID"]);
                vinogradi.Add(y);
            }
            if (X.vinograd.broj_sorti == 1)
            {
                a.id = 1;
                X.vinograd.Sorte.Add(a);
                sorta1_label.Text = a.ime_sorte;
                sorta1.Show();
                sorta1.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            if (X.vinograd.broj_sorti == 2)
            {
                a.id = 2;
                X.vinograd.Sorte.Add(a);
                sorta2_label.Text = a.ime_sorte;
                sorta2.Show();
                sorta2.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            if (X.vinograd.broj_sorti == 3)
            {
                a.id = 3;
                X.vinograd.Sorte.Add(a);
                sorta3_label.Text = a.ime_sorte;
                sorta3.Show();
                sorta3.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            if (X.vinograd.broj_sorti == 4)
            {
                a.id = 4;
                X.vinograd.Sorte.Add(a);
                sorta4_label.Text = a.ime_sorte;
                sorta4.Show();
                sorta4.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            if (X.vinograd.broj_sorti == 5)
            {
                a.id = 5;
                X.vinograd.Sorte.Add(a);
                sorta5_label.Text = a.ime_sorte;
                sorta5.Show();
                sorta5.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            if (X.vinograd.broj_sorti == 6)
            {
                a.id = 6;
                X.vinograd.Sorte.Add(a);
                sorta6_label.Text = a.ime_sorte;
                sorta6.Show();
                sorta6.Text = a.ime_sorte;
                foreach (Vinograd y in vinogradi)
                {
                    if (y.id_kor == X.id)
                    {
                        X.vinograd.id_vin = y.id_vin;
                        string dodajSortu = "INSERT INTO Sorta(SortaID,VinogradID,SortaDatum,SortaIme,SortaOznaka,SortaPovrsina) VALUES ('" + a.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.ime_sorte + "','" + a.oplem_oznaka + "','" + a.povrsina_zemlje + "')";
                        sqlNaredba = new SQLiteCommand(dodajSortu, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='" + X.vinograd.broj_sorti + "' WHERE KorisnikID='" + X.id + "'";
                        sqlNaredba = new SQLiteCommand(updateBr_s, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                    }
                }
            }
            ime_sorte_dodajnovi_entry.Text = null;
            oplem_oznaka_dodajnovu_entry.Text = null;
            pov_zemlje_dodajnovi_entry.Text = null;
            MessageBox.Show("Vaša sorta je spremljena", "Obavijest!",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            r.Close();
            sqlNaredba.Dispose();
            baza.kon.Close();
        }

             

        private void OnBerbaClicked(object sender, EventArgs e)
        {
            var berba = new BerbaWindow();
            berba.X = X;
            berba.Show();
            berba.postaviLAbel();
            this.Hide();
        }

        private void OnOdrzavanjeClicked(object sender, EventArgs e)
        {
            var odrzavanje = new OdrzavanjeWindow();
            odrzavanje.X = X;
            odrzavanje.Show();
            odrzavanje.postaviLAbel();
            this.Hide();
        }

        private void OnIshrana_i_gnojidbaClicked(object sender, EventArgs e)
        {
            var ishrana = new Ishrana_i_gnojidbaWindow();
            ishrana.X = X;
            ishrana.Show();
            ishrana.postaviLAbel();
            this.Hide();
        }

        private void OnKalkulatorClicked(object sender, EventArgs e)
        {
            var kalkulator = new KalkulatorWindow();
            kalkulator.X = X;
            kalkulator.Show();
            kalkulator.postaviLAbel();
            this.Hide();
        }

        private void OnOdjavaClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnSpremi_zastituClicked(object sender, EventArgs e)
        {
            if (arkod_entry.Text == "" || povha_entry.Text == "" || pocetak_entry.Text == "" || zavrsetak_entry.Text == "" || doza_entry.Text == "")
            {
                MessageBox.Show("Potrebno je unijeti sve podatke", "Pozor!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                var zastita = new ZastitaWindow();
                zastita.X = X;
                zastita.Show();
                zastita.postaviLAbel();
                this.Hide();
            }
            else
            {
                Zastita a = new Zastita();
                a.naziv = domaci_naziv_entry.Text;
                a.datum = DateTime.Parse(dat_zastite_dateTimePicker.Text);
                a.arkod_id = Int64.Parse(arkod_entry.Text);
                a.zasticena_pov = float.Parse(povha_entry.Text);
                a.pocetak = pocetak_entry.Text;
                a.zavrsetak = zavrsetak_entry.Text;
                a.doza_sredstva = float.Parse(doza_entry.Text);
                a.trgovacki_naziv = trg_naziv_entry.Text;
                a.datum_slijedece_zastite = DateTime.Parse(sljed_zast_dateTimePicker.Text);
                List<Sorta> sorte = new List<Sorta>();
                baza.kon.Open();
                string traziSortu = "SELECT * FROM Sorta";
                sqlNaredba = new SQLiteCommand(traziSortu, baza.kon);
                SQLiteDataReader r = sqlNaredba.ExecuteReader();
                while (r.Read())
                {
                    Sorta y = new Sorta();
                    y.id = Convert.ToInt64(r["SortaID"]);
                    y.id_vin = Convert.ToInt64(r["VinogradID"]);
                    sorte.Add(y);
                }
                if (sorta1.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        
                        if (x.id == 1)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','"+X.vinograd.id_vin+"','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','"+a.doza_sredstva+"','"+a.datum_slijedece_zastite+"')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();                          
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));                                                      
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);                               
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }

                    }
                }
                if (sorta2.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        if (x.id == 2)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','" + a.doza_sredstva + "','" + a.datum_slijedece_zastite + "')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }
                    }
                }
                if (sorta3.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        if (x.id == 3)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','" + a.doza_sredstva + "','" + a.datum_slijedece_zastite + "')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);                          
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }
                    }
                }
                if (sorta4.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        if (x.id == 4)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','" + a.doza_sredstva + "','" + a.datum_slijedece_zastite + "')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }
                    }
                }
                if (sorta5.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        if (x.id == 5)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','" + a.doza_sredstva + "','" + a.datum_slijedece_zastite + "')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }
                    }
                }
                if (sorta6.Checked == true)
                {
                    foreach (Sorta x in X.vinograd.Sorte)
                    {
                        if (x.id == 6)
                        {
                            x.zastite.Add(a);
                            MessageBox.Show("Vaša zastita je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajZastitu = "INSERT INTO Zastita(SortaID,VinogradID,ZastitaDatum,ZastitaArkod,ZastitaNaziv,ZastitaTrgovacki_naziv,ZastitaZasticena_povrsina,ZastitaPocetak,ZastitaZavrsetak,ZastitaDoza,ZastitaSljedeca_zastita) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.arkod_id + "','" + a.naziv + "','" + a.trgovacki_naziv + "','" + a.zasticena_pov + "','" + a.pocetak + "','" + a.zavrsetak + "','" + a.doza_sredstva + "','" + a.datum_slijedece_zastite + "')";
                            sqlNaredba = new SQLiteCommand(dodajZastitu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            domaci_naziv_entry.Text = null;
                            arkod_entry.Text = null;
                            povha_entry.Text = null;
                            pocetak_entry.Text = null;
                            zavrsetak_entry.Text = null;
                            doza_entry.Text = null;
                            trg_naziv_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Naziv", typeof(string));
                            dt.Columns.Add("ArkodID", typeof(int));
                            dt.Columns.Add("Trgovacki_naziv", typeof(string));
                            dt.Columns.Add("Pocetak", typeof(string));
                            dt.Columns.Add("Zavrsteak", typeof(string));
                            dt.Columns.Add("Doza", typeof(string));
                            dt.Rows.Add(a.datum, a.naziv, a.arkod_id, a.trgovacki_naziv, a.pocetak, a.zavrsetak, a.doza_sredstva);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
                            }
                        }
                    }
                }
                r.Close();
                sqlNaredba.Dispose();
                baza.kon.Close();
                float prosjZastita = 0;
                int br = 0;
                foreach (Sorta x in X.vinograd.Sorte)
                {
                    foreach (Zastita y in x.zastite)
                    {
                        br++;
                        prosjZastita += y.zasticena_pov;
                    }
                }
                double prosjek = Kalkulator.ProsjecnoTrajanjeZastite(prosjZastita, br);
                prosj_zastite_label.Text = prosjek.ToString();
            }
        }

        private void OnVinetory_profilClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetory_vinogradClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetory_dsClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetoryyClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnIzmjeni_pod_profilClicked(object sender, EventArgs e)
        {
            X.ime = ime_izmjena_entry.Text;
            X.prezime = prezime_izmjena_entry.Text;
            baza.kon.Open();
            string updateKorisnikIme = "UPDATE Korisnik set KorisnikIme='" + X.ime + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateKorisnikIme, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            string updateKorisnikPrezime = "UPDATE Korisnik set KorisnikPrezime='" + X.prezime + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateKorisnikPrezime, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            MessageBox.Show("Vaši podaci su izmijenjeni.", "Obavijest!",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            sqlNaredba.Dispose();
            baza.kon.Close();
        }

        private void OnIzmjeni_pod_vinogradClicked(object sender, EventArgs e)
        {
            X.vinograd.oib = Int64.Parse(oib_izmjena_entry.Text);
            X.vinograd.mibpg = Int32.Parse(mibpg_izmjena_entry.Text);
            X.vinograd.udaljenost_cokota = Single.Parse(cokot_izmjena_entry.Text);
            X.vinograd.udaljenost_reda = Single.Parse(red_izmjena_entry.Text);
            baza.kon.Open();
            string updateVinogradOib = "UPDATE Vinograd set VinogradOib='" + X.vinograd.oib + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateVinogradOib, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            string updateVinogradMibpg = "UPDATE Vinograd set VinogradMibpg='" + X.vinograd.mibpg + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateVinogradMibpg, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            string updateVinogradCokot = "UPDATE Vinograd set VinogradCokot='" + X.vinograd.udaljenost_cokota + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateVinogradCokot, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            string updateVinogradRed = "UPDATE Vinograd set VinogradRed='" + X.vinograd.udaljenost_reda + "' WHERE KorisnikID='" + X.id + "'";
            sqlNaredba = new SQLiteCommand(updateVinogradRed, baza.kon);
            sqlNaredba.ExecuteNonQuery();
            MessageBox.Show("Vaši podaci su izmijenjeni.", "Obavijest!",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            sqlNaredba.Dispose();
            baza.kon.Close();
        }       
        private void sorta1_CheckedChanged(object sender, EventArgs e)
        {

            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 1 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void sorta2_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 2 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void sorta3_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 3 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void sorta4_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 4 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void sorta5_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 5 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void sorta6_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Zastita x in zastite)
            {
                if (x.id_sorte == 6 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
            }
            baza.kon.Close();
        }

        private void OnPretraziClicked(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Naziv", typeof(string));
            dt.Columns.Add("ArkodID", typeof(int));
            dt.Columns.Add("Trgovacki_naziv", typeof(string));
            dt.Columns.Add("Pocetak", typeof(string));
            dt.Columns.Add("Zavrsteak", typeof(string));
            dt.Columns.Add("Doza", typeof(string));
            dt.Columns.Add("Sorta", typeof(string));
            List<Zastita> zastite = new List<Zastita>();
            string traziZastitu = "SELECT * FROM Zastita WHERE ZastitaDatum BETWEEN '" + predtraga_odPicker.Text + "' AND '" + pretraga_doPicker.Text + "'";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
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
            foreach (Sorta z in X.vinograd.Sorte)
            {
                foreach (Zastita x in zastite)
                {
                    if (x.id_sorte==z.id && x.id_vin == X.vinograd.id_vin)
                    {
                        dt.Rows.Add(x.datum, x.naziv, x.arkod_id, x.trgovacki_naziv, x.pocetak, x.zavrsetak, x.doza_sredstva, z.ime_sorte);
                    }
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6], red[7]);
            }
            baza.kon.Close();
        }

        private void OnDodaj_sliku_profilClicked(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                string imagename = open.SafeFileName;
                profilBox.Image = img.GetThumbnailImage(144, 110, null, new IntPtr());
                open.RestoreDirectory = true;

            }
            MessageBox.Show("Vaša slika je spremljena.", "Obavijest!",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnDodaj_sliku_vinogradClicked(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                string imagename = open.SafeFileName;
                vinogradBox.Image = img.GetThumbnailImage(144, 110, null, new IntPtr());
                open.RestoreDirectory = true;

            }
            MessageBox.Show("Vaša slika je spremljena.", "Obavijest!",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
