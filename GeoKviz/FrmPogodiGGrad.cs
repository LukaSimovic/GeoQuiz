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
    public partial class FrmPogodiGGrad : Form
    {

        OleDbConnection konekcija;
        OleDbCommand komanda;
        OleDbDataAdapter adapter;
        DataTable tabelaDrzave;

        bool zadatNaziv; //naziv-true, zastava-false
        List<int> kontinentIDs = new List<int>();
        string kontinentiStr;

        int sec, min, hours;
        int trenutni;
        string s, m, h;

        string podatakZaPrikaz;
        string nazivGGrada;
        List<string> naziviGGrada = new List<string>();

        int[] zadato = new int[195];
        bool[] pogodjeno = new bool[195];
        int brPogodaka;


        public FrmPogodiGGrad()
        {
            InitializeComponent();
            /*
             string putanja = Environment.CurrentDirectory;
             string[] putanjaBaze = putanja.Split(new string[] { "bin" }, StringSplitOptions.None);
             AppDomain.CurrentDomain.SetData("DataDirectory", putanjaBaze[0]);
             */
            konekcija = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\GeoQuizBaza.accdb");
        }

        private void FrmPogodiGGrad_Load(object sender, EventArgs e)
        {
            panelIgra.Visible = false;
            panelPodesavanja.Visible = true;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            panelIgra.Visible = true;
            panelPodesavanja.Visible = false;

            //PROVERA MODA IGRE
            if (rbNaziv.Checked)
            {
                zadatNaziv = true;
                pictureBoxZastava.Visible = false;
                textBox1.Visible = true;
                labelNazivDrzave.Visible = true;
            }
            else
            {
                zadatNaziv = false;
                pictureBoxZastava.Visible = true;
                textBox1.Visible = false;
                labelNazivDrzave.Visible = false;
            }

            //SASTAVLJANJE STRINGA ZA ODABRANE KONTINETE
            kontinentIDs.Clear();
            if (cbEvropa.Checked)
            {
                kontinentIDs.Add(1);
                kontinentIDs.Add(13);
            }
            if (cbAfrika.Checked)
            {
                kontinentIDs.Add(2);
            }
            if (cbAzija.Checked)
            {
                kontinentIDs.Add(3);
                if (!cbEvropa.Checked)
                {
                    kontinentIDs.Add(13);
                }
            }
            if (cbJA.Checked)
            {
                kontinentIDs.Add(4);
            }
            if (cbSA.Checked)
            {
                kontinentIDs.Add(5);
            }
            if (cbAO.Checked)
            {
                kontinentIDs.Add(6);
            }
            kontinentiStr = "(";
            for(int i=0; i<kontinentIDs.Count; i++)
            {
                kontinentiStr += kontinentIDs[i].ToString();
                if (i == kontinentIDs.Count - 1)
                {
                    kontinentiStr += ")";
                }
                else
                {
                    kontinentiStr += ",";
                }
            }
            string upit = "SELECT * FROM Drzave WHERE KontinentiID IN " + kontinentiStr;

            //POPUNJAVANJE TABELE   
            konekcija.Open();
            komanda = new OleDbCommand(upit, konekcija);
            adapter = new OleDbDataAdapter(komanda);
            tabelaDrzave = new DataTable();
            adapter.Fill(tabelaDrzave);
            konekcija.Close();

           

            //PRAVLJENJE REDOSLEDA
            int rbr;
            Random rand = new Random();
            for (int i = 0; i < tabelaDrzave.Rows.Count; i++)
            {
                bool novi;
                do
                {
                    rbr = rand.Next(0, tabelaDrzave.Rows.Count);
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
                pogodjeno[i] = false;

            }

            //DOHVATANJE PRVE DRZAVE
            trenutni = 0;
            if (zadatNaziv==true)
            {
                podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Naziv"].ToString();
                if (podatakZaPrikaz.Contains(","))
                {
                    int z = podatakZaPrikaz.IndexOf(',');
                    podatakZaPrikaz = podatakZaPrikaz.Substring(0, z);
                }
                textBox1.Text = podatakZaPrikaz;
            }
            else
            {
                podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Zastava"].ToString();
                pictureBoxZastava.Image = Image.FromFile(@"zastave\" + podatakZaPrikaz);
            }
            nazivGGrada = tabelaDrzave.Rows[zadato[trenutni]]["Glavni grad"].ToString();
            naziviGGrada.Clear();
            while (nazivGGrada.Contains(","))
            {
                int z = nazivGGrada.IndexOf(',');
                naziviGGrada.Add(nazivGGrada.Substring(0, z));
                nazivGGrada = nazivGGrada.Substring(z + 1);
            }
            naziviGGrada.Add(nazivGGrada);

            //DODATNA PODESAVANJA
            labelVreme.Text += " 00:00:00";
            labelSkor.Text += " 0/" + tabelaDrzave.Rows.Count.ToString();
            sec = 0;
            min = 0;
            hours = 0;
            brPogodaka = 0;
            timer1.Enabled = true;
            textBoxGrad.Focus();
        }

        private void textBoxGrad_TextChanged(object sender, EventArgs e)
        {
            bool pogodak = false;
            for (int i = 0; i < naziviGGrada.Count; i++)
            {
                if (textBoxGrad.Text.Equals(naziviGGrada[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    pogodak = true;
                    break;
                }
            }

            if (pogodak)
            {
                textBoxGrad.Text = "";
                brPogodaka++;
                labelSkor.Text = "Pogodjeno: " + brPogodaka + "/" + tabelaDrzave.Rows.Count.ToString();
                pogodjeno[trenutni] = true;

                if (brPogodaka == tabelaDrzave.Rows.Count)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Cestitamo!\nVase vreme je: " + h + ":" + m + ":" + s);
                }
                else
                {
                    do
                    {
                        trenutni = (trenutni + 1) % tabelaDrzave.Rows.Count;
                    } while (pogodjeno[trenutni] == true);

                    //DOHVATANJE SLEDECE DRZAVE
                    if (zadatNaziv == true)
                    {
                        podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Naziv"].ToString();
                        if (podatakZaPrikaz.Contains(","))
                        {
                            int z = podatakZaPrikaz.IndexOf(',');
                            podatakZaPrikaz = podatakZaPrikaz.Substring(0, z);
                        }
                        textBox1.Text = podatakZaPrikaz;
                    }
                    else
                    {
                        podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Zastava"].ToString();
                        pictureBoxZastava.Image = Image.FromFile(@"zastave\" + podatakZaPrikaz);
                    }
                    nazivGGrada = tabelaDrzave.Rows[zadato[trenutni]]["Glavni grad"].ToString();
                    naziviGGrada.Clear();
                    while (nazivGGrada.Contains(","))
                    {
                        int z = nazivGGrada.IndexOf(',');
                        naziviGGrada.Add(nazivGGrada.Substring(0, z));
                        nazivGGrada = nazivGGrada.Substring(z + 1);
                    }
                    naziviGGrada.Add(nazivGGrada);
                }
            }
        }

        private void buttonSled_Click(object sender, EventArgs e)
        {
            if (brPogodaka != tabelaDrzave.Rows.Count)
            {
                textBoxGrad.Text = "";

                do
                {
                    trenutni = (trenutni + 1) % tabelaDrzave.Rows.Count;
                } while (pogodjeno[trenutni] == true);


                //DOHVATANJE SLEDECE DRZAVE
                if (zadatNaziv == true)
                {
                    podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Naziv"].ToString();
                    if (podatakZaPrikaz.Contains(","))
                    {
                        int z = podatakZaPrikaz.IndexOf(',');
                        podatakZaPrikaz = podatakZaPrikaz.Substring(0, z);
                    }
                    textBox1.Text = podatakZaPrikaz;
                }
                else
                {
                    podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Zastava"].ToString();
                    pictureBoxZastava.Image = Image.FromFile(@"zastave\" + podatakZaPrikaz);
                }
                nazivGGrada = tabelaDrzave.Rows[zadato[trenutni]]["Glavni grad"].ToString();
                naziviGGrada.Clear();
                while (nazivGGrada.Contains(","))
                {
                    int z = nazivGGrada.IndexOf(',');
                    naziviGGrada.Add(nazivGGrada.Substring(0, z));
                    nazivGGrada = nazivGGrada.Substring(z + 1);
                }
                naziviGGrada.Add(nazivGGrada);

                textBoxGrad.Focus();
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (brPogodaka != tabelaDrzave.Rows.Count)
            {
                textBoxGrad.Text = "";

                do
                {
                    trenutni--;
                    if (trenutni < 0)
                        trenutni += tabelaDrzave.Rows.Count;
                } while (pogodjeno[trenutni] == true);


                //DOHVATANJE SLEDECE DRZAVE
                if (zadatNaziv == true)
                {
                    podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Naziv"].ToString();
                    if (podatakZaPrikaz.Contains(","))
                    {
                        int z = podatakZaPrikaz.IndexOf(',');
                        podatakZaPrikaz = podatakZaPrikaz.Substring(0, z);
                    }
                    textBox1.Text = podatakZaPrikaz;
                }
                else
                {
                    podatakZaPrikaz = tabelaDrzave.Rows[zadato[trenutni]]["Zastava"].ToString();
                    pictureBoxZastava.Image = Image.FromFile(@"zastave\" + podatakZaPrikaz);
                }
                nazivGGrada = tabelaDrzave.Rows[zadato[trenutni]]["Glavni grad"].ToString();
                naziviGGrada.Clear();
                while (nazivGGrada.Contains(","))
                {
                    int z = nazivGGrada.IndexOf(',');
                    naziviGGrada.Add(nazivGGrada.Substring(0, z));
                    nazivGGrada = nazivGGrada.Substring(z + 1);
                }
                naziviGGrada.Add(nazivGGrada);

                textBoxGrad.Focus();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //RESET
            timer1.Enabled = false;
            panelIgra.Visible = false;
            panelPodesavanja.Visible = true;

            labelVreme.Text = "Vreme:";
            labelSkor.Text = "Pogodjeno:";

            brPogodaka = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //IZLAZ
            timer1.Enabled = false;
            this.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec % 60 == 0)
            {
                min++;
                sec = 0;
                if (min % 60 == 0)
                {
                    hours++;
                    min = 0;
                }
            }

            if (sec < 10)
            {
                s = "0" + sec;
            }
            else
            {
                s = sec.ToString();
            }

            if (min < 10)
            {
                m = "0" + min;
            }
            else
            {
                m = min.ToString();
            }

            if (hours < 10)
            {
                h = "0" + hours;
            }
            else
            {
                h = hours.ToString();
            }

            labelVreme.Text = "Vreme: " + h + ":" + m + ":" + s;
        }

        private void panelSkor_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
