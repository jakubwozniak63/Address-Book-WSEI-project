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
            string path = "C:\\Users\\Kuba\\Desktop";
            if (!Directory.Exists(path + "\\AddressBook123"))
            {
                Directory.CreateDirectory(path + "\\AddressBook123");
            }
            if (!File.Exists(path + "\\AddressBook123\\contacts.xml"))
            {
                File.Create(path + "\\AddressBook123\\contacts.xml");
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
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Informacje_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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
                listView1.Items.Remove(listView1.SelectedItems[0]);
                osoby.RemoveAt(listView1.SelectedItems[0].Index);

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
            XmlDocument xDoc = new XmlDocument();
            string path = "C:\\Users\\Kuba\\Desktop";
            xDoc.Load(path + "\\AddressBook123\\contacts.xml");
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
