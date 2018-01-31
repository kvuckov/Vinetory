using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinetory
{
    public partial class MainWindow : Form
    {         

        public MainWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
            
        private void OnRegistracija_mainClicked(object sender, EventArgs e)
        {
            var registracija = new RegistracijaWindow();
            registracija.Show();
        }

        private void OnPrijava_mainClicked(object sender, EventArgs e)
        {
            var prijava = new PrijavaWindow();
            prijava.Show();
        }
    }
}
