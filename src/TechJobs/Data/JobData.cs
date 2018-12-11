using System;
using System.Collections.Generic;
using System.Linq;
using TechJobs.Models;

namespace TechJobs.Data
{
    class JobData
    {
    

        public List<Job> Jobs { get; set; } = new List<Job>();
        public JobFieldData<Employer> Employers { get; set; } = new JobFieldData<Employer>();
        public JobFieldData<Location> Locations { get; set; } = new JobFieldData<Location>();
        public JobFieldData<PositionType> PositionTypes { get; set; } = new JobFieldData<PositionType>();
        public JobFieldData<CoreCompetency> CoreCompetencies { get; set; } = new JobFieldData<CoreCompetency>();


        private JobData()
        {
            JobDataImporter.LoadData(this);
        }

        private static JobData instance;
        public static JobData GetInstance()
        {
            if (instance == null)
            {
                instance = new JobData();
            }

            return instance;
        }



        public List<Job> FindByValue(string value)
        {
            var results = from j in Jobs
                          where j.Employer.Contains(value)
                          || j.Location.Contains(value)
                          || j.Name.ToLower().Contains(value.ToLower())
                          || j.CoreCompetency.Contains(value)
                          || j.PositionType.Contains(value)
                          select j;

            return results.ToList();
        }


   
        public List<Job> FindByColumnAndValue(JobFieldType column, string value)
        {
            var results = from j in Jobs
                          where GetFieldByType(j, column).Contains(value)
                          select j;

            return results.ToList();
        }


        public static JobField GetFieldByType(Job job, JobFieldType type)
        {
            switch (type)
            {
                case JobFieldType.Employer:
                    return job.Employer;
                case JobFieldType.Location:
                    return job.Location;
                case JobFieldType.CoreCompetency:
                    return job.CoreCompetency;
                case JobFieldType.PositionType:
                    return job.PositionType;
            }

            throw new ArgumentException("Cannot get field of type: " + type);
        }


      
        public Job Find(int id)
        {
            var results = from j in Jobs
                          where j.ID == id
                          select j;

            return results.Single();
        }

    }
}
