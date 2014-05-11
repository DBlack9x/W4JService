using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface IJobUrlRepository
    {
        List<JobUrl> GetAll();
        JobUrl Get(string id);
        JobUrl Add(JobUrl item);
        void Remove(string id);
        bool Update(JobUrl item);
    }
}
