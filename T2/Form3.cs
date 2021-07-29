using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T2
{
    public partial class Form3 : Form
    {
        private Form4 form4 = new Form4();
        private List<User> Users { get; set; }
        private User _currentUser;
        private bool _isLoaded = false;


        public Form3()
        {
            InitializeComponent();
            //LoadUsersToListBox();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Users = new List<User>();

        }
        //private bool CheckUserInputs()
        //{
        //    if (!ValidateUserInputs())
        //    {
        //        MessageBox.Show("User can not be added. Please, fill inputs correct format!");
        //        return false;
        //    }

        //    return true;
        //}
        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaillbl_Click_1(object sender, EventArgs e)
        {

        }

      
        private bool ValidateMail(string mail)
        {
            return Regex.IsMatch(mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
     
       
        private void bunifuSavebtn_Click(object sender, EventArgs e)
        {
            var user = listBox1.SelectedItem as User;
            FileHelper.WriteUserToJson(bunifuFileNametxt.Text, user);
            MessageBox.Show("Saved");

        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            form4.Show();


        }
        private void LoadUsersToListBox()
        {
            listBox1.Items.Clear();
            listBox1.ValueMember = "FullName";
            foreach (var user in Users)
            {
                listBox1.Items.Add(user);
            }
        }

        private void SelectCurrentUser()
        {
            var userIndex = Users.IndexOf(_currentUser);
            listBox1.SelectedIndex = userIndex;
        }
        public bool ValidateUserInputs()
        {
            var status = true;
            if (string.IsNullOrWhiteSpace(this.bunifuFileNametxt.Text))
            {
                status = false;
            }

            if (string.IsNullOrWhiteSpace(this.bunifuSurnamelbl.Text))
            {
                status = false;
            }

            if (string.IsNullOrWhiteSpace(this.bunifuMail.Text))
            {
                status = false;
            }


            if (maskedTextBox1.Text != "(+   )   -" && !maskedTextBox1.MaskFull)
            {
                status = false;
            }
            return status;
        }



        private void LoadUserToForm(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            bunifuTextBox1.Text = user.FirstName;
            bunifuSurnamelbl.Text = user.LastName;
            bunifuMail.Text = user.Email;
            maskedTextBox1.Text = user.PhoneNumber;
            dateTimePicker1.Value = user.Birthdate;
        }
        private bool CheckUserFromList(User user)
        {
            return Users.Any(u => u.Guid == user.Guid);
        }

        private void bunifuAddbtn_Click(object sender, EventArgs e)
        {

            if (!_isLoaded)
            {
                _currentUser = new User();
            }
            else
            {
                _isLoaded = false;
                File.Delete(FileHelper.CreateFileName(_currentUser));
            }


            _currentUser.FirstName = bunifuTextBox1.Text;
            _currentUser.LastName = bunifuSurnamelbl.Text;
            _currentUser.FullName = $"{_currentUser.FirstName}{_currentUser.LastName}";
            _currentUser.Email = bunifuMail.Text;
            _currentUser.PhoneNumber = maskedTextBox1.Text;
            _currentUser.Birthdate =dateTimePicker1.Value;

            Users.Add(_currentUser);
            LoadUsersToListBox();
            SelectCurrentUser();
            bunifuAddbtn.Location = new Point(12, 479);
            bunifuClearbtn.Location = new Point(344, 479);

        }

        private void bunifuClearbtn_Click(object sender, EventArgs e)
        {
            _currentUser = null;
            ClearUserInputs();
            bunifuClearbtn.Location = new Point(12, 479);
            bunifuAddbtn.Location = new Point(344, 479);
        }


        private void ClearUserInputs()
        {
            bunifuTextBox1.Text = String.Empty;
            bunifuSurnamelbl.Text = String.Empty;
            bunifuMail.Text = String.Empty;
            maskedTextBox1.Text = String.Empty;
            dateTimePicker1.Text = String.Empty;
        }

        private void bunifuModifybtn_Click(object sender, EventArgs e)
        {
            _currentUser.FirstName = bunifuTextBox1.Text;
            _currentUser.LastName = bunifuSurnamelbl.Text;
            _currentUser.FullName = $"{_currentUser.FirstName}{_currentUser.LastName}";
            _currentUser.Email = bunifuMail.Text;
            _currentUser.PhoneNumber = maskedTextBox1.Text;
            _currentUser.Birthdate = dateTimePicker1.Value;

            LoadUsersToListBox();
            SelectCurrentUser();
        }
        private void FocusToTextBox(Control textBox)
        {
            textBox.Focus();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void bunifuLoadbtn_Click(object sender, EventArgs e)
        {
            var fileName = bunifuFileNametxt.Text;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("File name can not be blank!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FocusToTextBox(bunifuFileNametxt);
                return;
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show($"This file does not exist: {fileName}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FocusToTextBox(bunifuFileNametxt);
                return;
            }

            var user = FileHelper.ReadUserFromJson(fileName);

            if (CheckUserFromList(user))
            {
                MessageBox.Show($"User already in the list");
                return;
            }

            _currentUser = user;
            _isLoaded = true;

            Users.Add(_currentUser);
            LoadUsersToListBox();
            try
            {
                LoadUserToForm(_currentUser);
                SelectCurrentUser();
            }
            catch (Exception exception)
            {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentUser = listBox1.SelectedItem as User;

            try
            {
                LoadUserToForm(_currentUser);
                this.bunifuFileNametxt.Text = FileHelper.CreateFileName(_currentUser);
            }
            catch (Exception exception)
            {
            }
        }

        private void bunifuFileNametxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

