using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPassword { get; set; }
        public int? Gender { get; set; }
        public DateTime? DoB { get; set; }
        public string Phone { get; set; }
        public string Campus { get; set; }
        public int? Semester { get; set; }
        public double Gpa { get; set; }
    }
}
