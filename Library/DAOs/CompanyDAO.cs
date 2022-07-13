using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.DAOs
{
    public class CompanyDAO
    {
        //Using Singleton pattern 
        private static CompanyDAO instance;
        private static object instanceLock = new object();
        private CompanyDAO() { }
        public static CompanyDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CompanyDAO();
                    }
                }
                return instance;
            }
        }

        //Get all company from the database
        public IEnumerable<Company> GetAllCompany()
        {
            using (var context = new OJT_ManagementContext())
            {
                return context.Companies.ToList();
            }
        }

        //Get company by id from the database
        public Company GetCompanyById(int companyId)
        {
            using (var context = new OJT_ManagementContext())
            {
                return context.Companies.Find(companyId);
            }
        }

        //Add company to the database
        public void AddCompany(Company company)
        {
            try
            {
                if (GetCompanyById(company.CompanyId) == null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        context.Companies.Add(company);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Company already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Update company in the database
        public void UpdateCompany(Company company)
        {
            try
            {
                if (GetCompanyById(company.CompanyId) != null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        context.Companies.Attach(company);
                        context.Entry(company).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Company does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete company from the database
        public void DeleteCompany(int companyId)
        {
            try
            {
                if (GetCompanyById(companyId) != null)
                {
                    using (var context = new OJT_ManagementContext())
                    {
                        var company = context.Companies.Find(companyId);
                        context.Companies.Remove(company);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Company does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
