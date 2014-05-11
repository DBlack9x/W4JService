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
    public class JobTagsController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly JobTagRepository repository = new JobTagRepository();
        public IEnumerable<JobTag> GetAllJobTags()
        {
            string aathoy = "";
            return repository.GetAll();
        }
        public JobTag GetJobTag(int id)
        {
            JobTag item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }


        public HttpResponseMessage PostJobTag(JobTag item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<JobTag>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.JTagID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJobTag(int id, JobTag job)
        {
            job.JTagID = id;
            if (!repository.Update(job))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteJobTag(int id)
        {
            JobTag item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
        public IEnumerable<JobTag> GetJobTagByTag(string tag)
        {
            var item = repository.GetJobByTag(tag);
            return item;
        }
        public IEnumerable<JobTag> GetJobTagByCompany(string company)
        {
            var item = repository.GetJobByCompany(company);
            return item;
        }
        public IEnumerable<ChartData> GetNumberOfJobByTagType(string tagtype, string timefrom, string timeto)
        {
            var item = repository.CountJobByTagType(tagtype, timefrom, timeto);
            return item;
        }
        public IEnumerable<JobTag> GetJobTagByMultiTag(string tags)
        {
            var item = repository.FindJobByMultiTag(tags);
            return item;
        }
	}
}