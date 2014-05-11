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
    public class JobsController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly JobRepository repository = new JobRepository();
        public IEnumerable<Job> GetAllJobs()
        {
            return repository.GetAll();
        }
        public Job GetJob(int id)
        {
            Job item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        public IEnumerable<Job> GetJobsByCompany(string company)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Company.Name, company, StringComparison.OrdinalIgnoreCase));
        }
       
        public HttpResponseMessage PostJob(Job item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Job>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.JobID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJob(int id, Job job)
        {
            job.JobID = id;
            if (!repository.Update(job))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteJob(int id)
        {
            Job item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
	}
}