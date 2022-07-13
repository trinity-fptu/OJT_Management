using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DAOs
{
    public class JobDAO
    {
        //Using Singleton pattern to get the instance of the class
        private static JobDAO instance;
        private static object instanceLock = new object();
        private JobDAO() { }
        public static JobDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new JobDAO();
                    }
                }
                return instance;
            }
        }
        //Get all jobs from the database
        public IEnumerable<Job> GetAllJobs()
        {
            using (var context = new OJT_ManagementContext())
            {
                return context.Jobs.ToList();
            }
        }
        //Get a job by id from the database
        public Job GetJobById(int jobId, int companyId)
        {
            using (var context = new OJT_ManagementContext())
            {
                return context.Jobs.Where(j => j.JobId == jobId && j.CompanyId == companyId).FirstOrDefault();
            }
        }

        //Add a job to the database
        public void AddJob(Job job)
        {
            try
            {
                if (GetJobById(job.JobId, job.CompanyId) == null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        context.Jobs.Add(job);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Job already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Update a job in the database
        public void UpdateJob(Job job)
        {
            try
            {
                if (GetJobById(job.JobId, job.CompanyId) != null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        context.Jobs.Update(job);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Job does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Delete a job from the database
        public void DeleteJob(Job job)
        {
            try
            {
                if (GetJobById(job.JobId, job.CompanyId) != null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        context.Jobs.Remove(job);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Job does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
