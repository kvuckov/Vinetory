namespace Vinetory
{
    partial class PrijavaWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijavaWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kor_ime_entry = new System.Windows.Forms.TextBox();
            this.lozinka_entry = new System.Windows.Forms.TextBox();
            this.prijava_kor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Korisničko ime :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lozinka :";
            // 
            // kor_ime_entry
            // 
            this.kor_ime_entry.Location = new System.Drawing.Point(214, 136);
            this.kor_ime_entry.Name = "kor_ime_entry";
            this.kor_ime_entry.Size = new System.Drawing.Size(100, 20);
            this.kor_ime_entry.TabIndex = 2;
            // 
            // lozinka_entry
            // 
            this.lozinka_entry.Location = new System.Drawing.Point(214, 164);
            this.lozinka_entry.Name = "lozinka_entry";
            this.lozinka_entry.Size = new System.Drawing.Size(100, 20);
            this.lozinka_entry.TabIndex = 3;
            // 
            // prijava_kor
            // 
            this.prijava_kor.Location = new System.Drawing.Point(-1, 255);
            this.prijava_kor.Name = "prijava_kor";
            this.prijava_kor.Size = new System.Drawing.Size(440, 44);
            this.prijava_kor.TabIndex = 4;
            this.prijava_kor.Text = "Prijavi se";
            this.prijava_kor.UseVisualStyleBackColor = true;
            this.prijava_kor.Click += new System.EventHandler(this.OnPrijava_korClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 109);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(96, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 27);
            this.label9.TabIndex = 26;
            this.label9.Text = "Prijava";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.ForestGreen;
            this.label10.Location = new System.Drawing.Point(100, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 24);
            this.label10.TabIndex = 27;
            this.label10.Text = "Korisnički podaci";
            // 
            // PrijavaWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 304);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.prijava_kor);
            this.Controls.Add(this.lozinka_entry);
            this.Controls.Add(this.kor_ime_entry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrijavaWindow";
            this.Text = "Vinetory";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox kor_ime_entry;
        private System.Windows.Forms.TextBox lozinka_entry;
        private System.Windows.Forms.Button prijava_kor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}