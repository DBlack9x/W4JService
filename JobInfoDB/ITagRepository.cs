using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag Get(string id);
        Tag Add(Tag item);
        void Remove(string id);
        bool Update(Tag item);
        Tag GetTagByName(string name);
    }
}
