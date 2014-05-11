using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace _5JobService.Models
{
    public class JobTempRepository : IJobTempRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();
        public JobTempRepository()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<JobTemp> GetAll()
        {
            var jobs = db.JobTemps;
            foreach(JobTemp j in jobs)
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DateTime));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, j.PostDate);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
               // j.PostDate.ToString = jsonString;
                //ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
              // j.PostDate = Dat ;
            }
            return jobs;
        }

        public JobTemp Get(int id)
        {
            JobTemp job = db.JobTemps.Find(id);
            
            return job;
           
        }

        public JobTemp Add(JobTemp item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            db.JobTemps.Add(item);
            db.SaveChanges();
           
            return item;
        }

        public void Remove(int id)
        {
            JobTemp job = db.JobTemps.Find(id);
            db.JobTemps.Remove(job);
            db.SaveChanges();
        }

        public bool Update(JobTemp item)
        {

            JobTemp entry = db.JobTemps.Find(item.JobTempID);

            db.Entry(entry).Property(e => e.IDProcessState).CurrentValue = item.IDProcessState;

            db.SaveChanges();
            
            return true;
        }
    }
}