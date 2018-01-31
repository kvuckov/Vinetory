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
    public partial class RegistracijaWindow : Form
    {
        private Korisnik a = new Korisnik();

        internal Korisnik A { get => a; set => a = value; }
        Database baza = new Database();
        SQLiteCommand sqlNaredba;
        public RegistracijaWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;          
        }
        private void OnNastaviClicked(object sender, EventArgs e)
        {          
            if (kor_ime_entry.Text=="" || oib_entry.Text == "" || mibpg_entry.Text == "" || red_udaljenost_entry.Text == "" || cokot_udaljenost_entry.Text == "")
            {
                MessageBox.Show("Potrebno je unijeti sve podatke", "Pozor!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                var registracija = new RegistracijaWindow();
                registracija.Show();
                this.Hide();
            }
            else
            {
                A.ime = ime_entry.Text;
                A.prezime = prezime_entry.Text;
                A.kor_ime = kor_ime_entry.Text;
                A.lozinka = lozinka_entry.Text;
                A.vinograd = new Vinograd();
                A.vinograd.oib = Int64.Parse(oib_entry.Text);
                A.vinograd.mibpg =Int32.Parse( mibpg_entry.Text);
                A.vinograd.broj_sorti = 0;
                A.vinograd.udaljenost_reda = Single.Parse(red_udaljenost_entry.Text);
                A.vinograd.udaljenost_cokota = Single.Parse(cokot_udaljenost_entry.Text);

                List<string> korisnici = new List<string>();
                
                bool postoji = false;
                baza.kon.Open();
                
                string traziKor = "SELECT * FROM Korisnik";
                sqlNaredba = new SQLiteCommand(traziKor, baza.kon);

                SQLiteDataReader r = sqlNaredba.ExecuteReader();
                while (r.Read())
                {
                    string ime = r["KorisnikKor_ime"].ToString();
                    korisnici.Add(ime);
                }
                foreach (string x in korisnici)
                {
                    if (x == A.kor_ime)
                    {
                        postoji = true;
                    }
                }
                if (postoji == true)
                {
                    MessageBox.Show("Korisnik sa tim korisničkim imenom već postoji. Odaberite drugo korisničko ime.", "Pozor!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var registracija = new RegistracijaWindow();
                    registracija.Show();
                    this.Close();
                }
                else
                {
                    try
                    {
                        string dodajKor = "INSERT INTO Korisnik(KorisnikIme,KorisnikPrezime,KorisnikKor_ime,KorisnikLozinka) VALUES ('" + A.ime + "','" + A.prezime + "','" + A.kor_ime + "','" + A.lozinka + "')";
                        sqlNaredba = new SQLiteCommand(dodajKor, baza.kon);
                        sqlNaredba.ExecuteNonQuery();
                        
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    r.Close();
                    sqlNaredba.Dispose();
                    baza.kon.Close();
                    
                    var uvodni = new UvodniWindow();
                    uvodni.X = A;
                    uvodni.Show();
                    uvodni.postaviLAbel();
                    uvodni.postavi_bazu();
                    this.Hide();
                }
                
            }
        }

       
    }
}
