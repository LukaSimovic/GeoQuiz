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
    public partial class Frm4Drzave : Form
    {
        OleDbConnection konekcija;
        OleDbCommand komanda;
        OleDbDataAdapter adapter;
        DataTable tabelaDrzava;
        DataTable tabelaDrzaveNivo;

        int[] zadato = new int[195];
        int trenutni;
        string naziv;
        string[] pogresniNazivi = new string[3];

        int tezakNivoVrsta, srednjiNivoVrsta;

        int tacnoDugme;

        int m, s;
        string mm, ss;

        int brPogodaka;

        public Frm4Drzave()
        {
            InitializeComponent();
            /*
            string putanja = Environment.CurrentDirectory;
            string[] putanjaBaze = putanja.Split(new string[] { "bin" }, StringSplitOptions.None);
            AppDomain.CurrentDomain.SetData("DataDirectory", putanjaBaze[0]);
            */
            konekcija = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\GeoQuizBaza.accdb");
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Frm4Drzave_Load(object sender, EventArgs e)
        {
            button1.Visible = button2.Visible = button3.Visible = button4.Visible = false;
            pictureBox1.Visible = false;
            labelVreme.Visible = false;
            labelRez.Visible = false;
            buttonStart.Visible = true;
            rbLako.Checked = rb30.Checked = true;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //VIDLJIVOST KOMPONENTI
            button1.Visible = button2.Visible = button3.Visible = button4.Visible = true;
            pictureBox1.Visible = true;
            labelVreme.Visible = true;
            labelRez.Visible = true;
            buttonStart.Visible = false;
            groupBox1.Enabled = groupBox2.Enabled = false;
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = true;
            button2.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;


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
            string imeSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
            pictureBox1.Image = Image.FromFile(@"zastave\" + imeSlike);
            naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
            if (naziv.Contains(","))
            {
                int z = naziv.IndexOf(',');
                naziv = naziv.Substring(0,z);
            }
            tezakNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["TezakNivo"].ToString());
            srednjiNivoVrsta = Int32.Parse(tabelaDrzava.Rows[0]["SrednjiNivo"].ToString());
            konekcija.Close();


            //UZIMANJE POGRESNIH ODGOVORA
            Random rand2 = new Random();
            int pom, maxbr=0;

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

            for (int i=0; i<3; i++)
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
                    pogresniNazivi[i] = tabelaDrzava.Rows[0]["Naziv"].ToString();
                    if (pogresniNazivi[i].Contains(","))
                    {
                        int z = pogresniNazivi[i].IndexOf(',');
                        pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                    }
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
                            string pomNaziv = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                            if (pomNaziv.Contains(","))
                            {
                                int z = pomNaziv.IndexOf(',');
                                pomNaziv = pomNaziv.Substring(0, z);
                            }
                            if (pogresniNazivi[j] == pomNaziv)
                            {
                                novi = false;
                                break;
                            }
                        }
                    } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi==false);

                    pogresniNazivi[i] = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                    if (pogresniNazivi[i].Contains(","))
                    {
                        int z = pogresniNazivi[i].IndexOf(',');
                        pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                    }

                }

               
            }

            //RASPORED ODGOVORA
            tacnoDugme = rand2.Next(1, 5);
            if (tacnoDugme == 1)
            {
                button1.Text = naziv;
                button2.Text = pogresniNazivi[0];
                button3.Text = pogresniNazivi[1];
                button4.Text = pogresniNazivi[2];
            }
            else if (tacnoDugme == 2)
            {
                button2.Text = naziv;
                button1.Text = pogresniNazivi[0];
                button3.Text = pogresniNazivi[1];
                button4.Text = pogresniNazivi[2];
            }
            else if (tacnoDugme == 3)
            {
                button3.Text = naziv;
                button1.Text = pogresniNazivi[0];
                button2.Text = pogresniNazivi[1];
                button4.Text = pogresniNazivi[2];
            }
            else if (tacnoDugme == 4)
            {
                button4.Text = naziv;
                button1.Text = pogresniNazivi[0];
                button3.Text = pogresniNazivi[1];
                button2.Text = pogresniNazivi[2];
            }


            timer1.Enabled = true;
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 1)
            {
                button2.BackColor = Color.Transparent;
                button3.BackColor = Color.Transparent;
                button4.BackColor = Color.Transparent;

                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;

                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                string imeSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                pictureBox1.Image = Image.FromFile(@"zastave\" + imeSlike);
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
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
                        pogresniNazivi[i] = tabelaDrzava.Rows[0]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }
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
                                string pomNaziv = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                                if (pomNaziv.Contains(","))
                                {
                                    int z = pomNaziv.IndexOf(',');
                                    pomNaziv = pomNaziv.Substring(0, z);
                                }
                                if (pogresniNazivi[j] == pomNaziv)
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNazivi[i] = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }

                    }


                }

                //RASPORED ODGOVORA
                tacnoDugme = rand2.Next(1, 5);
                if (tacnoDugme == 1)
                {
                    button1.Text = naziv;
                    button2.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 2)
                {
                    button2.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 3)
                {
                    button3.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button2.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 4)
                {
                    button4.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button2.Text = pogresniNazivi[2];
                }
            }
            else
            {
                button1.BackColor = Color.IndianRed;
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
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 2)
            {
                button1.BackColor = Color.Transparent;
                button3.BackColor = Color.Transparent;
                button4.BackColor = Color.Transparent;
                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;


                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                string imeSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                pictureBox1.Image = Image.FromFile(@"zastave\" + imeSlike);
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
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
                        pogresniNazivi[i] = tabelaDrzava.Rows[0]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }
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
                                string pomNaziv = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                                if (pomNaziv.Contains(","))
                                {
                                    int z = pomNaziv.IndexOf(',');
                                    pomNaziv = pomNaziv.Substring(0, z);
                                }
                                if (pogresniNazivi[j] == pomNaziv)
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNazivi[i] = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }

                    }


                }


                tacnoDugme = rand2.Next(1, 5);

                if (tacnoDugme == 1)
                {
                    button1.Text = naziv;
                    button2.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 2)
                {
                    button2.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 3)
                {
                    button3.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button2.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 4)
                {
                    button4.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button2.Text = pogresniNazivi[2];
                }
            }
            else
            {
                button2.BackColor = Color.IndianRed;
               
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
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 3)
            {
                button2.BackColor = Color.Transparent;
                button1.BackColor = Color.Transparent;
                button4.BackColor = Color.Transparent;
                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;


                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                string imeSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                pictureBox1.Image = Image.FromFile(@"zastave\" + imeSlike);
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
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
                        pogresniNazivi[i] = tabelaDrzava.Rows[0]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }
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
                                string pomNaziv = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                                if (pomNaziv.Contains(","))
                                {
                                    int z = pomNaziv.IndexOf(',');
                                    pomNaziv = pomNaziv.Substring(0, z);
                                }
                                if (pogresniNazivi[j] == pomNaziv)
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNazivi[i] = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }

                    }


                }


                tacnoDugme = rand2.Next(1, 5);

                if (tacnoDugme == 1)
                {
                    button1.Text = naziv;
                    button2.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 2)
                {
                    button2.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 3)
                {
                    button3.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button2.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 4)
                {
                    button4.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button2.Text = pogresniNazivi[2];
                }
            }
            else
            {
                button3.BackColor = Color.IndianRed;
                
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
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (tacnoDugme == 4)
            {
                button2.BackColor = Color.Transparent;
                button3.BackColor = Color.Transparent;
                button1.BackColor = Color.Transparent;
                brPogodaka++;
                labelRez.Text = "Rezultat: " + brPogodaka;


                //DOHVATANJE NOVE ZASTAVE I TACNOG ODGOVORA
                trenutni++;
                konekcija.Open();
                komanda = new OleDbCommand("SELECT * FROM Drzave WHERE ID=" + zadato[trenutni], konekcija);
                adapter = new OleDbDataAdapter(komanda);
                tabelaDrzava = new DataTable();
                adapter.Fill(tabelaDrzava);
                string imeSlike = tabelaDrzava.Rows[0]["Zastava"].ToString();
                pictureBox1.Image = Image.FromFile(@"zastave\" + imeSlike);
                naziv = tabelaDrzava.Rows[0]["Naziv"].ToString();
                if (naziv.Contains(","))
                {
                    int z = naziv.IndexOf(',');
                    naziv = naziv.Substring(0, z);
                }
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
                        pogresniNazivi[i] = tabelaDrzava.Rows[0]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }
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
                                string pomNaziv = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                                if (pomNaziv.Contains(","))
                                {
                                    int z = pomNaziv.IndexOf(',');
                                    pomNaziv = pomNaziv.Substring(0, z);
                                }
                                if (pogresniNazivi[j] == pomNaziv)
                                {
                                    novi = false;
                                    break;
                                }
                            }
                        } while (Int32.Parse(tabelaDrzaveNivo.Rows[pom]["ID"].ToString()) == zadato[trenutni] || novi == false);

                        pogresniNazivi[i] = tabelaDrzaveNivo.Rows[pom]["Naziv"].ToString();
                        if (pogresniNazivi[i].Contains(","))
                        {
                            int z = pogresniNazivi[i].IndexOf(',');
                            pogresniNazivi[i] = pogresniNazivi[i].Substring(0, z);
                        }

                    }


                }



                tacnoDugme = rand2.Next(1, 5);

                if (tacnoDugme == 1)
                {
                    button1.Text = naziv;
                    button2.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 2)
                {
                    button2.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 3)
                {
                    button3.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button2.Text = pogresniNazivi[1];
                    button4.Text = pogresniNazivi[2];
                }
                else if (tacnoDugme == 4)
                {
                    button4.Text = naziv;
                    button1.Text = pogresniNazivi[0];
                    button3.Text = pogresniNazivi[1];
                    button2.Text = pogresniNazivi[2];
                }
            }
            else
            {
               
                button4.BackColor = Color.IndianRed;

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
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            button1.Visible = button2.Visible = button3.Visible = button4.Visible = false;
            pictureBox1.Visible = false;
            labelVreme.Visible = false;
            labelRez.Visible = false;
            buttonStart.Visible = true;
            rbLako.Checked = rb30.Checked = true;
            groupBox1.Enabled = groupBox2.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            if(m>0 && s < 0)
            {
                m--;
                s = 59;
            }else if(m==0 && s <= 0)
            {
                labelVreme.Text = "00:00";
                timer1.Enabled = false;
                MessageBox.Show("Kraj igre!\nVas rezultat je: "+brPogodaka);
                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
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
