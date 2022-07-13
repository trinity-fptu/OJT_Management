using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories.CompanyRepo
{
    public interface ICompanyRepo
    {
        public IEnumerable<Company> GetAllCompany();
        public Company GetCompanyById(int companyId);
        public void AddCompany(Company company);
        public void UpdateCompany(Company company);
        public void DeleteCompany(int companyId);
    }
}
