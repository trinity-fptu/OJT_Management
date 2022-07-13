using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DAOs;

namespace Library.Repositories.JobRepo
{
    public class JobRepo : IJobRepo
    {
        private JobDAO jobDAO;
        public JobRepo()
        {
            jobDAO = JobDAO.Instance;
        }
        public IEnumerable<Job> GetAllJobs()
            => jobDAO.GetAllJobs();
        public Job GetJobById(int jobId, int companyId)
            => jobDAO.GetJobById(jobId, companyId);
        public void AddJob(Job job)
            => jobDAO.AddJob(job);
        public void UpdateJob(Job job)
            => jobDAO.UpdateJob(job);
        public void DeleteJob(Job job)
            => jobDAO.DeleteJob(job);
    }
}
