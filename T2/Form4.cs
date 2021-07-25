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
        Form2 form2 = new Form2();
        Form1 form1 = new Form1();

        public Form4()
        {
            InitializeComponent();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

            form2.Show();  
        }

        private void guna2GradientCircleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();

            form1.Show();
        }
    }
}
