using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories.JobRepo
{
    public interface IJobRepo
    {
        public IEnumerable<Job> GetAllJobs();
        public Job GetJobById(int jobId, int companyId);
        public void AddJob(Job job);
        public void UpdateJob(Job job);
        public void DeleteJob(Job job);
    }
}
