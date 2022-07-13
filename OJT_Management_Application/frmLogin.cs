using Library.Models;
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
using Library.Repositories.StudentRepo;
using Newtonsoft.Json;
using OJT_Application;

namespace Application
{
    public partial class frmLogin : Form
    {
        private IStudentRepo studentRepo;
        private Student student;
        private Admin admin;
        public frmLogin()
        {
            InitializeComponent();
            studentRepo = new StudentRepo();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var Email = txtEmail.Text;
            var Password = txtPassword.Text;
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter your email and password");
            }
            else
            {
                if (Email.Equals(admin.AdminEmail) && Password.Equals(admin.AdminPassword))
                {
                    MessageBox.Show("Login Successful");
                    frmAdmin adminForm = new frmAdmin();
                    adminForm.Show();
                    this.Hide();
                }
                else
                {
                    student = studentRepo.GetStudentByEmailAndPassword(Email, Password);
                    if (student != null)
                    {
                        MessageBox.Show("Login Successful");
                        frmStudent studentForm = new frmStudent();
                        studentForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Wrong email or password!");
                    }
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
