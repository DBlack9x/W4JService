using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _5JobService.Models
{
    public class TagRepository : ITagRepository
    {
        private JobDatabaseEntities db = new JobDatabaseEntities();

        public TagRepository()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public IEnumerable<Tag> GetAll()
        {
            var tag = db.Tags.Include(t=>t.TagType);
            return tag;


        }

        public Tag Get(string id)
        {
            Tag t = db.Tags.Find(id);

            return t;
           
        }

        public Tag Add(Tag item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }


            db.Tags.Add(item);

            db.SaveChanges();
           
            return item;
        }

        public void Remove(string id)
        {
            Tag t = db.Tags.Find(id);
            db.Tags.Remove(t);
            db.SaveChanges();
        }

        public bool Update(Tag item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public Tag GetTagByName(string name)
        {
            var t = db.Tags
                    .Where(b => b.NameTag == name)
                    .FirstOrDefault();
            return t;
        }
    }
}