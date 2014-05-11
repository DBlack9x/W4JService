using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace _5JobService.Models
{
    public class JobTagRepository : IJobTagRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();
        public JobTagRepository()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
   
        }
        public List<JobTag> GetAll()
        {


            var jobtags = db.JobTags.Include(j => j.Job).Include(j => j.Tag);
            return jobtags.ToList();
        }

        public JobTag Get(int id)
        {
            JobTag job = db.JobTags.Find(id);
            
            return job;
           
        }

        public JobTag Add(JobTag item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            db.JobTags.Add(item);
            db.SaveChanges();
           
            return item;
        }

        public void Remove(int id)
        {
            JobTag job = db.JobTags.Find(id);
            db.JobTags.Remove(job);
            db.SaveChanges();
        }

        public bool Update(JobTag item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public IEnumerable<JobTag> GetJobByTag(string tag)
        {
            var jtag = db.JobTags
                    .Where(b => b.Tag.TagID == tag).Include(t=>t.Job).Include(t=>t.Tag).Include(t=>t.Job.Company) ;
            List<JobTag> lst = jtag.ToList();
            if(lst==null)
            {
                var jt = db.JobTags
                    .Where(b => b.Tag.NameTag == tag).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
                return jt;
            }
            return jtag;
        }
        public IEnumerable<JobTag> GetJobByCompany(string company)
        {

            var jtag = db.JobTags
                    .Where(b => b.Job.Company.IDCompany== company).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
            List<JobTag> l = jtag.ToList();
            return jtag;
        }
        public IEnumerable<JobTag> FindJobByMultiTag(string tags)
        {
            string[] tmp = tags.Split(';');
            string query = "Select * from JobTag j0 ";
            if(tmp.Length>1)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (i != tmp.Length - 1)
                    {
                        string j1 = "j" + i;
                        string j2 = "j" + (i + 1);
                        query +=  " join JobTag " + j2 + " on (" + j1 + ".JobID =" + j2 + ".JobID) ";

                    }
                    else
                    {
                        query += " where ";
                        for (int j = 0; j < tmp.Length; j++)
                        {
                            if (j != tmp.Length - 1)
                            {
                                string job = "j" + j;
                                query += job + ".TagID='" + tmp[j] + "' and ";
                            }
                            else
                            {
                                string job = "j" + j;
                                query += job + ".TagID='" + tmp[j] + "'";
                            }
                        }
                    }

                }
            }
            else
            {
                query += " where j0.TagID='" + tmp[0] + "'";
            }           
           
           var jtag=     db.JobTags.SqlQuery(query);
            //var jtag = db.JobTags
            //        .Where lstTag.Contains( t =>t.).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
            List<JobTag> list = jtag.ToList();
            string saas = "";
            CompanyRepository cr = new CompanyRepository();
            foreach(JobTag jta in list)
            {
                jta.Job.Company = cr.GetCompanyByID(jta.Job.IDCompany);
            }
            return list;
        }
        public  IEnumerable<ChartData> CountJobByTagType(string tagtype,string timefrom, string timeto)
        {
            List<ChartData> lst = new List<ChartData>();
            if(tagtype=="NNLT")
            {
                ChartData c =CountJobByTag("Csharp", timefrom, timeto);
                c.ChartDTName = "C#";
                lst.Add(c);
                lst.Add(CountJobByTag("Java", timefrom, timeto));
                lst.Add(CountJobByTag("PHP", timefrom, timeto));

                c = CountJobByTag("cplusplus", timefrom, timeto);
                c.ChartDTName="C++";
                lst.Add(c);
                lst.Add(CountJobByTag("Drupal", timefrom, timeto));
                lst.Add(CountJobByTag("Ruby", timefrom, timeto));
            }
            if(tagtype=="NTDD")
            {
                lst.Add(CountJobByTag("Window Phone", timefrom, timeto));
                lst.Add(CountJobByTag("Android", timefrom, timeto));
                lst.Add(CountJobByTag("iOS", timefrom, timeto));

            }
            if (tagtype == "PTW")
            {
                ChartData c = CountJobByTag("ROR", timefrom, timeto);
                c.ChartDTName = "Ruby On Rail";
                lst.Add(c);

                c = CountJobByTag("Asp", timefrom, timeto);
                c.ChartDTName = "Asp.Net";
                lst.Add(c);


                c = CountJobByTag("drupal", timefrom, timeto);
                c.ChartDTName = "Drupal";
                lst.Add(c);

                c = CountJobByTag("php", timefrom, timeto);
                c.ChartDTName = "PHP";
                lst.Add(c);
                     
                lst.Add( CountJobByTag("Sharepoint", timefrom, timeto));
                lst.Add(CountJobByTag("WordPress", timefrom, timeto));
            }
            if (tagtype == "Location")
            {
                ChartData c = CountJobByTag("HCM", timefrom, timeto);
                c.ChartDTName = "Hồ Chí Minh";
                lst.Add(c);                
              
                c = CountJobByTag("DN", timefrom, timeto);
                c.ChartDTName = "Đà Nẵng";
                lst.Add(c);       
  
              
                c = CountJobByTag("HN", timefrom, timeto);
                c.ChartDTName = "Hà Nội";
                lst.Add(c);         

            }
            return lst;

        }
        public ChartData CountJobByTag(string tagid, string timefrom, string timeto)
        {

            int flag = 0;
            ChartData c = new ChartData();
            try
            {
                DateTime dt1 = DateTime.Parse(timefrom);
                DateTime dt2 = DateTime.Parse(timeto);
                if (DateTime.Compare(dt1, dt2) <= 0)
                {
                    var jtag = db.JobTags.Where(b => b.Tag.TagID == tagid && b.Job.PostDate> dt1 
                  && b.Job.PostDate < dt2).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
                    List<JobTag> jt = jtag.ToList();
                    c.ChartDTName = tagid;
                    c.ChartDTNumber = jt.Count;
                    return c;
                   
                }
                flag = 1;
               
             
            }
            catch (Exception e)
            {
             
            }
            if(flag!=1)
            {
                try
                {
                    DateTime dt1 = DateTime.Parse(timefrom);

                    var jtag = db.JobTags.Where(b => b.Tag.TagID == tagid && b.Job.PostDate> dt1).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
                    c.ChartDTName = tagid;
                    c.ChartDTNumber = jtag.Count();
                    return c;
                    flag = 2;
                }
                catch (Exception e)
                {
                 
                }
            }
            if (flag != 1 && flag != 2)
            {
                try
                {
                    DateTime dt2 = DateTime.Parse(timeto);

                    var jtag = db.JobTags.Where(b => b.Tag.TagID == tagid && b.Job.PostDate < dt2).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
                    c.ChartDTName = tagid;
                    c.ChartDTNumber = jtag.Count();
                    return c;
                    flag = 3;                  
                }
                catch (Exception e)
                {

                }
            }
            if(flag!=1 && flag!=2 && flag!=3)
            {
               
                try
                {
                    var jtag = db.JobTags.Where(b => b.Tag.TagID == tagid).Include(t => t.Job).Include(t => t.Tag).Include(t => t.Job.Company);
                    c.ChartDTName = tagid;
                    c.ChartDTNumber = jtag.Count();
                    return c;              
                }
                catch (Exception e)
                {

                }
            
            }

            
            return null;
          
        }
    }
}