using Library.DAOs;
using Library.Models;
using Library.Repositories.StudentRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJT_Management_WinApp
{
    public partial class frmStudent : Form
    {
        public static Student userStudent;
        public static Admin userAdmin;
        public IStudentRepo studentRepo;

        public frmStudent()
        {
            InitializeComponent();
            studentRepo = new StudentRepo();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {

            //Check userAdmin and userStudent
            if (userAdmin != null)
            {
                using (var context = new OJT_ManagementContext())
                {
                    var students = context.Students.ToList();
                    dgvStudent.DataSource = students;
                }
            }
            else if (userStudent != null)
            {
                //Load data of student
                txtStudentID.Text = userStudent.StudentId.ToString();
                txtStudentName.Text = userStudent.StudentName.ToString();
                txtStudentEmail.Text = userStudent.StudentEmail.ToString();
                txtStudentPassword.Text = userStudent.StudentPassword.ToString();
                dtpDoB.Value = (DateTime)userStudent.DoB;
                if (userStudent.Gender == 1)
                {
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                }
                else if (userStudent.Gender == 0)
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = true;
                }
                txtPhone.Text = userStudent.Phone.ToString();
                txtCampus.Text = userStudent.Campus.ToString();
                txtSemester.Text = userStudent.Semester.ToString();
                txtGPA.Text = userStudent.Gpa.ToString();

                //Hide unavailable button
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                if (userStudent.Gpa <= 5 && userStudent.Semester < 5)
                {
                    btnRegister.Enabled = false;
                    txtNotification.Text = "You are not eligible for the OJT";
                }
                else
                {
                    btnRegister.Enabled = true;
                    txtNotification.Text = "Please register job to attend OJT semester";
                }
                dgvStudent.Enabled = false;
                dgvStudent.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Check null value
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtStudentName.Text) || string.IsNullOrWhiteSpace(txtStudentEmail.Text)
                    || string.IsNullOrWhiteSpace(txtStudentPassword.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtCampus.Text)
                    || string.IsNullOrWhiteSpace(txtSemester.Text) || string.IsNullOrWhiteSpace(txtGPA.Text) || rbMale.Checked == false && rbFemale.Checked == false)
                {
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    //Check if Student ID is exist
                    if (studentRepo.GetStudentByID(int.Parse(txtStudentID.Text)) != null)
                    {
                        MessageBox.Show("Student ID is already exist");
                    }
                    else
                    {
                        //Check if Student Email is exist
                        if (studentRepo.GetStudentByEmail(txtStudentEmail.Text) != null)
                        {
                            MessageBox.Show("Student email is already exist");
                        }
                        else
                        {
                            //Add new student to database using studentDAO
                            Student newStudent = new Student();
                            newStudent.StudentId = int.Parse(txtStudentID.Text);
                            newStudent.StudentName = txtStudentName.Text;
                            newStudent.StudentEmail = txtStudentEmail.Text;
                            newStudent.StudentPassword = txtStudentPassword.Text;
                            newStudent.DoB = dtpDoB.Value;
                            newStudent.Phone = txtPhone.Text;
                            newStudent.Campus = txtCampus.Text;
                            newStudent.Semester = int.Parse(txtSemester.Text);
                            newStudent.Gpa = float.Parse(txtGPA.Text);
                            if (rbMale.Checked == true)
                            {
                                newStudent.Gender = 1;
                            }
                            else if (rbFemale.Checked == true)
                            {
                                newStudent.Gender = 0;
                            }
                            studentRepo.AddStudent(newStudent);
                            MessageBox.Show("Student has been added!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Check null value
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtStudentName.Text) || string.IsNullOrWhiteSpace(txtStudentEmail.Text)
                    || string.IsNullOrWhiteSpace(txtStudentPassword.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtCampus.Text)
                    || string.IsNullOrWhiteSpace(txtSemester.Text) || string.IsNullOrWhiteSpace(txtGPA.Text) || rbMale.Checked == false && rbFemale.Checked == false)
                {
                    MessageBox.Show("Please fill all fields");
                }
                else
                {
                    //Check if Student ID is exist
                    if (studentRepo.GetStudentByID(int.Parse(txtStudentID.Text)) == null)
                    {
                        MessageBox.Show("Student ID is not exist");
                    }
                    else
                    {
                        //Check if Student Email is exist
                        if (studentRepo.GetStudentByEmail(txtStudentEmail.Text) != null)
                        {
                            MessageBox.Show("Student email is already exist");
                        }
                        else
                        {
                            //Update student to database using studentDAO
                            Student updateStudent = new Student();
                            updateStudent.StudentId = int.Parse(txtStudentID.Text);
                            updateStudent.StudentName = txtStudentName.Text;
                            updateStudent.StudentEmail = txtStudentEmail.Text;
                            updateStudent.StudentPassword = txtStudentPassword.Text;
                            updateStudent.DoB = dtpDoB.Value;
                            updateStudent.Phone = txtPhone.Text;
                            updateStudent.Campus = txtCampus.Text;
                            updateStudent.Semester = int.Parse(txtSemester.Text);
                            updateStudent.Gpa = float.Parse(txtGPA.Text);
                            if (rbMale.Checked == true)
                            {
                                updateStudent.Gender = 1;
                            }
                            else if (rbFemale.Checked == true)
                            {
                                updateStudent.Gender = 0;
                            }
                            studentRepo.UpdateStudent(updateStudent);
                            MessageBox.Show("Student has been updated!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Student deleteStudent = new Student();
                //Load data of selected student to textbox
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    int studentID = int.Parse(dgvStudent.SelectedRows[0].Cells[0].Value.ToString());
                    deleteStudent = studentRepo.GetStudentByID(studentID);
                    txtStudentID.Text = deleteStudent.StudentId.ToString();
                    txtStudentName.Text = deleteStudent.StudentName;
                    txtStudentEmail.Text = deleteStudent.StudentEmail;
                    txtStudentPassword.Text = deleteStudent.StudentPassword;
                    dtpDoB.Value = (DateTime)deleteStudent.DoB;
                }
                else
                {
                    MessageBox.Show("Please select a student");
                }
                //Delete student from database using studentDAO
                if (MessageBox.Show("Are you sure you want to delete this student?", "Delete Student", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    studentRepo.DeleteStudent(deleteStudent);
                    MessageBox.Show("Student has been deleted!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Return to login form 
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void lbClearText_Click(object sender, EventArgs e)
        {
            //Clear all text of textbox
            txtStudentID.ResetText();
            txtStudentName.ResetText();
            txtStudentEmail.ResetText();
            txtStudentPassword.ResetText();
            txtPhone.ResetText();
            txtCampus.ResetText();
            txtSemester.ResetText();
            txtGPA.ResetText();
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtStudentID.Text = dgvStudent.Rows[rowIndex].Cells[0].Value.ToString();
            txtStudentName.Text = dgvStudent.Rows[rowIndex].Cells[1].Value.ToString();
            txtStudentEmail.Text = dgvStudent.Rows[rowIndex].Cells[2].Value.ToString();
            txtStudentPassword.Text = dgvStudent.Rows[rowIndex].Cells[3].Value.ToString();
            dtpDoB.Value = (DateTime)dgvStudent.Rows[rowIndex].Cells[4].Value;
            if (dgvStudent.Rows[rowIndex].Cells[5].Value.ToString() == "1")
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }
            else if (dgvStudent.Rows[rowIndex].Cells[5].Value.ToString() == "0")
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;
            }
            txtPhone.Text = dgvStudent.Rows[rowIndex].Cells[6].Value.ToString();
            txtCampus.Text = dgvStudent.Rows[rowIndex].Cells[7].Value.ToString();
            txtSemester.Text = dgvStudent.Rows[rowIndex].Cells[8].Value.ToString();
            txtGPA.Text = dgvStudent.Rows[rowIndex].Cells[9].Value.ToString();
        }
    }
}
