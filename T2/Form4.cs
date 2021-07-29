using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T2
{
    public partial class Form4 : Form
    {
        //Form2 form2 = new Form2();

        public Form4()
        {
            InitializeComponent();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

        Form3 form3 = new Form3();
            form3.Show();  
        }

        private void guna2GradientCircleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();

           // form1.Show();
        }

        private void guna2PictureBox1_MouseHover(object sender, EventArgs e)
        {
            Lukoilbtn.BackColor = Color.White;
        }

        private void Lukoilbtn_MouseLeave(object sender, EventArgs e)
        {
            Lukoilbtn.BackColor = Color.Transparent;
        }

        private void Lukoilbtn_Click(object sender, EventArgs e)
        {
            Lukoil lukoil = new Lukoil();
            lukoil.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
