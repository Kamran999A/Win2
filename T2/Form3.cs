using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T2
{
    public partial class Form3 : Form
    {

        public Applier _applier;


        private Form2 _form;


        public Form3()
        {
            InitializeComponent();
        }

          public Form3(Form2 form) : this()
        {
            _form = form;
        }
        

        public Form3(Form2 form, Applier applier) : this(form)
        {
            _applier = applier;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
           
              if (_applier != null)
              {
                bunifuTextBox1.Text = _applier.FirstName;
                
                bunifuSurnamelbl.Text = _applier.LastName;

                bunifuMail.Text = _applier.Email;

                maskedTextBox1.Text = _applier.PhoneNumber;

                if (_applier.Gender == bunifuMaleRbtn.Text)
                {
                    bunifuMaleRbtn.Checked = true;
                }
                else if (_applier.Gender == bunifuFemaleRbtn.Text)
                {
                    bunifuFemaleRbtn.Checked = true;
                }
                if (_applier.Languages.Contains(bunifuCheckBoxEnglish.Text))
                    bunifuCheckBoxEnglish.Checked = true;

                if (_applier.Languages.Contains(bunifuCheckBoxRussian.Text))
                    bunifuCheckBoxRussian.Checked = true;

                if (_applier.Languages.Contains(bunifuCheckBoxJewish.Text))
                    bunifuCheckBoxJewish.Checked = true;

                if (_applier.Languages.Contains(bunifuCheckBoxGerman.Text))
                    bunifuCheckBoxGerman.Checked = true;
              }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaillbl_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaillbl_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuLanguagelbl_Click(object sender, EventArgs e)
        {

        }

        private void gunaEnglish_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateMail(string mail)
        {
            return Regex.IsMatch(mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        private string Capitalize(string name)
        {
            return $"{char.ToUpper(name[0])}{name.Substring(1)}";
        }
       
        private void bunifuSavebtn_Click(object sender, EventArgs e)
        {
            if (ValidateMail(bunifuMail.Text))
            {

                var firstName = Capitalize(bunifuTextBox1.Text);
                var lastName = Capitalize(bunifuSurnamelbl.Text);

                Applier applier = new Applier();

                applier.FirstName = firstName;
                applier.LastName = lastName;
                applier.Gender = (bunifuMaleRbtn.Checked) ? $"{bunifuMaleRbtn.Text}" : $"{bunifuFemaleRbtn.Text}";

                applier.Email = bunifuMail.Text;
                applier.PhoneNumber = maskedTextBox1.Text;
                if (bunifuCheckBoxEnglish.Checked)
                {
                    applier.Languages.Add(bunifuCheckBoxEnglish.Text);
                }

                if (bunifuCheckBoxRussian.Checked)
                {
                    applier.Languages.Add(bunifuCheckBoxRussian.Text);
                }

                if (bunifuCheckBoxGerman.Checked)
                {
                    applier.Languages.Add(bunifuCheckBoxGerman.Text);
                }

                if (bunifuCheckBoxJewish.Checked)
                {
                    applier.Languages.Add(bunifuCheckBoxJewish.Text);
                }

                if (_applier == null)
                {
                    _form.Appliers.Add(applier);
                    _applier = applier;
                }
                else
                {
                    _applier.FirstName = applier.FirstName;
                    _applier.LastName = applier.LastName;
                    _applier.Gender = applier.Gender;
                    _applier.Email = applier.Email;
                    _applier.PhoneNumber = applier.PhoneNumber;
                    _applier.Languages = applier.Languages;
                }
                _form.Changes = true;

                FileHelper.WriteToJson(_form.Appliers);
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("You must be fill all required inputs!");
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();

            _form.Show();
        }

        private void bunifuBackPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();

            _form.Show();
        }
    }
}
