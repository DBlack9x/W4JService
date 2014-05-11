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
    public class JobTempsController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly JobTempRepository repository = new JobTempRepository();
        public IEnumerable<JobTemp> GetAllJobs()
        {
            return repository.GetAll();
        }
        public JobTemp GetJob(int id)
        {
            JobTemp item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
       

        public HttpResponseMessage PostJob(JobTemp item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<JobTemp>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.JobTempID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJob(int id, JobTemp job)
        {
            job.JobTempID = id;
            if (!repository.Update(job))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteJob(int id)
        {
            JobTemp item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
	}
}