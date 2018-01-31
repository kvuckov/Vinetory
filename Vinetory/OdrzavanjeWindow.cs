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
    public partial class OdrzavanjeWindow : Form
    {
        private Korisnik x;

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        public OdrzavanjeWindow()
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
            mibpg_izmjena_entry.Text =Convert.ToString( X.vinograd.mibpg);
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
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 1 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }
        

        private void OnDodaj_sortuClicked(object sender, EventArgs e)
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

        private void OnZastitaClicked(object sender, EventArgs e)
        {
            var zastita = new ZastitaWindow();
            zastita.X = X;
            zastita.Show();
            zastita.postaviLAbel();
            this.Hide();
        }

        private void OnBerbaClicked(object sender, EventArgs e)
        {
            var berba = new BerbaWindow();
            berba.X = X;
            berba.Show();
            berba.postaviLAbel();
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
        private void OnSpremi_odrzavanjeClicked(object sender, EventArgs e)
        {
            Odrzavanje a = new Odrzavanje();
            a.vrsta_posla =vrsta_posla_entry.Text;
            a.opis_posla = opis_poslaTextBox.Text;
            a.datum=DateTime.Parse(datum_odrzavanja_dateTimePicker.Text);
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
            if (sorta1.Checked == true)
            {
                foreach (Sorta x in X.vinograd.Sorte)
                {
                     if (x.id == 1)
                    {
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
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
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
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
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
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
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
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
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
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
                        x.odrzavanja.Add(a);
                        MessageBox.Show("Vaše održavanje je spremljeno", "Obavijest!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string dodajOdrzavanje = "INSERT INTO Odrzavanje(SortaID,VinogradID,OdrzavanjeDatum,OdrzavanjeNaziv,OdrzavanjeOpis) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.vrsta_posla + "','" + a.opis_posla + "')";
                        sqlNaredba = new SQLiteCommand(dodajOdrzavanje, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        vrsta_posla_entry.Text = null;
                        opis_poslaTextBox.Text = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Datum", typeof(DateTime));
                        dt.Columns.Add("OdrzavanjeNaziv", typeof(string));
                        dt.Columns.Add("OdrzavanjeOpis", typeof(string));
                        dt.Rows.Add(a.datum, a.vrsta_posla, a.opis_posla);
                        foreach (DataRow red in dt.Rows)
                        {
                            dataGridView1.Rows.Add(red[0], red[1], red[2]);
                        }
                    }
                }
            }
            r.Close();
            sqlNaredba.Dispose();
            baza.kon.Close();
        }

        private void OnOdjavaClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnVinetoryClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetoryProfilClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetoryVinogradClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
        }

        private void OnVinetoryDSClicked(object sender, EventArgs e)
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

        private void OnIzmjeni_pod_VinogradClicked(object sender, EventArgs e)
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
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 1 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void sorta2_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 2 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void sorta3_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 3 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void sorta4_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 4 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void sorta5_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 5 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void sorta6_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Odrzavanje x in odrzavanja)
            {
                if (x.id_sorte == 6 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2]);
            }
            baza.kon.Close();
        }

        private void OnPretragaClicked(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Vrsta_posla", typeof(string));
            dt.Columns.Add("Opis_posla", typeof(string));
            dt.Columns.Add("Sorta", typeof(string));
            List<Odrzavanje> odrzavanja = new List<Odrzavanje>();
            string traziOdrzavanje = "SELECT * FROM Odrzavanje WHERE OdrzavanjeDatum BETWEEN '" + predtraga_odPicker.Text + "' AND '" + pretraga_doPicker.Text + "'";
            sqlNaredba = new SQLiteCommand(traziOdrzavanje, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Odrzavanje temp = new Odrzavanje();
                temp.id = Convert.ToInt32(r["OdrzavanjeID"]);
                temp.id_sorte = Convert.ToInt32(r["SortaID"]);
                temp.id_vin = Convert.ToInt32(r["VinogradID"]);
                temp.datum = Convert.ToDateTime(r["OdrzavanjeDatum"]);
                temp.vrsta_posla = Convert.ToString(r["OdrzavanjeNaziv"]);
                temp.opis_posla = Convert.ToString(r["OdrzavanjeOpis"]);
                odrzavanja.Add(temp);
            }
            foreach (Sorta z in X.vinograd.Sorte)
            {
                foreach (Odrzavanje x in odrzavanja)
                {
                    if (x.id_sorte==z.id && x.id_vin == X.vinograd.id_vin)
                    {
                        dt.Rows.Add(x.datum, x.vrsta_posla, x.opis_posla, z.ime_sorte);
                    }
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3]);
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
