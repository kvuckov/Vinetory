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
using System.Drawing.Imaging;
namespace Vinetory
{
    public partial class GlavniWindow : Form
    {
        private Korisnik x= new Korisnik();
       

        internal Korisnik X { get => x; set => x = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        public GlavniWindow()
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
                if (x.id == 1) {
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
            List<Zastita> zastite = new List<Zastita>();
            baza.kon.Open();
            string traziZastitu = "SELECT * FROM Zastita WHERE ZastitaSljedeca_zastita>= '"+DateTime.Today+"' ";
            sqlNaredba = new SQLiteCommand(traziZastitu, baza.kon);
            SQLiteDataReader r = sqlNaredba.ExecuteReader();
            while (r.Read())
            {
                Zastita y = new Zastita();
                y.id_vin = Convert.ToInt32(r["VinogradID"]);
                y.id_sorte = Convert.ToInt32(r["SortaID"]);
                y.naziv = Convert.ToString(r["ZastitaNaziv"]);
                y.trgovacki_naziv = Convert.ToString(r["ZastitaTrgovacki_naziv"]);
                y.datum_slijedece_zastite = Convert.ToDateTime(r["ZastitaSljedeca_zastita"]);
                zastite.Add(y);
            }
            r.Close();
            sqlNaredba.Dispose();
            baza.kon.Close();
            int brojac = 0;            
            foreach(Zastita x in zastite)
            {
                if (x.id_vin == X.vinograd.id_vin)
                {
                    brojac++;
                    if (brojac == 1)
                    {

                        novosti1_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 2)
                    {

                        novosti2_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 3)
                    {

                        novosti3_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 4)
                    {

                        novosti4_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 5)
                    {

                        novosti5_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 6)
                    {

                        novosti6_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 7)
                    {

                        novosti7_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 8)
                    {

                        novosti8_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 9)
                    {

                        novosti9_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                    if (brojac == 10)
                    {

                        novosti10_label.Text = Convert.ToString("- " + x.datum_slijedece_zastite + " potrebno je nanijeti zastitu tipa - " + x.trgovacki_naziv + " ( " + x.naziv + " )");
                    }
                }
            }
        }
        private void OnOdjavaClicked(object sender, EventArgs e)
        {
            this.Close();           
        }

 
        private void OnDodaj_sortu_dodajnoviClicked(object sender, EventArgs e)
        {
            if (ime_sorte_dodajnovi_entry.Text == "" || oplem_oznaka_dodajnovu_entry.Text == "" || pov_zemlje_dodajnovi_entry.Text == "")
            {
                MessageBox.Show("Potrebno je unijeti sve podatke", "Pozor!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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
                            string updateBr_s = "UPDATE Vinograd set VinogradBroj_sorti='"+X.vinograd.broj_sorti+"' WHERE KorisnikID='"+X.id+"'";
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

        private void OnIshrana_i_gnojudbaClicked(object sender, EventArgs e)
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

        private void OnVinetoryyClicked(object sender, EventArgs e)
        {
            var glavni = new GlavniWindow();
            glavni.X = X;
            glavni.Show();
            glavni.postaviLAbel();
            this.Hide();
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

        private void OnIzmjeni_podVinogradClicked(object sender, EventArgs e)
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
      
        private void OnDodaj_fotoProfilClicked(object sender, EventArgs e)
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

        private void OnDodaj_fotoVinogradClicked(object sender, EventArgs e)
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
