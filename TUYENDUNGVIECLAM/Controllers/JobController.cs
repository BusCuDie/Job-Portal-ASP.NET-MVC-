using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class JobController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();
        // GET: Job
        public ActionResult Index()
        {
            if (Session["userRole"]!=null && Session["userRole"].Equals("Jobseeker"))
            {
                string id = Session["userid"].ToString();
                ViewBag.checksave = context.Saveds.Where(x => x.jobseekr_usernmae == id).ToList();

            }
            string keyword = Request.QueryString["keyword"];
            string loaction = Request.QueryString["location"];


            List<Job> ls = null;
            if (Request.QueryString.Count == 0)
            {
                ls = context.Jobs.Where(x=>x.job_state==1).OrderByDescending(x => x.job_postdate).ToList();
            }
            else
            {
                ls = context.Jobs.Where(x => ( x.job_title.Contains(keyword) || x.job_location.Contains(keyword)) && x.job_state == 1).ToList();
            }
            return View(ls);
        }

        public ActionResult Create()
        {
            if (Session["userid"] == null && Session["username"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            Job job = new Job();
            if (Request.Form.Count > 0)
            {
              
                job.job_title = Request.Form["job_title"];
                job.job_description = Request.Form["job_description"];
               
                    job.job_type = Request.Form["job_type"];
                job.job_salar = Request.Form["job_salar"];

                job.job_experience = Request.Form["job_experience"];
                job.job_location = Request.Form["job_location"];
                job.job_state = 0;
                job.job_address = Request.Form["job_address"];
                job.job_category = Request.Form["category_id"];
                string strdate = Request.Form["job_starttime"];
                if (!String.IsNullOrEmpty(strdate))
                {
                    DateTime starttime = Convert.ToDateTime(strdate);
                    job.job_starttime = starttime;
                }
               
             
                DateTime date_post = DateTime.Now;
                job.job_postdate = date_post;
                job.extraskill = Request.Form["extraskill"];
                job.require_skill = Request.Form["require_skill"];
                job.job_benefit = Request.Form["job_benefit"];
                if (Session["userid"] != null)
                {

                    job.employer_usename = Session["userid"].ToString();
                }
                if (String.IsNullOrEmpty(job.job_title) || String.IsNullOrEmpty(job.job_description) ||
                    String.IsNullOrEmpty(job.job_salar) || String.IsNullOrEmpty(job.job_address) ||
                    String.IsNullOrEmpty(job.extraskill) || String.IsNullOrEmpty(job.require_skill) ||
                    String.IsNullOrEmpty(job.job_benefit) ||String.IsNullOrEmpty(strdate))
                {
                    ViewData["job_err"] = "VUI LÒNG ĐIỂN ĐÂY ĐỦ THÔNG TIN VỀ CÔNG VIỆC";
                }
                else
                {
                    context.Jobs.InsertOnSubmit(job);
                    context.SubmitChanges();
                    return RedirectToAction("Index", "Job");
                }
            }
            return View(job);
        }
        public ActionResult Edit(int id)
        {
            Job job = context.Jobs.FirstOrDefault(x => x.job_id == id);
            if (Request.Form.Count == 0)
            {
                return View(job);
            }
            job.job_title = Request.Form["job_title"];
            job.job_description = Request.Form["job_description"];
            job.job_type = Request.Form["job_type"];
            job.job_salar = Request.Form["job_salar"];

            job.job_experience = Request.Form["job_experience"];
            job.job_location = Request.Form["job_location"];
            job.job_state = 0;
            job.job_address = Request.Form["job_address"];
            job.job_category = Request.Form["category_id"];
            string strdate = Request.Form["job_starttime"];
            if (!String.IsNullOrEmpty(strdate))
            {
                DateTime starttime = Convert.ToDateTime(strdate);
                job.job_starttime = starttime;
            }
           
            job.extraskill = Request.Form["extraskill"];
            job.require_skill = Request.Form["require_skill"];
            job.job_benefit = Request.Form["job_benefit"];

            if (String.IsNullOrEmpty(job.job_title) || String.IsNullOrEmpty(job.job_description) ||
                String.IsNullOrEmpty(job.job_salar) || String.IsNullOrEmpty(job.job_address) ||
                String.IsNullOrEmpty(job.extraskill) || String.IsNullOrEmpty(job.require_skill) ||
                String.IsNullOrEmpty(job.job_benefit)|| String.IsNullOrEmpty(strdate))
            {
                ViewData["job_err"] = "VUI LÒNG ĐIỂN ĐÂY ĐỦ THÔNG TIN VỀ CÔNG VIỆC";
            }
            else
            {
               
                context.SubmitChanges();
                return RedirectToAction("Details", "Job",new { id =job.job_id});
            }
            return View(job);
        }
        public ActionResult Details(int id)
        {
            if (Session["userRole"] != null && Session["userRole"].Equals("Jobseeker"))
            {
                string jobseeker_id =Session["userid"].ToString();
                ViewBag.checkapply = context.Applications.Where(x => x.jobseekr_usernmae == jobseeker_id).ToList();

            }
            var job = context.Jobs.FirstOrDefault(x => x.job_id == id);
            return View(job);
        }
        public ActionResult Delete(int id)
        {
        
            Job job = context.Jobs.FirstOrDefault(x => x.job_id == id);
            context.Jobs.DeleteOnSubmit(job);
            context.SubmitChanges();
            return RedirectToAction("JobPosted", "Employer");
        }
    }
}