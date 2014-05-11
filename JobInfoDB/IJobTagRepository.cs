using JobInfoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5JobService.Models
{
    interface IJobTagRepository
    {
        List<JobTag> GetAll();
        JobTag Get(int id);
        JobTag Add(JobTag item);
        void Remove(int id);
        bool Update(JobTag item);
        IEnumerable<JobTag> GetJobByTag(string tag);
        IEnumerable<JobTag> GetJobByCompany(string company);
        IEnumerable<JobTag> FindJobByMultiTag(string tags);
        IEnumerable<ChartData> CountJobByTagType(string tagtype, string timefrom, string timeto);
    }
}
