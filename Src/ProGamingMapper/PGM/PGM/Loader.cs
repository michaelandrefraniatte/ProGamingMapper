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

namespace PGM
{
    public partial class Loader : Form
    {
        private static string[] tempargs;
        public Loader(string[] args)
        {
            InitializeComponent();
            tempargs = args;
        }
        private void Loader_Load(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.Image = PGM.Properties.Resources.loader;
        }
        private void Loader_Shown(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(tempargs.Length > 0 ? tempargs[0] : null);
            form1.Show();
        }
        public void HideLoader()
        {
            Application.OpenForms["Loader"].Hide();
        }
        public void CloseLoader()
        {
            Application.OpenForms["Loader"].Close();
        }
    }
}