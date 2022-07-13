using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Models;

namespace OJT_Management_WinApp
{
    public partial class frmAdmin : Form
    {
        public static Admin admin;

        public frmAdmin()
        {
            InitializeComponent();

        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            txtAdminID.Text = admin.AdminId.ToString();
            txtAdminEmail.Text = admin.AdminEmail.ToString();
            txtAdminPassword.Text = admin.AdminPassword.ToString();
            //Load all admins from database and display them in the listbox
            using (var context = new OJT_ManagementContext())
            {
                var admins = context.Admins.ToList();
                dgvAdmin.DataSource = admins;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAdminID.Text) || string.IsNullOrWhiteSpace(txtAdminEmail.Text) || string.IsNullOrWhiteSpace(txtAdminPassword.Text))
                {
                    MessageBox.Show("Please fill all textbox");
                }
                else
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        //Check if admin Id is already exist
                        var checkAdminId = context.Admins.Where(x => x.AdminId == Int32.Parse(txtAdminID.Text)).FirstOrDefault();
                        if (checkAdminId != null)
                        {
                            MessageBox.Show("Admin Id is already exist");
                        }
                        else
                        {
                            //Check if admin email is already exist
                            var checkAdminEmail = context.Admins.Where(x => x.AdminEmail == txtAdminEmail.Text).FirstOrDefault();
                            if (checkAdminEmail != null)
                            {
                                MessageBox.Show("Admin email is already exist");
                            }
                            else
                            {
                                //Insert new admin to database
                                var newAdmin = new Admin
                                {
                                    AdminId = Int32.Parse(txtAdminID.Text),
                                    AdminEmail = txtAdminEmail.Text,
                                    AdminPassword = txtAdminPassword.Text
                                };
                                context.Admins.Add(newAdmin);
                                context.SaveChanges();
                                MessageBox.Show("Add new admin successfully");
                            }
                        }
                    }
                    MessageBox.Show("Add new admin successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAdminID.Text) || string.IsNullOrWhiteSpace(txtAdminEmail.Text) || string.IsNullOrWhiteSpace(txtAdminPassword.Text))
                {
                    MessageBox.Show("Please fill all textbox");
                }
                else
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        //Check if admin Id is already exist
                        var checkAdminId = context.Admins.Where(x => x.AdminId == Int32.Parse(txtAdminID.Text)).FirstOrDefault();
                        if (checkAdminId != null)
                        {
                            MessageBox.Show("Admin Id is already exist");
                        }
                        else
                        {
                            //Check if admin email is already exist
                            var checkAdminEmail = context.Admins.Where(x => x.AdminEmail == txtAdminEmail.Text).FirstOrDefault();
                            if (checkAdminEmail != null)
                            {
                                MessageBox.Show("Admin email is already exist");
                            }
                            else
                            {
                                //Update admin to database
                                var updateAdmin = context.Admins.Where(x => x.AdminId == Int32.Parse(txtAdminID.Text)).FirstOrDefault();
                                updateAdmin.AdminEmail = txtAdminEmail.Text;
                                updateAdmin.AdminPassword = txtAdminPassword.Text;
                                context.SaveChanges();
                                MessageBox.Show("Update admin successfully");
                            }
                        }
                    }
                    MessageBox.Show("Update admin successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAdminID.Text))
                {
                    MessageBox.Show("Please enter admin Id");
                }
                else
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        //Check if admin Id is already exist
                        var checkAdminId = context.Admins.Where(x => x.AdminId == Int32.Parse(txtAdminID.Text)).FirstOrDefault();
                        if (checkAdminId != null)
                        {
                            //Delete admin from database
                            var deleteAdmin = context.Admins.Where(x => x.AdminId == Int32.Parse(txtAdminID.Text)).FirstOrDefault();
                            context.Admins.Remove(deleteAdmin);
                            context.SaveChanges();
                            MessageBox.Show("Delete admin successfully");
                        }
                        else
                        {
                            MessageBox.Show("Admin Id is not exist");
                        }
                    }
                    MessageBox.Show("Delete admin successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Logout admin and return to login form
            this.Hide();
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (cbOption.SelectedItem == "Student_Management")
            {
                //Switch to student management form
                this.Hide();
                frmStudent frmStudent = new frmStudent();
                frmStudent.Show();
            }
            else if (cbOption.SelectedItem == "Company_Management")
            {
                //Switch to company management form
                this.Hide();
                frmCompany frmCompany = new frmCompany();
                frmCompany.Show();
            }
            else if (cbOption.SelectedItem == "Job_Management")
            {
                //Switch to admin management form
                this.Hide();
                frmJob frmJob = new frmJob();
                frmJob.Show();
            }
        }

        private void lbClearText_Click(object sender, EventArgs e)
        {
            //Reset text of textboxs
            txtAdminID.ResetText();
            txtAdminEmail.ResetText();
            txtAdminPassword.ResetText();
        }

        private void dgvAdmin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtAdminID.Text = dgvAdmin.Rows[rowIndex].Cells[0].Value.ToString();
            txtAdminEmail.Text = dgvAdmin.Rows[rowIndex].Cells[1].Value.ToString();
            txtAdminPassword.Text = dgvAdmin.Rows[rowIndex].Cells[2].Value.ToString();
        }
    }
}
