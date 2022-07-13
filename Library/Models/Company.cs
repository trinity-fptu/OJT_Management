using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Company
    {
        public Company()
        {
            Jobs = new HashSet<Job>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
