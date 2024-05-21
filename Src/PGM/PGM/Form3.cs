using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGM
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public void SetLabel1Text(string text)
        {
            this.label1.Text = text;
        }
        public void SetLabel2Text(string text)
        {
            this.label2.Text = text;
        }
        public void SetLabel3Text(string text)
        {
            this.label3.Text = text;
        }
        public void SetLabel4Text(string text)
        {
            this.label4.Text = text;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.form3visible = false;
            this.Hide();
            e.Cancel = true;
        }
    }
}
