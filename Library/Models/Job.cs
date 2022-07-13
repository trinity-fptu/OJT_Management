using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Job
    {
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public string JobName { get; set; }
        public string Type { get; set; }
        public string Require { get; set; }
        public int Quantity { get; set; }

        public virtual Company Company { get; set; }
    }
}
