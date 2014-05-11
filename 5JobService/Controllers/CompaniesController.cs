using _5JobService.Models;
using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace _5JobService.Controllers
{
    public class CompaniesController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly CompanyRepository repository = new CompanyRepository();


        public IEnumerable<Company> GetAllCompanies()
        {
            return repository.GetAll();
        }
        public Company GetCompanyById(string id)
        {
            Company item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        public Company GetCompanyByName(string name)
        {
            Company item = repository.GetCompanyByName(name);          
            return item;
        }


        public HttpResponseMessage PostCompany(Company item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Company>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.IDCompany });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJob(string id, Company company)
        {
            company.IDCompany = id;
            if (!repository.Update(company))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteCompany(string id)
        {
            Company item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
	}
}