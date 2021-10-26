
namespace GeoKviz
{
    partial class FrmPogodiGGrad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPogodiGGrad));
            this.panelSkor = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelVreme = new System.Windows.Forms.Label();
            this.labelSkor = new System.Windows.Forms.Label();
            this.panelPodesavanja = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAO = new System.Windows.Forms.CheckBox();
            this.cbSA = new System.Windows.Forms.CheckBox();
            this.cbJA = new System.Windows.Forms.CheckBox();
            this.cbAfrika = new System.Windows.Forms.CheckBox();
            this.cbAzija = new System.Windows.Forms.CheckBox();
            this.cbEvropa = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNaziv = new System.Windows.Forms.RadioButton();
            this.rbZastava = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelIgra = new System.Windows.Forms.Panel();
            this.pictureBoxZastava = new System.Windows.Forms.PictureBox();
            this.labelNazivDrzave = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSled = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.textBoxGrad = new System.Windows.Forms.TextBox();
            this.panelSkor.SuspendLayout();
            this.panelPodesavanja.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelIgra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZastava)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSkor
            // 
            this.panelSkor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSkor.Controls.Add(this.button2);
            this.panelSkor.Controls.Add(this.button1);
            this.panelSkor.Controls.Add(this.labelVreme);
            this.panelSkor.Controls.Add(this.labelSkor);
            this.panelSkor.Location = new System.Drawing.Point(475, 27);
            this.panelSkor.Name = "panelSkor";
            this.panelSkor.Size = new System.Drawing.Size(267, 394);
            this.panelSkor.TabIndex = 2;
            this.panelSkor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSkor_Paint);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(83, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 49);
            this.button2.TabIndex = 3;
            this.button2.Text = "Izlaz";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(83, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelVreme
            // 
            this.labelVreme.AutoSize = true;
            this.labelVreme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVreme.Location = new System.Drawing.Point(18, 121);
            this.labelVreme.Name = "labelVreme";
            this.labelVreme.Size = new System.Drawing.Size(82, 25);
            this.labelVreme.TabIndex = 1;
            this.labelVreme.Text = "Vreme:";
            // 
            // labelSkor
            // 
            this.labelSkor.AutoSize = true;
            this.labelSkor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkor.Location = new System.Drawing.Point(18, 52);
            this.labelSkor.Name = "labelSkor";
            this.labelSkor.Size = new System.Drawing.Size(122, 25);
            this.labelSkor.TabIndex = 0;
            this.labelSkor.Text = "Pogodjeno:";
            // 
            // panelPodesavanja
            // 
            this.panelPodesavanja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPodesavanja.Controls.Add(this.groupBox2);
            this.panelPodesavanja.Controls.Add(this.groupBox1);
            this.panelPodesavanja.Controls.Add(this.buttonStart);
            this.panelPodesavanja.Location = new System.Drawing.Point(12, 27);
            this.panelPodesavanja.Name = "panelPodesavanja";
            this.panelPodesavanja.Size = new System.Drawing.Size(457, 394);
            this.panelPodesavanja.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAO);
            this.groupBox2.Controls.Add(this.cbSA);
            this.groupBox2.Controls.Add(this.cbJA);
            this.groupBox2.Controls.Add(this.cbAfrika);
            this.groupBox2.Controls.Add(this.cbAzija);
            this.groupBox2.Controls.Add(this.cbEvropa);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(200, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 162);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kontinenti";
            // 
            // cbAO
            // 
            this.cbAO.AutoSize = true;
            this.cbAO.Checked = true;
            this.cbAO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAO.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAO.Location = new System.Drawing.Point(122, 111);
            this.cbAO.Name = "cbAO";
            this.cbAO.Size = new System.Drawing.Size(103, 38);
            this.cbAO.TabIndex = 5;
            this.cbAO.Text = "Australija \r\ni okeanija";
            this.cbAO.UseVisualStyleBackColor = true;
            // 
            // cbSA
            // 
            this.cbSA.AutoSize = true;
            this.cbSA.Checked = true;
            this.cbSA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSA.Location = new System.Drawing.Point(122, 76);
            this.cbSA.Name = "cbSA";
            this.cbSA.Size = new System.Drawing.Size(108, 21);
            this.cbSA.TabIndex = 4;
            this.cbSA.Text = "S. Amerika";
            this.cbSA.UseVisualStyleBackColor = true;
            // 
            // cbJA
            // 
            this.cbJA.AutoSize = true;
            this.cbJA.Checked = true;
            this.cbJA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbJA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbJA.Location = new System.Drawing.Point(122, 36);
            this.cbJA.Name = "cbJA";
            this.cbJA.Size = new System.Drawing.Size(106, 21);
            this.cbJA.TabIndex = 3;
            this.cbJA.Text = "J. Amerika";
            this.cbJA.UseVisualStyleBackColor = true;
            // 
            // cbAfrika
            // 
            this.cbAfrika.AutoSize = true;
            this.cbAfrika.Checked = true;
            this.cbAfrika.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAfrika.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAfrika.Location = new System.Drawing.Point(16, 120);
            this.cbAfrika.Name = "cbAfrika";
            this.cbAfrika.Size = new System.Drawing.Size(72, 21);
            this.cbAfrika.TabIndex = 2;
            this.cbAfrika.Text = "Afrika";
            this.cbAfrika.UseVisualStyleBackColor = true;
            // 
            // cbAzija
            // 
            this.cbAzija.AutoSize = true;
            this.cbAzija.Checked = true;
            this.cbAzija.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAzija.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAzija.Location = new System.Drawing.Point(16, 76);
            this.cbAzija.Name = "cbAzija";
            this.cbAzija.Size = new System.Drawing.Size(65, 21);
            this.cbAzija.TabIndex = 1;
            this.cbAzija.Text = "Azija";
            this.cbAzija.UseVisualStyleBackColor = true;
            // 
            // cbEvropa
            // 
            this.cbEvropa.AutoSize = true;
            this.cbEvropa.Checked = true;
            this.cbEvropa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEvropa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEvropa.Location = new System.Drawing.Point(16, 36);
            this.cbEvropa.Name = "cbEvropa";
            this.cbEvropa.Size = new System.Drawing.Size(81, 21);
            this.cbEvropa.TabIndex = 0;
            this.cbEvropa.Text = "Evropa";
            this.cbEvropa.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNaziv);
            this.groupBox1.Controls.Add(this.rbZastava);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zadato:";
            // 
            // rbNaziv
            // 
            this.rbNaziv.AutoSize = true;
            this.rbNaziv.Checked = true;
            this.rbNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNaziv.Location = new System.Drawing.Point(8, 48);
            this.rbNaziv.Name = "rbNaziv";
            this.rbNaziv.Size = new System.Drawing.Size(126, 22);
            this.rbNaziv.TabIndex = 1;
            this.rbNaziv.TabStop = true;
            this.rbNaziv.Text = "Naziv drzave";
            this.rbNaziv.UseVisualStyleBackColor = true;
            // 
            // rbZastava
            // 
            this.rbZastava.AutoSize = true;
            this.rbZastava.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbZastava.Location = new System.Drawing.Point(8, 93);
            this.rbZastava.Name = "rbZastava";
            this.rbZastava.Size = new System.Drawing.Size(143, 22);
            this.rbZastava.TabIndex = 0;
            this.rbZastava.Text = "Zastava drzave";
            this.rbZastava.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(126, 232);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(189, 102);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelIgra
            // 
            this.panelIgra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIgra.Controls.Add(this.pictureBoxZastava);
            this.panelIgra.Controls.Add(this.labelNazivDrzave);
            this.panelIgra.Controls.Add(this.textBox1);
            this.panelIgra.Controls.Add(this.buttonSled);
            this.panelIgra.Controls.Add(this.buttonPrev);
            this.panelIgra.Controls.Add(this.textBoxGrad);
            this.panelIgra.Location = new System.Drawing.Point(12, 27);
            this.panelIgra.Name = "panelIgra";
            this.panelIgra.Size = new System.Drawing.Size(457, 394);
            this.panelIgra.TabIndex = 4;
            // 
            // pictureBoxZastava
            // 
            this.pictureBoxZastava.Location = new System.Drawing.Point(62, 34);
            this.pictureBoxZastava.Name = "pictureBoxZastava";
            this.pictureBoxZastava.Size = new System.Drawing.Size(337, 177);
            this.pictureBoxZastava.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxZastava.TabIndex = 12;
            this.pictureBoxZastava.TabStop = false;
            // 
            // labelNazivDrzave
            // 
            this.labelNazivDrzave.AutoSize = true;
            this.labelNazivDrzave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNazivDrzave.Location = new System.Drawing.Point(58, 56);
            this.labelNazivDrzave.Name = "labelNazivDrzave";
            this.labelNazivDrzave.Size = new System.Drawing.Size(75, 20);
            this.labelNazivDrzave.TabIndex = 11;
            this.labelNazivDrzave.Text = "Drzava:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(62, 85);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(337, 34);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSled
            // 
            this.buttonSled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSled.Location = new System.Drawing.Point(285, 322);
            this.buttonSled.Name = "buttonSled";
            this.buttonSled.Size = new System.Drawing.Size(114, 34);
            this.buttonSled.TabIndex = 9;
            this.buttonSled.Text = "Sledeci";
            this.buttonSled.UseVisualStyleBackColor = true;
            this.buttonSled.Click += new System.EventHandler(this.buttonSled_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrev.Location = new System.Drawing.Point(62, 322);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(114, 34);
            this.buttonPrev.TabIndex = 8;
            this.buttonPrev.Text = "Prethodni";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // textBoxGrad
            // 
            this.textBoxGrad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGrad.Location = new System.Drawing.Point(62, 266);
            this.textBoxGrad.Name = "textBoxGrad";
            this.textBoxGrad.Size = new System.Drawing.Size(337, 34);
            this.textBoxGrad.TabIndex = 7;
            this.textBoxGrad.TextChanged += new System.EventHandler(this.textBoxGrad_TextChanged);
            // 
            // FrmPogodiGGrad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 441);
            this.Controls.Add(this.panelIgra);
            this.Controls.Add(this.panelPodesavanja);
            this.Controls.Add(this.panelSkor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPogodiGGrad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POGODI GLAVNI GRAD";
            this.Load += new System.EventHandler(this.FrmPogodiGGrad_Load);
            this.panelSkor.ResumeLayout(false);
            this.panelSkor.PerformLayout();
            this.panelPodesavanja.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelIgra.ResumeLayout(false);
            this.panelIgra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZastava)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSkor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelVreme;
        private System.Windows.Forms.Label labelSkor;
        private System.Windows.Forms.Panel panelPodesavanja;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbAO;
        private System.Windows.Forms.CheckBox cbSA;
        private System.Windows.Forms.CheckBox cbJA;
        private System.Windows.Forms.CheckBox cbAfrika;
        private System.Windows.Forms.CheckBox cbAzija;
        private System.Windows.Forms.CheckBox cbEvropa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNaziv;
        private System.Windows.Forms.RadioButton rbZastava;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelIgra;
        private System.Windows.Forms.PictureBox pictureBoxZastava;
        private System.Windows.Forms.Label labelNazivDrzave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSled;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.TextBox textBoxGrad;
    }
}