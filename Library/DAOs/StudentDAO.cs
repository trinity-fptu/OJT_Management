using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DAOs
{
    public class StudentDAO
    {
        //Using Singleton pattern
        private static StudentDAO instance = null;
        private static readonly object instanceLock = new object();
        private StudentDAO()
        {

        }
        public static StudentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StudentDAO();
                    }
                }
                return instance;
            }
        }

        //Get StudentList from database
        public IEnumerable<Student> GetStudentList()
        {
            IEnumerable<Student> list = GetStudentList();
            try
            {
                var context = new OJT_ManagementContext();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //CRUD
        //Get Student by ID
        public Student GetStudentByID(int studentId)
        {
            try
            {
                var context = new OJT_ManagementContext();
                return context.Students.Where(x => x.StudentId == studentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get Student by Email and Password
        public Student GetStudentByEmailAndPassword(string studentEmail, string studentPassword)
        {
            try
            {
                var context = new OJT_ManagementContext();
                return context.Students.Where(x => x.StudentEmail == studentEmail && x.StudentPassword == studentPassword).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Add new Student
        public void AddStudent(Student student)
        {
            try
            {
                if (GetStudentByEmailAndPassword(student.StudentEmail, student.StudentPassword) != null)
                {
                    throw new Exception("Student already exists");
                }
                else
                {
                    var context = new OJT_ManagementContext();
                    context.Students.Add(student);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Update Student
        public void UpdateStudent(Student student)
        {
            try
            {
                if (GetStudentByID(student.StudentId) != null)
                {
                    var context = new OJT_ManagementContext();
                    context.Entry(student).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Student not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete Student
        public void DeleteStudent(Student student)
        {
            try
            {
                if (GetStudentByID(student.StudentId) != null)
                {
                    var context = new OJT_ManagementContext();
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Student not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
