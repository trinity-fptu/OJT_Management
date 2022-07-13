using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories.StudentRepo
{
    public interface IStudentRepo
    {
        public IEnumerable<Student> GetStudentList();
        public Student GetStudentByID(int studentId);
        public Student GetStudentByEmail(string studentEmail);
        public Student GetStudentByEmailAndPassword(string studentEmail, string studentPassword);       
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void DeleteStudent(Student student);
    }
}
