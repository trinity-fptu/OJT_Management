using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DAOs;

namespace Library.Repositories.CompanyRepo
{
    public class CompanyRepo : ICompanyRepo
    {
        private CompanyDAO companyDAO;
        public CompanyRepo()
        {
            companyDAO = CompanyDAO.Instance;
        }
        public IEnumerable<Company> GetAllCompany()
            => companyDAO.GetAllCompany();
        public Company GetCompanyById(int companyId)
            => companyDAO.GetCompanyById(companyId);
        public void AddCompany(Company company)
            => companyDAO.AddCompany(company);
        public void UpdateCompany(Company company)
            => companyDAO.UpdateCompany(company);
        public void DeleteCompany(int companyId)
            => companyDAO.DeleteCompany(companyId);
    }
}
