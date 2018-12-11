using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

       
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        
        public IActionResult Index(int id)
        {
           

            Job theJob = jobData.Find(id);
            NewJobViewModel theRealJob = new NewJobViewModel();

            theRealJob.Name = theJob.Name;
            theRealJob.EmployerID = theJob.Employer.ID;
            theRealJob.CoreCompetencyID = theJob.CoreCompetency.ID;
            theRealJob.LocationID = theJob.Location.ID;
            theRealJob.PositionTypeID = theJob.PositionType.ID;

            return View(theJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
           

            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)
                };

                jobData.Jobs.Add(newJob);
                return Redirect(String.Format("/job?id={0}", newJob.ID));
            }

        

            return View(newJobViewModel);
        }
    }
}