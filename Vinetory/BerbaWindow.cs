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
    public partial class BerbaWindow : Form
    {
        private Korisnik x;

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        public BerbaWindow()
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
            int brojac = 0;
            float broji_kg = 0;
            float b = 0;
            HashSet<DateTime> datumi = new HashSet<DateTime>();
            foreach (Sorta x in X.vinograd.Sorte)
            {
                foreach (Berba y in x.berbe)
                {
                    datumi.Add(y.datum);
                    broji_kg += y.obrano_kg;
                    b = y.obrano_kg / 1000;
                }
            }
            datumi.Count();
            brojac = datumi.Count;
            float c = Kalkulator.ProsjecnaBerba(broji_kg, brojac);
            prosjecna_berba_label.Text = c.ToString();
            odrzane_berbe_label.Text = brojac.ToString();
            zadnja_berba_label.Text = b.ToString();
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));          
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 1 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
            
        }
        private void OnSpremi_berbuClicked(object sender, EventArgs e)
        {
            if (utroseni_budzet_entry.Text == "" || sati_branja_entry.Text == "" || obrano_kg_entry.Text == "" || obrana_pov_entry.Text == "")
            {
                MessageBox.Show("Potrebno je unijeti sve podatke", "Pozor!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                var berba = new BerbaWindow();
                berba.X = X;
                berba.Show();
                berba.postaviLAbel();
                this.Hide();
            }
            else
            {
                Berba a = new Berba();
                a.datum = DateTime.Parse(datum_berbe_dateTimePicker.Text);
                a.broj_beraca = Int32.Parse(broj_beraca_entry.Text);
                a.utroseni_budzet = float.Parse(utroseni_budzet_entry.Text);
                a.sati_branja = float.Parse(sati_branja_entry.Text);
                a.obrano_kg = float.Parse(obrano_kg_entry.Text);
                a.obrana_pov = float.Parse(obrana_pov_entry.Text);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','"+a.sati_branja+"','"+a.obrano_kg+"','"+a.obrana_pov+"')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));                           
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);                                             
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','" + a.sati_branja + "','" + a.obrano_kg + "','" + a.obrana_pov + "')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','" + a.sati_branja + "','" + a.obrano_kg + "','" + a.obrana_pov + "')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','" + a.sati_branja + "','" + a.obrano_kg + "','" + a.obrana_pov + "')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','" + a.sati_branja + "','" + a.obrano_kg + "','" + a.obrana_pov + "')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
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
                            x.berbe.Add(a);
                            MessageBox.Show("Vaša berba je spremljena", "Obavijest!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string dodajBerbu = "INSERT INTO Berba(SortaID,VinogradID,BerbaDatum,BerbaBroj_beraca,BerbaUtroseni_budzet,BerbaSati_branja,BerbaObrano_kg,BerbaObrana_povrsina) VALUES ('" + x.id + "','" + X.vinograd.id_vin + "','" + a.datum + "','" + a.broj_beraca + "','" + a.utroseni_budzet + "','" + a.sati_branja + "','" + a.obrano_kg + "','" + a.obrana_pov + "')";
                            sqlNaredba = new SQLiteCommand(dodajBerbu, baza.kon);
                            sqlNaredba.ExecuteNonQuery();
                            broj_beraca_entry.Text = null;
                            utroseni_budzet_entry.Text = null;
                            sati_branja_entry.Text = null;
                            obrano_kg_entry.Text = null;
                            obrana_pov_entry.Text = null;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Datum", typeof(DateTime));
                            dt.Columns.Add("Broj_beraca", typeof(int));
                            dt.Columns.Add("Utroseni_budzet", typeof(float));
                            dt.Columns.Add("Sati_branja", typeof(string));
                            dt.Columns.Add("Obrano_kg", typeof(float));
                            dt.Columns.Add("Obrana_povrsina", typeof(float));
                            dt.Rows.Add(a.datum, a.broj_beraca, a.utroseni_budzet, a.sati_branja, a.obrano_kg, a.obrana_pov);
                            foreach (DataRow red in dt.Rows)
                            {
                                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
                            }
                        }
                    }
                }
                r.Close();
                sqlNaredba.Dispose();
                baza.kon.Close();
                int brojac = 0;
                float broji_kg = 0;
                float b = 0;
                foreach (Sorta x in X.vinograd.Sorte)
                {
                    foreach (Berba y in x.berbe)
                    {
                        brojac++;
                        broji_kg += y.obrano_kg;
                        b = y.obrano_kg / 1000;
                    }
                }
                float c = Kalkulator.ProsjecnaBerba(broji_kg, brojac);
                prosjecna_berba_label.Text = c.ToString();
                odrzane_berbe_label.Text = brojac.ToString();
                zadnja_berba_label.Text = b.ToString();
            }
        }

       
        private void OnDodaj_sortuClicked(object sender, EventArgs e)
        {
            X.vinograd.broj_sorti++;
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

        private void OnIzmjeni_pod_ProfilClicked(object sender, EventArgs e)
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
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 1 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
         
        }

        private void sorta2_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 2 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
          
        }

        private void sorta3_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 3 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
          
        }

        private void sorta4_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 4 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
        
        }

        private void sorta5_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 5 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
            baza.kon.Close();
     
        }

        private void sorta6_CheckedChanged(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(int));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Berba x in berbe)
            {
                if (x.id_sorte == 6 && x.id_vin == X.vinograd.id_vin)
                {
                    dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov);
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5]);
            }
  
            baza.kon.Close();
        }

     

        private void OnPretraziClicked(object sender, EventArgs e)
        {
            baza.kon.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Broj_beraca", typeof(string));
            dt.Columns.Add("Utroseni_budzet", typeof(float));
            dt.Columns.Add("Sati_branja", typeof(string));
            dt.Columns.Add("Obrano_kg", typeof(float));
            dt.Columns.Add("Obrana_povrsina", typeof(float));
            dt.Columns.Add("Sorta", typeof(string));
            List<Berba> berbe = new List<Berba>();
            string traziBerbu = "SELECT * FROM Berba WHERE BerbaDatum BETWEEN '"+predtraga_odPicker.Text+"' AND '"+pretraga_doPicker.Text+"'";
            sqlNaredba = new SQLiteCommand(traziBerbu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Berba temp = new Berba();
                temp.id = Convert.ToInt32(r["BerbaID"]);
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
            foreach (Sorta z in X.vinograd.Sorte)
            {
                foreach (Berba x in berbe)
                {
                    if (x.id_sorte == z.id && x.id_vin == X.vinograd.id_vin)
                    {
                        dt.Rows.Add(x.datum, x.broj_beraca, x.utroseni_budzet, x.sati_branja, x.obrano_kg, x.obrana_pov, z.ime_sorte);
                    }
                }
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow red in dt.Rows)
            {
                    dataGridView1.Rows.Add(red[0], red[1], red[2], red[3], red[4], red[5], red[6]);
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
