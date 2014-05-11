using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface IJobRepository
    {
        List<Job> GetAll();
        Job Get(int id);
        Job Add(Job item);
        void Remove(int id);
        bool Update(Job item);
    }
}
