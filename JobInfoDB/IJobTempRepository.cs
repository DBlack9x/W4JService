using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface IJobTempRepository
    {
        IEnumerable<JobTemp> GetAll();
        JobTemp Get(int id);
        JobTemp Add(JobTemp item);
        void Remove(int id);
        bool Update(JobTemp item);
    }
}
