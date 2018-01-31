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
    public partial class KalkulatorWindow : Form
    {
        private Korisnik x = new Korisnik();

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        public KalkulatorWindow()
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
            foreach (Sorta x in X.vinograd.Sorte)
            {
                if (x.id == 1)
                {
                    sorta1_label.Text = x.ime_sorte;
                   
                }
                if (x.id == 2)
                {
                    sorta2_label.Text = x.ime_sorte;
                  
                }
                if (x.id == 3)
                {
                    sorta3_label.Text = x.ime_sorte;
                  
                }
                if (x.id == 4)
                {
                    sorta4_label.Text = x.ime_sorte;
                    
                }
                if (x.id == 5)
                {
                    sorta5_label.Text = x.ime_sorte;
                }
                if (x.id == 6)
                {
                    sorta5_label.Text = x.ime_sorte;
                }
            }
        }
        private void OnIzmjeni_podatke_VinogradClicked(object sender, EventArgs e)
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

        private void OnIzracunaj1Clicked(object sender, EventArgs e)
        {
            float a, c, d, b;
            a = float.Parse(prvi_rač_x_entry.Text);
            b = float.Parse(prvi_rač_y_entry.Text);
            c = float.Parse(prvi_rac_grozdpup_entry.Text);
            d = float.Parse(prvi_rac_prosjtezina_entry.Text);
            double rezultat=Kalkulator.IzracunPupovaPoCokotu(b,c,d);
            y_label.Text = b.ToString();
            x_label.Text = a.ToString();
            br_pupova_label.Text = rezultat.ToString();
            float f = float.Parse(br_pupova_label.Text);
            double rezz = Kalkulator.IzracunPupovaPoCokotu10(f)+rezultat;
            int rez = (int)rezz;
            br_pupova10_label.Text = rez.ToString();
            float pov_cokota = (X.vinograd.udaljenost_cokota * X.vinograd.udaljenost_reda);
            double ha = 10000 * double.Parse(prvi_rač_x_entry.Text);
            double cokota_po_ha = ha / pov_cokota;
            double rodd = cokota_po_ha*b;
            int rod = (int)rodd;
            rod_label.Text = rod.ToString();
            pup_opterecen_label.Text = rez.ToString();
        }

        private void OnIzracunaj2Clicked(object sender, EventArgs e)
        {
            float a, c, d, b;
            a = float.Parse(drugi_rac_x_entry.Text);
            b = float.Parse(drugi_rac_y_entry.Text);
            c = float.Parse(drugi_rac_grozdpup_entry.Text);
            d = float.Parse(drugi_rac_posjtezina_entry.Text);
            rod_od_label.Text = b.ToString();
            povrsina_label.Text = a.ToString();
            double rezultatt = Kalkulator.IzracunPupovaPoCokotu(b, c, d);
            float rezultat = (float)rezultatt;
            broj_pupova2_label .Text = rezultat.ToString();
            float f = float.Parse(broj_pupova2_label.Text);                      
            float pov_cokota = (X.vinograd.udaljenost_cokota * X.vinograd.udaljenost_reda);
            float ha = 10000 * float.Parse(drugi_rac_x_entry.Text);
            float cokota_po_haa = ha / pov_cokota;
            int cokota_po_ha = (int)cokota_po_haa;
            float opterecenje = rezultat / cokota_po_ha;            
            broj_cokota2_label.Text = cokota_po_ha.ToString();
            opteretiti_pupova2_label.Text = opterecenje.ToString();
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
