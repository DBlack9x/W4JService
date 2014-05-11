using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _5JobService.Models
{
    public class CompanyRepository : ICompanyRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();

        public CompanyRepository()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<Company> GetAll()
        {
            var company = db.Companies;
            return company;


        }

        public Company Get(string id)
        {
            Company company = db.Companies.Find(id);

            return company;
           
        }

        public Company Add(Company item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            
            
            db.Companies.Add(item);

            db.SaveChanges();
           
            return item;
        }

        public void Remove(string id)
        {
            Company Company = db.Companies.Find(id);
            db.Companies.Remove(Company);
            db.SaveChanges();
        }

        public bool Update(Company item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public Company GetCompanyByName(string name)
        {
            var company = db.Companies
                    .Where(b => b.Name == name)
                    .FirstOrDefault();
            return company;
        }
        public Company GetCompanyByID(string id)
        {
            var company = db.Companies
                    .Where(b => b.IDCompany == id)
                    .FirstOrDefault();
            return company;
        }
    }
}