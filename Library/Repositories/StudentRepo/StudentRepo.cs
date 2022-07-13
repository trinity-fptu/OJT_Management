using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DAOs;

namespace Library.Repositories.StudentRepo
{
    public class StudentRepo : IStudentRepo
    {
        private StudentDAO studentDAO;
        public StudentRepo()
        {
            studentDAO = StudentDAO.Instance;
        }
        public IEnumerable<Student> GetStudentList()
        {
            return studentDAO.GetStudentList();
        }

        public Student GetStudentByID(int studentId)
            => studentDAO.GetStudentByID(studentId);
        public Student GetStudentByEmail(string studentEmail)
            => studentDAO.GetStudentByEmail(studentEmail);
        public Student GetStudentByEmailAndPassword(string studentEmail, string studentPassword)
            => studentDAO.GetStudentByEmailAndPassword(studentEmail, studentPassword);
        public void AddStudent(Student student)
            => studentDAO.AddStudent(student);

        public void UpdateStudent(Student student)
            => studentDAO.UpdateStudent(student);

        public void DeleteStudent(Student student)
            => studentDAO.DeleteStudent(student);

    }
}
