namespace Vinetory
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.prijava_main = new System.Windows.Forms.Button();
            this.registracija_main = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(86, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(532, 204);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.registracija_main);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(361, 238);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(257, 100);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.prijava_main);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(86, 238);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(277, 100);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // prijava_main
            // 
            this.prijava_main.BackColor = System.Drawing.SystemColors.Control;
            this.prijava_main.Location = new System.Drawing.Point(3, 3);
            this.prijava_main.Name = "prijava_main";
            this.prijava_main.Size = new System.Drawing.Size(271, 97);
            this.prijava_main.TabIndex = 0;
            this.prijava_main.Text = "Prijavi se";
            this.prijava_main.UseVisualStyleBackColor = false;
            this.prijava_main.Click += new System.EventHandler(this.OnPrijava_mainClicked);
            // 
            // registracija_main
            // 
            this.registracija_main.BackColor = System.Drawing.SystemColors.Control;
            this.registracija_main.ForeColor = System.Drawing.SystemColors.ControlText;
            this.registracija_main.Location = new System.Drawing.Point(3, 3);
            this.registracija_main.Name = "registracija_main";
            this.registracija_main.Size = new System.Drawing.Size(254, 97);
            this.registracija_main.TabIndex = 1;
            this.registracija_main.Text = "Registriraj se";
            this.registracija_main.UseVisualStyleBackColor = false;
            this.registracija_main.Click += new System.EventHandler(this.OnRegistracija_mainClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(693, 409);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Vinetory";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button registracija_main;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button prijava_main;
    }
}

