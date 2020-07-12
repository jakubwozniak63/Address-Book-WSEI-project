using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AdressBook
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        List<firma> firmy = new List<firma>();

        private void Form2_Load(object sender, EventArgs e)
        {
           
            string path = "C:\\Users\\Kuba\\Desktop";
            if (!Directory.Exists(path + "\\firmy"))
            {
                Directory.CreateDirectory(path + "\\firmy");
            }


            if (File.Exists(path + "\\firmy\\ListaFirm.xml"))
            {
                XmlDocument xDoc1 = new XmlDocument();

               
                xDoc1.Load(path + "\\firmy\\ListaFirm.xml");

                   foreach (XmlNode xNode in xDoc1.SelectNodes("Osoby/Osoba"))
                   {
                       firma f = new firma();
                       f.NazwaFirmy = xNode.SelectSingleNode("NazwaFirmy").InnerText;
                       f.NazwaOrganuReprezentacji = xNode.SelectSingleNode("NazwaOrganu").InnerText;
                       f.DataRozpoczeciaDzialalosci = DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("DataRozpoczeciaDzialanosci").InnerText));
                       f.NIP = xNode.SelectSingleNode("Nip").InnerText;
                       f.Regon = xNode.SelectSingleNode("Regon").InnerText;
                       f.Strona = xNode.SelectSingleNode("StronaWWW").InnerText;
                       f.Emailfirmy = xNode.SelectSingleNode("Email").InnerText;
                       f.AdresSiedziby = xNode.SelectSingleNode("Adres").InnerText;
                       f.IloscPracownikow = (Convert.ToInt64(xNode.SelectSingleNode("IloscPracownikow").InnerText));

                       firmy.Add(f);
                       listView1.Items.Add(f.NazwaFirmy + " " + f.NIP);

                   }
               }
               else
               {

                   XmlTextWriter xW = new XmlTextWriter(path + "\\firmy\\ListaFirm.xml", Encoding.UTF8);
                   xW.WriteStartElement("Firmy");
                   xW.WriteEndElement();
                   xW.Close();

               } 
                
            
        }
        
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        class firma
        {
            public string NazwaFirmy
            {
                get;
                set;
            }
            public string NazwaOrganuReprezentacji
            {
                get;
                set;
            }
            public DateTime DataRozpoczeciaDzialalosci
            {
                get;
                set;
            }
            public string NIP
            {
                get;
                set;
            }
            public string Regon
            {
                get;
                set;
            }
            public string Strona
            {
                get;
                set;
            }
            public string Emailfirmy
            {
                get;
                set;
            }
            public string AdresSiedziby
            {
                get;
                set;
            }
            public decimal IloscPracownikow
            {
                get;
                set;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Na pewno chcesz zakończyć", "Zakończ", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {



                XmlDocument xDoc = new XmlDocument();
                string path = "C:\\Users\\Kuba\\Desktop";
                xDoc.Load(path + "\\firmy\\ListaFirm.xml");

                
                XmlNode xNode = xDoc.SelectSingleNode("Firmy");
                xNode.RemoveAll();

                
                foreach (firma f in firmy)
                {
                    
                    XmlNode xTop = xDoc.CreateElement("Firma");
                    XmlNode xNazwaFirmy = xDoc.CreateElement("NazwaFirmy");
                    XmlNode xNazwaOrganu = xDoc.CreateElement("NazwaOrganu");
                    XmlNode xDataRozpoczeciaDzialasnosci = xDoc.CreateElement("DataRozpoczeciaDzialalnosci");
                    XmlNode xNip = xDoc.CreateElement("NIP");
                    XmlNode xRegon = xDoc.CreateElement("Regon");
                    XmlNode xStronaWWW = xDoc.CreateElement("StronaWWW");
                    XmlNode xEmail = xDoc.CreateElement("Email");
                    XmlNode xAdres = xDoc.CreateElement("Adres");
                    XmlNode xIloscPracownikow = xDoc.CreateElement("IloscPracownikow");

                   
                    xNazwaFirmy.InnerText = f.NazwaFirmy;
                    xNazwaOrganu.InnerText = f.NazwaOrganuReprezentacji;
                    xDataRozpoczeciaDzialasnosci.Value = f.DataRozpoczeciaDzialalosci.ToFileTime().ToString();
                    xNip.InnerText = f.NIP;
                    xRegon.InnerText = f.Regon;
                    xStronaWWW.InnerText = f.Strona;
                    xEmail.InnerText = f.Emailfirmy;
                    xAdres.InnerText = f.AdresSiedziby;
                    xIloscPracownikow.Value = f.IloscPracownikow.ToString();


                    xTop.AppendChild(xNazwaFirmy);
                    xTop.AppendChild(xNazwaOrganu);
                    xTop.AppendChild(xDataRozpoczeciaDzialasnosci);
                    xTop.AppendChild(xNip);
                    xTop.AppendChild(xRegon);
                    xTop.AppendChild(xStronaWWW);
                    xTop.AppendChild(xEmail);
                    xTop.AppendChild(xAdres);
                    xTop.AppendChild(xIloscPracownikow);

                    xDoc.DocumentElement.AppendChild(xTop);
                }
                xDoc.Save(path + "\\firmy\\ListaFirm.xml");
            }
            else if (confirm == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            firma f = new firma();
            f.NazwaFirmy = textBox1.Text;
            f.NazwaOrganuReprezentacji = textBox2.Text;
            f.DataRozpoczeciaDzialalosci = dateTimePicker11.Value;
            f.NIP = textBox3.Text;
            f.Regon = textBox4.Text;
            f.Strona = textBox5.Text;
            f.Emailfirmy = textBox7.Text;
            f.AdresSiedziby = textBox6.Text;
            f.IloscPracownikow = numericUpDown1.Value;
            firmy.Add(f);
            listView1.Items.Add(f.NazwaFirmy + " " + f.NIP);
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            numericUpDown1.Value = 0;
            dateTimePicker11.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            firmy[listView1.SelectedItems[0].Index].NazwaFirmy = textBox1.Text;
            firmy[listView1.SelectedItems[0].Index].NazwaOrganuReprezentacji = textBox2.Text;
            firmy[listView1.SelectedItems[0].Index].DataRozpoczeciaDzialalosci = dateTimePicker11.Value;
            firmy[listView1.SelectedItems[0].Index].NIP = textBox3.Text;
            firmy[listView1.SelectedItems[0].Index].Regon = textBox4.Text;
            firmy[listView1.SelectedItems[0].Index].Strona = textBox5.Text;
            firmy[listView1.SelectedItems[0].Index].Emailfirmy = textBox7.Text;
            firmy[listView1.SelectedItems[0].Index].AdresSiedziby = textBox6.Text;
            firmy[listView1.SelectedItems[0].Index].IloscPracownikow = numericUpDown1.Value;
            listView1.SelectedItems[0].Text = textBox1.Text + " " + textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Usun();
        }

            void Usun()
            {
                try
                {                  
                    firmy.RemoveAt(listView1.SelectedItems[0].Index);

                    listView1.Items.Remove(listView1.SelectedItems[0]);

                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                    textBox6.Text = string.Empty;
                    textBox7.Text = string.Empty;
                    numericUpDown1.Value = 0;
                    dateTimePicker11.Value = DateTime.Now;
                }
                catch
                {

                }          
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                textBox1.Text = firmy[listView1.SelectedItems[0].Index].NazwaFirmy;
                textBox2.Text = firmy[listView1.SelectedItems[0].Index].NazwaOrganuReprezentacji;
                textBox3.Text = firmy[listView1.SelectedItems[0].Index].NIP;
                textBox4.Text = firmy[listView1.SelectedItems[0].Index].Regon;
                textBox5.Text = firmy[listView1.SelectedItems[0].Index].Strona;
                textBox6.Text = firmy[listView1.SelectedItems[0].Index].AdresSiedziby;
                textBox7.Text = firmy[listView1.SelectedItems[0].Index].Emailfirmy;
                numericUpDown1.Value = firmy[listView1.SelectedItems[0].Index].IloscPracownikow;
                dateTimePicker11.Value = firmy[listView1.SelectedItems[0].Index].DataRozpoczeciaDzialalosci;

            }
            catch
            {

            }
        }
    }
}
