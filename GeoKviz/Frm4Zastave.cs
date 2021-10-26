using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoKviz
{
    public partial class Frm4Zastave : Form
    {

        OleDbConnection konekcija;
        OleDbCommand komanda;
        OleDbDataAdapter adapter;
        DataTable tabelaDrzava;
        DataTable tabelaDrzaveNivo;

        int[] zadato = new int[195];
        int trenutni;
        string naziv;
        string tacanNazivSlike;
        string[] pogresniNaziviSlika = new string[3];

        int tezakNivoVrsta, srednjiNivoVrsta;

        int tacnoDugme;

        int m, s;
        string mm, ss;

        int brPogodaka;

       

        public Frm4Zastave()
        {
            InitializeComponent();
            /*
            string putanja = Environment.CurrentDirectory;
            string[] putanjaBaze = putanja.Split(new string[] { "bin" }, StringSplitOptions.None);
            AppDomain.CurrentDomain.SetData("DataDirectory", putanjaBaze[0]);
            */
            konekcija = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\GeoQuizBaza.accdb");
            
            }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = false;
            textBoxNaziv.Visible = false;
            labelVreme.Visible = false;
            labelRez.Visible = false;
            buttonStart.Visible = true;
            rbLako.Checked = rb30.Checked = true;
            groupBox1.Enabled = groupBox2.Enabled = true;
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 3)
            {
                pictureBox2.BackColor = Color.Transparent;
                pictureBox1.BackColor = Color.Transparent;
                pictureBox4.BackColor = Color.Transparent;


                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;

                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                tacanNazivSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
                textBoxNaziv.Text = naziv;
                tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
                srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
                konekcija.Close();

                //UZIMANJE POGRESNIH ODGOVORA
                Random rand2 = new Random();
                int pom, maxbr = 0;
                if (rbTesko.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE TezakNivo=" + tezakNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }
                if (rbSrednje.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE SrednjiNivo=" + srednjiNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }

                for (int i = 0; i < 3; i++)
                {

                    if (rbLako.Checked)
                    {
                        do
                        {
                            pom = rand2.Next(1, 196);
                        } while (pom == zadato[trenutni]);

                        konekcija.Open();
                        komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + pom, konekcija);
                        adapter = new OleDbDataAdapter(komanda);
                        tabelaDrzava = new DataTable();
                        adapter.Fill(tabelaDrzava);
                        pogresniNaziviSlika[i] = tabelaDrzava.Rows[0]["Zastava"].ToString();
                        konekcija.Close();
                    }
                    else if (rbTesko.Checked || rbSrednje.Checked)
                    {
                        bool novi = true;
                        do
                        {
                            pom = rand2.Next(0, maxbr);
                            novi = true;
                            for (int j = 0; j < i; j++)
                            {
                                if (pogresniNaziviSlika[j] == tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString())
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNaziviSlika[i] = tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString();

                    }


                }

                //RASPORED ODGOVORA
                tacnoDugme = rand2.Next(1, 5);
                if (tacnoDugme == 1)
                {
                    pictureBox1.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);

                }
                else if (tacnoDugme == 2)
                {
                    pictureBox2.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 3)
                {
                    pictureBox3.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 4)
                {
                    pictureBox4.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }

            }
            else
            {
                pictureBox3.BackColor = Color.FromArgb(200, 255, 0, 0);

                s -= 2;
                if (m > 0 && s < 0)
                {
                    m--;
                    s = 59;
                }
                else if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                    timer1.Enabled = false;
                    MessageBox.Show("Kraj igre!\nVas rezultat je: " + brPogodaka);
                    pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = false;
                }

                if(m==0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                }
                else
                {
                    if (s < 10)
                    {
                        ss = "0" + s;
                    }
                    else
                    {
                        ss = s.ToString();
                    }


                    mm = "0" + m;


                    labelVreme.Text = mm + ":" + ss;
                }


            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 2)
            {
                pictureBox3.BackColor = Color.Transparent;
                pictureBox1.BackColor = Color.Transparent;
                pictureBox4.BackColor = Color.Transparent;


                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;

                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                tacanNazivSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
                textBoxNaziv.Text = naziv;
                tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
                srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
                konekcija.Close();

                //UZIMANJE POGRESNIH ODGOVORA
                Random rand2 = new Random();
                int pom, maxbr = 0;
                if (rbTesko.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE TezakNivo=" + tezakNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }
                if (rbSrednje.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE SrednjiNivo=" + srednjiNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }

                for (int i = 0; i < 3; i++)
                {

                    if (rbLako.Checked)
                    {
                        do
                        {
                            pom = rand2.Next(1, 196);
                        } while (pom == zadato[trenutni]);

                        konekcija.Open();
                        komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + pom, konekcija);
                        adapter = new OleDbDataAdapter(komanda);
                        tabelaDrzava = new DataTable();
                        adapter.Fill(tabelaDrzava);
                        pogresniNaziviSlika[i] = tabelaDrzava.Rows[0]["Zastava"].ToString();
                        konekcija.Close();
                    }
                    else if (rbTesko.Checked || rbSrednje.Checked)
                    {
                        bool novi = true;
                        do
                        {
                            pom = rand2.Next(0, maxbr);
                            novi = true;
                            for (int j = 0; j < i; j++)
                            {
                                if (pogresniNaziviSlika[j] == tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString())
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNaziviSlika[i] = tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString();

                    }


                }

                //RASPORED ODGOVORA
                tacnoDugme = rand2.Next(1, 5);
                if (tacnoDugme == 1)
                {
                    pictureBox1.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);

                }
                else if (tacnoDugme == 2)
                {
                    pictureBox2.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 3)
                {
                    pictureBox3.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 4)
                {
                    pictureBox4.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }

            }
            else
            {
                pictureBox2.BackColor = Color.FromArgb(200, 255, 0, 0);

                s -= 2;
                if (m > 0 && s < 0)
                {
                    m--;
                    s = 59;
                }
                else if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                    timer1.Enabled = false;
                    MessageBox.Show("Kraj igre!\nVas rezultat je: " + brPogodaka);
                    pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = false;
                }

                if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                }
                else
                {
                    if (s < 10)
                    {
                        ss = "0" + s;
                    }
                    else
                    {
                        ss = s.ToString();
                    }


                    mm = "0" + m;


                    labelVreme.Text = mm + ":" + ss;
                }


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 1)
            {
                pictureBox2.BackColor = Color.Transparent;
                pictureBox3.BackColor = Color.Transparent;
                pictureBox4.BackColor = Color.Transparent;


                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;

                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                tacanNazivSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
                textBoxNaziv.Text = naziv;
                tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
                srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
                konekcija.Close();

                //UZIMANJE POGRESNIH ODGOVORA
                Random rand2 = new Random();
                int pom, maxbr = 0;
                if (rbTesko.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE TezakNivo=" + tezakNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }
                if (rbSrednje.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE SrednjiNivo=" + srednjiNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }

                for (int i = 0; i < 3; i++)
                {

                    if (rbLako.Checked)
                    {
                        do
                        {
                            pom = rand2.Next(1, 196);
                        } while (pom == zadato[trenutni]);

                        konekcija.Open();
                        komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + pom, konekcija);
                        adapter = new OleDbDataAdapter(komanda);
                        tabelaDrzava = new DataTable();
                        adapter.Fill(tabelaDrzava);
                        pogresniNaziviSlika[i] = tabelaDrzava.Rows[0]["Zastava"].ToString();
                        konekcija.Close();
                    }
                    else if (rbTesko.Checked || rbSrednje.Checked)
                    {
                        bool novi = true;
                        do
                        {
                            pom = rand2.Next(0, maxbr);
                            novi = true;
                            for (int j = 0; j < i; j++)
                            {
                                if (pogresniNaziviSlika[j] == tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString())
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNaziviSlika[i] = tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString();

                    }


                }

                //RASPORED ODGOVORA
                tacnoDugme = rand2.Next(1, 5);
                if (tacnoDugme == 1)
                {
                    pictureBox1.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);

                }
                else if (tacnoDugme == 2)
                {
                    pictureBox2.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 3)
                {
                    pictureBox3.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 4)
                {
                    pictureBox4.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }

            }
            else
            {
                pictureBox1.BackColor = Color.FromArgb(200, 255, 0, 0);

                s -= 2;
                if (m > 0 && s < 0)
                {
                    m--;
                    s = 59;
                }
                else if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                    timer1.Enabled = false;
                    MessageBox.Show("Kraj igre!\nVas rezultat je: " + brPogodaka);
                    pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = false;
                }

                if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                }
                else
                {
                    if (s < 10)
                    {
                        ss = "0" + s;
                    }
                    else
                    {
                        ss = s.ToString();
                    }


                    mm = "0" + m;


                    labelVreme.Text = mm + ":" + ss;
                }


            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 4)
            {
                pictureBox2.BackColor = Color.Transparent;
                pictureBox1.BackColor = Color.Transparent;
                pictureBox3.BackColor = Color.Transparent;


                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;

                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                tacanNazivSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
                textBoxNaziv.Text = naziv;
                tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
                srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
                konekcija.Close();

                //UZIMANJE POGRESNIH ODGOVORA
                Random rand2 = new Random();
                int pom, maxbr = 0;
                if (rbTesko.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE TezakNivo=" + tezakNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }
                if (rbSrednje.Checked)
                {
                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE SrednjiNivo=" + srednjiNivoVrsta, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzaveNivo = new DataTable();
                    adapter.Fill(tabelaDrzaveNivo);
                    konekcija.Close();
                    maxbr = tabelaDrzaveNivo.Rows.Count;
                }

                for (int i = 0; i < 3; i++)
                {

                    if (rbLako.Checked)
                    {
                        do
                        {
                            pom = rand2.Next(1, 196);
                        } while (pom == zadato[trenutni]);

                        konekcija.Open();
                        komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + pom, konekcija);
                        adapter = new OleDbDataAdapter(komanda);
                        tabelaDrzava = new DataTable();
                        adapter.Fill(tabelaDrzava);
                        pogresniNaziviSlika[i] = tabelaDrzava.Rows[0]["Zastava"].ToString();
                        konekcija.Close();
                    }
                    else if (rbTesko.Checked || rbSrednje.Checked)
                    {
                        bool novi = true;
                        do
                        {
                            pom = rand2.Next(0, maxbr);
                            novi = true;
                            for (int j = 0; j < i; j++)
                            {
                                if (pogresniNaziviSlika[j] == tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString())
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNaziviSlika[i] = tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString();

                    }


                }

                //RASPORED ODGOVORA
                tacnoDugme = rand2.Next(1, 5);
                if (tacnoDugme == 1)
                {
                    pictureBox1.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);

                }
                else if (tacnoDugme == 2)
                {
                    pictureBox2.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 3)
                {
                    pictureBox3.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }
                else if (tacnoDugme == 4)
                {
                    pictureBox4.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                    pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                    pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                    pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
                }

            }
            else
            {
                pictureBox4.BackColor = Color.FromArgb(200, 255, 0, 0);

                s -= 2;
                if (m > 0 && s < 0)
                {
                    m--;
                    s = 59;
                }
                else if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                    timer1.Enabled = false;
                    MessageBox.Show("Kraj igre!\nVas rezultat je: " + brPogodaka);
                    pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = false;
                }

                if (m == 0 && s <= 0)
                {
                    labelVreme.Text = "00:00";
                }
                else
                {
                    if (s < 10)
                    {
                        ss = "0" + s;
                    }
                    else
                    {
                        ss = s.ToString();
                    }


                    mm = "0" + m;


                    labelVreme.Text = mm + ":" + ss;
                }


            }
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Frm4Zastave_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = false;
            textBoxNaziv.Visible = false;
            labelVreme.Visible = false;
            labelRez.Visible = false;
            buttonStart.Visible = true;
            rbLako.Checked = rb30.Checked = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //VIDLJIVOST KOMPONENTI
            pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = true;
            textBoxNaziv.Visible = true;
            labelVreme.Visible = true;
            labelRez.Visible = true;
            buttonStart.Visible = false;
            groupBox1.Enabled = groupBox2.Enabled = false;
            pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = true;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;

            //TAJMER
            if (rb30.Checked)
            {
                m = 0; s = 30;
            }
            else if (rb60.Checked)
            {
                m = 1; s = 0;
            }
            else if (rb90.Checked)
            {
                m = 1; s = 30;
            }
            else
            {
                m = 2; s = 0;
            }

            if (s < 10)
            {
                ss = "0" + s;
            }
            else
            {
                ss = s.ToString();
            }
            mm = "0" + m;
            labelVreme.Text = mm + ":" + ss;
            labelRez.Text = "Rezultat: 0";
            brPogodaka = 0;

            //POSTAVLJANJE REDOSLEDA
            Random rand = new Random();
            int rbr;
            for (int i = 0; i < 195; i++)
            {
                bool novi;
                do
                {
                    rbr = rand.Next(1, 196);
                    novi = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (rbr == zadato[j])
                        {
                            novi = false;
                            break;
                        }
                    }
                } while (novi == false);

                zadato[i] = rbr;

            }

            //UZIMANJE PRVE DRZAVE
            trenutni = 0;
            konekcija.Open();
            komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
            adapter = new OleDbDataAdapter(komanda);
            tabelaDrzava = new DataTable();
            adapter.Fill(tabelaDrzava);
            tacanNazivSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
            naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
            if (naziv.Contains(","))
            {
                int z = naziv.IndexOf(',');
                naziv = naziv.Substring(0, z);
            }
            textBoxNaziv.Text = naziv;
            tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
            srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
            konekcija.Close();

            //UZIMANJE POGRESNIH ODGOVORA
            Random rand2 = new Random();
            int pom, maxbr = 0;

            if (rbTesko.Checked)
            {
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE TezakNivo=" + tezakNivoVrsta, konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzaveNivo = new DataTable();
                adapter.Fill(tabelaDrzaveNivo);
                konekcija.Close();
                maxbr = tabelaDrzaveNivo.Rows.Count;
            }
            if (rbSrednje.Checked)
            {
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE SrednjiNivo=" + srednjiNivoVrsta, konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzaveNivo = new DataTable();
                adapter.Fill(tabelaDrzaveNivo);
                konekcija.Close();
                maxbr = tabelaDrzaveNivo.Rows.Count;
            }

            for (int i = 0; i < 3; i++)
            {

                if (rbLako.Checked)
                {
                    do
                    {
                        pom = rand2.Next(1, 196);
                    } while (pom == zadato[trenutni]);

                    konekcija.Open();
                    komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + pom, konekcija);
                    adapter = new OleDbDataAdapter(komanda);
                    tabelaDrzava = new DataTable();
                    adapter.Fill(tabelaDrzava);
                    pogresniNaziviSlika[i] = tabelaDrzava.Rows[0]["Zastava"].ToString();
                    konekcija.Close();
                }
                else if (rbTesko.Checked || rbSrednje.Checked)
                {
                    bool novi = true;
                    do
                    {
                        pom = rand2.Next(0, maxbr);
                        novi = true;
                        for (int j = 0; j < i; j++)
                        {
                            if (pogresniNaziviSlika[j] == tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString())
                            {
                                novi = false;
                                break;
                            }
                        }
                    } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                    pogresniNaziviSlika[i] = tabelaDrzaveNivo.Rows[pom]["Zastava"].ToString();

                }


            }

            //RASPORED ODGOVORA
            tacnoDugme = rand2.Next(1, 5);
            if (tacnoDugme == 1)
            {
                pictureBox1.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);

            }
            else if (tacnoDugme == 2)
            {
                pictureBox2.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
            }
            else if (tacnoDugme == 3)
            {
                pictureBox3.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                pictureBox4.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
            }
            else if (tacnoDugme == 4)
            {
                pictureBox4.Image = Image.FromFile(@"zastave\" + tacanNazivSlike);
                pictureBox2.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[0]);
                pictureBox3.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[1]);
                pictureBox1.Image = Image.FromFile(@"zastave\" + pogresniNaziviSlika[2]);
            }


            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            if (m > 0 && s < 0)
            {
                m--;
                s = 59;
            }
            else if (m == 0 && s <= 0)
            {
                labelVreme.Text = "00:00";
                timer1.Enabled = false;
                MessageBox.Show("Kraj igre!\nVas rezultat je: " + brPogodaka);
                pictureBox1.Enabled = pictureBox2.Enabled = pictureBox3.Enabled = pictureBox4.Enabled = false;
            }


            if (s < 10)
            {
                ss = "0" + s;
            }
            else
            {
                ss = s.ToString();
            }


            mm = "0" + m;

            labelVreme.Text = mm + ":" + ss;
        }
    }
}
