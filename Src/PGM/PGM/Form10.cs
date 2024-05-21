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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public void SetLabel1Text(string text)
        {
            this.label1.Text = text;
        }
        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.form10visible = false;
            this.Hide();
            e.Cancel = true;
        }
    }
}
