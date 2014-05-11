using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _5JobService.Models
{
    public class JobRepository : IJobRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();
        public IEnumerable<Job> GetAll()
        {
            var jobs = db.Jobs.Include(j => j.Company);
            return jobs;
        }

        public Job Get(int id)
        {
            Job job = db.Jobs.Find(id);
            
            return job;
           
        }

        public Job Add(Job item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            db.Jobs.Add(item);
            db.SaveChanges();
           
            return item;
        }

        public void Remove(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
        }

        public bool Update(Job item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}