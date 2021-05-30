using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class JobseekerController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();
        // GET: Jobseeker
        public ActionResult AllJobseeker()
        {
            var s = context.Jobseekers.ToList();
            return View(s);
        }
        public ActionResult Application(int job_id)
        {
            Application application = new Application();
            string jobseeker_id = Session["userid"].ToString();
            int i = context.Applications.Where(x => x.job_id == job_id && x.jobseekr_usernmae == jobseeker_id).Count();
            if (i == 0)
            {

                application.job_id = job_id;
                application.jobseekr_usernmae = jobseeker_id;
                context.Applications.InsertOnSubmit(application);
                context.SubmitChanges();
                return RedirectToAction("Details", "Job", new { id = job_id });
            }

            return View();
        }
        public ActionResult Save(int job_id)
        {
            Saved save = new Saved();
            string jobseeker_id = Session["userid"].ToString();
            int i = context.Saveds.Where(x => x.job_id == job_id && x.jobseekr_usernmae == jobseeker_id).Count();
            if (i == 0)
            {
                save.job_id = job_id;
                save.jobseekr_usernmae = jobseeker_id;
                context.Saveds.InsertOnSubmit(save);
                context.SubmitChanges();
                return RedirectToAction("Index", "Job");
            }

            return View();
        }
        public ActionResult Saved(string id)
        {
            ViewBag.saved = context.Save(id).ToList();
            return View();
        }
        public ActionResult Applied(string id)
        {
            ViewBag.applied = context.Applied(id).ToList();
            return View();

        }
        public ActionResult Jobseeker_Profile(string id)
        {
            Jobseeker jsk = context.Jobseekers.FirstOrDefault(x => x.username == id);
            return View(jsk);

        }
        public ActionResult Cancel_Apply(int job_id)
        {
            string jobseeker_id = Session["userid"].ToString();
            Application application = context.Applications.FirstOrDefault(x => x.job_id == job_id && x.jobseekr_usernmae == jobseeker_id);
            context.Applications.DeleteOnSubmit(application);
            context.SubmitChanges();
            return RedirectToAction("Applied", "Jobseeker", new { id = Session["userid"].ToString() });
        }
        public ActionResult Cancel_Save(int job_id)
        {
            string jobseeker_id = Session["userid"].ToString();
            Saved save = context.Saveds.FirstOrDefault(x => x.job_id == job_id && x.jobseekr_usernmae == jobseeker_id);
            context.Saveds.DeleteOnSubmit(save);
            context.SubmitChanges();
            return RedirectToAction("Saved", "Jobseeker", new { id = Session["userid"].ToString() });
        }
        public ActionResult EditProfile_JSK(string id)
        {
            Jobseeker jobseeker = context.Jobseekers.FirstOrDefault(x => x.username == id);
            if (Request.Form.Count > 0)
            {
                jobseeker.name = Request.Form["name"];
                jobseeker.phone = Request.Form["phone"];
                jobseeker.address = Request.Form["address"];
                jobseeker.experience = Request.Form["experience"];
                jobseeker.gender = Request.Form["gender"];
                jobseeker.skill = Request.Form["skill"];
                string aboutme = Request.Form["aboutme"];
                if (!String.IsNullOrEmpty(aboutme))
                {
                    jobseeker.aboutme = aboutme;
                }
                jobseeker.current_role = Request.Form["current_role"];
                string day = Request.Form["birthday"];
                DateTime birthday = Convert.ToDateTime(jobseeker.birthday);
                if (!String.IsNullOrEmpty(day))
                {
                    birthday = Convert.ToDateTime(day);
                }

                jobseeker.birthday = birthday;
                jobseeker.extraskill = Request.Form["extraskill"];
                jobseeker.place = Request.Form["place"];
                jobseeker.educational = Request.Form["educational"];
                jobseeker.achivement = Request.Form["achivement"];
                jobseeker.career = Request.Form["career"];
                if (String.IsNullOrEmpty(jobseeker.name) || String.IsNullOrEmpty(jobseeker.phone) ||
                String.IsNullOrEmpty(jobseeker.address) || String.IsNullOrEmpty(jobseeker.skill) ||
                String.IsNullOrEmpty(jobseeker.aboutme) || String.IsNullOrEmpty(jobseeker.current_role) ||
                String.IsNullOrEmpty(jobseeker.extraskill) || String.IsNullOrEmpty(jobseeker.career) ||
                 String.IsNullOrEmpty(jobseeker.career) || String.IsNullOrEmpty(day) ||
                String.IsNullOrEmpty(jobseeker.achivement))
                {
                    ViewData["js_error"] = "THÔNG TIN KHÔNG ĐƯỢC ĐỂ TRỐNG,VUI LÒNG NHẬP ĐẦY ĐỦ";
                }
                else
                {
                    context.SubmitChanges();
                    return RedirectToAction("Jobseeker_Profile", "Jobseeker", new { id = jobseeker.username });
                }


            }
            return View(jobseeker);
        }
        public ActionResult ChangePassword(string id)
        {
            Jobseeker jobseeker = context.Jobseekers.FirstOrDefault(x => x.username == id);
            if (Request.Form.Count > 0)
            {
                jobseeker.password = Request.Form["password"];
                string repass = Request.Form["repass"];

                if (String.IsNullOrEmpty(jobseeker.password) || String.IsNullOrEmpty(repass))
                {
                    ViewData["err_empty"] = "KHÔNG ĐƯỢC ĐỂ TRỐNG";



                }
                else if (!jobseeker.password.Equals(repass))
                {

                    ViewData["pass_err"] = "MẬT KHẨU KHÔNG KHỚP,VUI LÒNG THỬ LẠI";


                }
                else
                {
                   
                    context.SubmitChanges();
                    return RedirectToAction("Index", "Job");
                }
            }
            return View(jobseeker);
        }
    }
}