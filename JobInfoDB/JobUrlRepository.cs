using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace _5JobService.Models
{
    public class JobUrlRepository : IJobUrlRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();

        public JobUrlRepository()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public List<JobUrl> GetAll()
        {
           
           // var joburls = db.JobUrls.Include(j => j.ProcessState);
            db.Configuration.ProxyCreationEnabled = false;
            List<JobUrl> joburls1 = db.JobUrls.ToList();

            return joburls1;
        }

        public JobUrl Get(string id)
        {
            JobUrl job = db.JobUrls.Find(id);
            
            return job;
           
        }

        public JobUrl Add(JobUrl item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            db.JobUrls.Add(item);
            db.SaveChanges();
           
            return item;
        }

        public void Remove(string id)
        {
            JobUrl job = db.JobUrls.Find(id);
            db.JobUrls.Remove(job);
            db.SaveChanges();
        }

        public bool Update(JobUrl item)
        {
            
            JobUrl entry = db.JobUrls.Find(item.IDJobUrl);
          
            db.Entry(entry).Property(e => e.IDProcessState).CurrentValue = item.IDProcessState;
            
            db.SaveChanges();
            return true;
        }
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}