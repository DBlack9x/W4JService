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
    public class JobUrlsController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly JobUrlRepository repository = new JobUrlRepository();
        public List<JobUrl> GetAllJobUrls()
        {
            return repository.GetAll();
        }
        public JobUrl GetJobUrl(string id)
        {
            JobUrl item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostJob(JobUrl item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<JobUrl>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.IDJobUrl});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJob(string id, JobUrl job)
        {
            job.IDJobUrl = id;
            if (!repository.Update(job))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteJob(string id)
        {
            JobUrl item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
	}
}