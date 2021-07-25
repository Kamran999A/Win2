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

namespace T2
{
    public partial class Form2 : Form
    {
        public IList<Applier> Appliers { get;  set; }
        public bool Changes { get; set; } = false;
        public Form2()
        {
            InitializeComponent();
         
        }
       

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDataToDropBox();
        }



        private void LoadData()
        {
            FileHelper.FileName = "appliers.json";

            if (File.Exists(FileHelper.FileName))
            {
                Appliers = FileHelper.ReadFromJson();
            }
            else
            {
                Appliers = new List<Applier>();
            }
        }

        private Applier GetApplier(int index)
        {
            return Appliers[index];
        }
        private void LoadDataToDropBox()
        {
            guna2ComboBox1.Items.Clear();

            foreach (var applier in Appliers)
            {
                guna2ComboBox1.Items.Add(applier.GetFullname());
            }
        }
        public new void Show()
        {
            if (Changes)
            {
                LoadDataToDropBox();
                Changes = false;
            }

            base.Show();
        }

        private void bunifuThinLoadbtn_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.Items.Count == 0)
            {
                MessageBox.Show("Applier list empty. You must add new applier!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(guna2ComboBox1.Text))
            {
                MessageBox.Show("You must be select applier!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var applier = GetApplier(guna2ComboBox1.SelectedIndex);

            var form3 = new Form3(this, applier);

            this.Hide();

            form3.Show();
        }

        private void bunifuThinCreatebtn_Click(object sender, EventArgs e)
        {
            this.Hide();

            var form3 = new Form3(this);

            form3.Show();
        }
    }
}
