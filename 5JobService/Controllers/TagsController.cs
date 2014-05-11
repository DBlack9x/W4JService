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
    public class TagsController : ApiController
    {
        //
        // GET: /Jobs/
        static readonly TagRepository repository = new TagRepository();

        public IEnumerable<Tag> GetAllTags()
        {
            return repository.GetAll();
        }
        public Tag GetTag(string id)
        {
            Tag item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }


        public HttpResponseMessage PostTag(Tag item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Tag>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.TagID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutJobTag(string id, Tag item)
        {
            item.TagID = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteJobTag(string id)
        {
            Tag item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}