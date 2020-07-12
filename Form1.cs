using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace AdressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Osoba> osoby = new List<Osoba>();

        #region methods for objects
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //tworzenie folderu oraz pliku w wyznaczonym miejscu (w przypadku gdy nie istnieją)
            string path = "C:\\Users\\Kuba\\Desktop";
            if (!Directory.Exists(path + "\\Kontakty"))
            {
                Directory.CreateDirectory(path + "\\Kontakty");
            }

            
            if (File.Exists(path + "\\Kontakty\\ListaKontaktow.xml"))
            {
                XmlDocument xDoc1 = new XmlDocument();

                //Ładowanie zapisanych kontaktów z pliku xml
                xDoc1.Load(path + "\\Kontakty\\ListaKontaktow.xml");

                foreach (XmlNode xNode in xDoc1.SelectNodes("Osoby/Osoba"))
                {
                    Osoba o = new Osoba();
                    o.Imie = xNode.SelectSingleNode("Imie").InnerText;
                    o.Email = xNode.SelectSingleNode("Email").InnerText;
                    o.Ulica = xNode.SelectSingleNode("Ulica").InnerText;
                    o.Firma = xNode.SelectSingleNode("Firma").InnerText;
                    o.DataUrodzenia = DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("Dataurodzenia").InnerText));
                    o.DodatkoweInformacje = xNode.SelectSingleNode("DodatkoweInformacje").InnerText;
                    osoby.Add(o);
                    listView1.Items.Add(o.Imie + " " + o.Firma);

                }
            }
            else
            {
                
                    XmlTextWriter xW = new XmlTextWriter(path + "\\Kontakty\\ListaKontaktow.xml", Encoding.UTF8);
                    xW.WriteStartElement("Osoby");
                    xW.WriteEndElement();
                    xW.Close();
                
            }
            

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            Osoba o = new Osoba();
            o.Imie = textBox1.Text;
            o.Email = textBox3.Text;
            o.DataUrodzenia = dateTimePicker1.Value;
            o.Ulica = textBox2.Text;
            o.Firma = textBox4.Text;
            o.DodatkoweInformacje = textBox5.Text;
            osoby.Add(o);
            listView1.Items.Add(o.Imie +" "+  o.Firma);
            textBox1.Text ="";  //string.Empty;
            textBox2.Text ="";  //string.Empty;
            textBox3.Text ="";  //string.Empty;
            textBox4.Text ="";  //string.Empty;
            textBox5.Text = "";     //string.Empty; 
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Informacje_Enter(object sender, EventArgs e)
        {

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {

                textBox1.Text = osoby[listView1.SelectedItems[0].Index].Imie;
                textBox2.Text = osoby[listView1.SelectedItems[0].Index].Ulica;
                textBox3.Text = osoby[listView1.SelectedItems[0].Index].Email;
                textBox4.Text = osoby[listView1.SelectedItems[0].Index].Firma;
                textBox5.Text = osoby[listView1.SelectedItems[0].Index].DodatkoweInformacje;
                dateTimePicker1.Value = osoby[listView1.SelectedItems[0].Index].DataUrodzenia;

           }
           catch
           {

           }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Usun();
        }
        void Usun()
        {
            try
            {
                // listView1.Items.Remove(listView1.SelectedItems[0]);
                // osoby.RemoveAt(listView1.SelectedItems[0].Index);

                osoby.RemoveAt(listView1.SelectedItems[0].Index);

                listView1.Items.Remove(listView1.SelectedItems[0]);

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            catch 
            {
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            osoby[listView1.SelectedItems[0].Index].Imie = textBox1.Text;
            osoby[listView1.SelectedItems[0].Index].Ulica = textBox2.Text;
            osoby[listView1.SelectedItems[0].Index].Email = textBox3.Text;
            osoby[listView1.SelectedItems[0].Index].Firma = textBox4.Text;
            osoby[listView1.SelectedItems[0].Index].DodatkoweInformacje = textBox5.Text;
            osoby[listView1.SelectedItems[0].Index].DataUrodzenia = dateTimePicker1.Value;
            listView1.SelectedItems[0].Text = textBox1.Text + " " + textBox4.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Na pewno chcesz zakończyć", "Zakońćz", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {



                XmlDocument xDoc = new XmlDocument();
                string path = "C:\\Users\\Kuba\\Desktop";
                xDoc.Load(path + "\\Kontakty\\ListaKontaktow.xml");

                //pozbywanie sie istniejących osób w pliku w celu uniknięcia duplikatów
                XmlNode xNode = xDoc.SelectSingleNode("Osoby");
                xNode.RemoveAll();

                //wpisywanie istniejących osób do listy
                foreach (Osoba o in osoby)
                {
                    //tworzenie node'ów
                    XmlNode xTop = xDoc.CreateElement("Osoba");
                    XmlNode xImie = xDoc.CreateElement("Imie");
                    XmlNode xEmail = xDoc.CreateElement("Email");
                    XmlNode xUlica = xDoc.CreateElement("Ulica");
                    XmlNode xFirma = xDoc.CreateElement("Firma");
                    XmlNode xDodatkoweInformacje = xDoc.CreateElement("DodatkoweInformacje");
                    XmlNode xDataUrodzenia = xDoc.CreateElement("Dataurodzenia");

                    //wypełnianie node'ów
                    xImie.InnerText = o.Imie;
                    xEmail.InnerText = o.Email;
                    xUlica.InnerText = o.Ulica;
                    xFirma.InnerText = o.Firma;
                    xDodatkoweInformacje.InnerText = o.DodatkoweInformacje;
                    xDataUrodzenia.InnerText = o.DataUrodzenia.ToFileTime().ToString(); //kowertowanie daty do zmiennej liczbowej, a następnie do stringa

                    //składowanie wszystkich nodów w node osoba
                    xTop.AppendChild(xImie);
                    xTop.AppendChild(xEmail);
                    xTop.AppendChild(xUlica);
                    xTop.AppendChild(xFirma);
                    xTop.AppendChild(xDodatkoweInformacje);
                    xTop.AppendChild(xDataUrodzenia);

                    //składowanie noda osoba zawierającego poprzednie node'y w dokumencie osoby
                    xDoc.DocumentElement.AppendChild(xTop);
                }
                xDoc.Save(path + "\\Kontakty\\ListaKontaktow.xml");
            }
            else if (confirm == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }
    }

    class Osoba
    {
        public string Imie
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Ulica
        {
            get;
            set;
        }
        public string Firma
        {
            get;
            set;
        }
        public string DodatkoweInformacje
        {
            get;
            set;
        }

        public DateTime DataUrodzenia
        {
            get;
            set;
        }

    }

}
