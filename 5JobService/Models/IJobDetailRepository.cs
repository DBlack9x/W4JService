using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface IJobDetailRepository
    {
        IEnumerable<JobDetail> GetAll();
        Job Get(int id);
        Job Add(JobDetail item);
        void Remove(int id);
        bool Update(JobDetail item);
    }
}
