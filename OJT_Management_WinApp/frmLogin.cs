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
using Library.DAOs;
using Library.Models;
using Library.Repositories.StudentRepo;
using Newtonsoft.Json;

namespace OJT_Management_WinApp
{
    public partial class frmLogin : Form
    {
        public IStudentRepo studentRepo;
        public static Student student;
        public static Admin loginAdmin;

        public frmLogin()
        {
            InitializeComponent();
            studentRepo = new StudentRepo();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                var context = new OJT_ManagementContext();
                IEnumerable<Admin> aList = context.Admins.ToList();
                //Check if there is any admin in database
                if (aList.Count() == 0)
                {
                    //If there is no admin in database
                    MessageBox.Show("There is no valid account in database. Please use another account or create one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var Email = txtEmail.Text;
            var Password = txtPassword.Text;
            var context = new OJT_ManagementContext();
            IEnumerable<Admin> aList = context.Admins.ToList();
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || cbOption.SelectedItem == null)
            {
                MessageBox.Show("Please enter your email and password and select your role");
            }
            else
            {
                if (cbOption.SelectedItem == "Admin")
                {
                    foreach (var item in aList)
                    {
                        loginAdmin = item;
                        if (Email.Equals(item.AdminEmail) && Password.Equals(item.AdminPassword))
                        {
                            MessageBox.Show("Login Successful");
                            frmAdmin adminForm = new frmAdmin();
                            frmAdmin.admin = loginAdmin;
                            adminForm.Show();
                            this.Hide();
                        }
                    }
                }
                else if (cbOption.SelectedItem == "Student")
                {
                    student = studentRepo.GetStudentByEmailAndPassword(Email, Password);
                    if (student != null)
                    {
                        MessageBox.Show("Login Successful");
                        frmStudent studentForm = new frmStudent();
                        studentForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed! Wrong email or password!");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmail.ResetText();
            txtPassword.ResetText();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
