using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company Get(string id);
        Company Add(Company item);
        void Remove(string id);
        bool Update(Company item);
        Company GetCompanyByName(string name);
        Company GetCompanyByID(string id);
    }
}
