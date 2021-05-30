using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class AdminController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();

        // GET: Admin
        public ActionResult ListJob()
        {
            var ls = context.Jobs.Where(x => x.job_state == 0).ToList();

          
            return View(ls);
        }
        public ActionResult AcceptJob(int id)
        {
            Job job = context.Jobs.FirstOrDefault(x => x.job_id == id);
            if (job != null)
            {
                job.job_state = 1;
                context.SubmitChanges();
                return RedirectToAction("ListJob", "Admin");
            }
            return View();
        }
        public ActionResult JobDetails(int id)
        {
            var job = context.Jobs.FirstOrDefault(x => x.job_id == id);
            return View(job);
        }
        public ActionResult ManageEmployer()
        {
            var ls = context.Employers.ToList();


            return View(ls);
        }
        public ActionResult AddAEmployer()
        {
            Employer em = new Employer();

            if (Request.Form.Count > 0)
            {

                em.name = Request.Form["name"].Trim();
                em.aboutus = Request.Form["aboutus"].Trim();
                em.tel = Request.Form["tel"].Trim();
                em.email = Request.Form["email"].Trim();
                em.username = Request.Form["username"].Trim();
                em.password = Request.Form["password"].Trim();
                string RenterPass = Request.Form["RenterPass"].Trim();
                em.location = Request.Form["location"].Trim();
                em.address = Request.Form["address"].Trim();
                em.webiste = Request.Form["webiste"].Trim();
                em.amount_employee = Request.Form["amount_employee"].Trim();
                em.contact_person = Request.Form["contact_person"].Trim();
                em.contact_phone = Request.Form["contact_phone"].Trim();

                Employer test = context.Employers.FirstOrDefault(x => x.username == em.username);
                if (String.IsNullOrEmpty(em.name) || String.IsNullOrEmpty(em.aboutus) ||
                    String.IsNullOrEmpty(em.tel) || String.IsNullOrEmpty(em.email) ||
                    String.IsNullOrEmpty(em.password) || String.IsNullOrEmpty(RenterPass) ||
                    String.IsNullOrEmpty(em.address) || String.IsNullOrEmpty(em.webiste) ||
                    String.IsNullOrEmpty(em.amount_employee) || String.IsNullOrEmpty(em.contact_person) ||
                    String.IsNullOrEmpty(em.contact_phone) || String.IsNullOrEmpty(em.username))
                {
                    ViewData["em_error"] = "THÔNG TIN KHÔNG ĐƯỢC ĐỂ TRỐNG,VUI LÒNG NHẬP ĐẦY ĐỦ";
                }
                else if (test != null)
                {
                    ViewData["em_user"] = "TÊN NGƯỜI DÙNG ĐÃ TỒN TẠI, VUI LÒNG NHẬP TÊN NGƯỜI DÙNG KHÁC";
                }          
                else if (!em.password.Equals(RenterPass))
                {
                    ViewData["em_err_pass"] = "MẬT KHẨU KHÔNG TRÙNG KHỚP";
                }
                else
                {

                    context.Employers.InsertOnSubmit(em);
                    context.SubmitChanges();
                    return RedirectToAction("ManageEmployer", "Admin");
                }


            }

            return View(em);
        }
        public ActionResult DetailsEmp(string username)
        {
            var em = context.Employers.FirstOrDefault(x => x.username == username);
            if (em != null)
            {
                return View(em);

            }
            return View();
        }
        public ActionResult DeleteEmp(string username)
        {
            Employer em = context.Employers.FirstOrDefault(x => x.username == username);
           
            if (em == null)
            {
                return View();

            }
            context.Employers.DeleteOnSubmit(em);
            context.SubmitChanges();
            return RedirectToAction("ManageEmployer", "Admin");
        }
    
        public ActionResult ManageJobseeker()
        {
            var ls = context.Jobseekers.ToList();


            return View(ls);
        }
        public ActionResult AddAJobseeker()
        {
            Jobseeker jobseeker = new Jobseeker();
            if (Request.Form.Count > 0)
            {
                jobseeker.name = Request.Form["name"];
                jobseeker.phone = Request.Form["phone"];
                jobseeker.address = Request.Form["address"];
                jobseeker.experience = Request.Form["experience"];
                jobseeker.skill = Request.Form["skill"];
                jobseeker.aboutme = Request.Form["aboutme"];
                jobseeker.current_role = Request.Form["current_role"];
                jobseeker.username = Request.Form["username"];
                jobseeker.password = Request.Form["password"];
                string day = Request.Form["birthday"];
                if (!String.IsNullOrEmpty(day))
                {
                    DateTime birthday = Convert.ToDateTime(day);
                    jobseeker.birthday = birthday;
                }


                string repass = Request.Form["repass"];
                jobseeker.gender = Request.Form["gender"];
                jobseeker.extraskill = Request.Form["extraskill"];
                jobseeker.email = Request.Form["email"];
                jobseeker.place = Request.Form["place"];
                jobseeker.educational = Request.Form["educational"];
                jobseeker.achivement = Request.Form["achivement"];
                jobseeker.career = Request.Form["career"];
                Jobseeker test = context.Jobseekers.FirstOrDefault(x => x.username == jobseeker.username);
                if (String.IsNullOrEmpty(jobseeker.name) || String.IsNullOrEmpty(jobseeker.phone) ||
                    String.IsNullOrEmpty(jobseeker.address) || String.IsNullOrEmpty(jobseeker.skill) ||
                    String.IsNullOrEmpty(jobseeker.aboutme) || String.IsNullOrEmpty(jobseeker.current_role) ||
                    String.IsNullOrEmpty(jobseeker.password) || String.IsNullOrEmpty(jobseeker.extraskill) ||
                    String.IsNullOrEmpty(jobseeker.email) || String.IsNullOrEmpty(jobseeker.career) ||
                    String.IsNullOrEmpty(jobseeker.achivement) || String.IsNullOrEmpty(day) || String.IsNullOrEmpty(jobseeker.username) || String.IsNullOrEmpty(jobseeker.gender))
                {
                    ViewData["js_error"] = "THÔNG TIN KHÔNG ĐƯỢC ĐỂ TRỐNG,VUI LÒNG NHẬP ĐẦY ĐỦ";
                }
                else if (test != null)
                {
                    ViewData["js_email"] = "TÊN NGƯỜI DÙNG ĐÃ TỒN TẠI, VUI LÒNG NHẬP TÊN NGƯỜI DÙNG KHÁC";
                }
                else if (!jobseeker.password.Equals(repass))
                {
                    ViewData["js_err_pass"] = "MẬT KHẨU KHÔNG TRÙNG KHỚP";
                }
                else
                {

                    context.Jobseekers.InsertOnSubmit(jobseeker);
                    context.SubmitChanges();
                    return RedirectToAction("ManageJobseeker", "Admin");
                }

            }
            return View(jobseeker);
        }
        public ActionResult EditJSK(string username)
        {
            Jobseeker jobseeker = context.Jobseekers.FirstOrDefault(x => x.username == username);
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
                    return RedirectToAction("ManageJobseeker", "Admin");
                }


            }
            return View(jobseeker);
        }
        public ActionResult DetailJSK(string username)
        {
            Jobseeker jsk = context.Jobseekers.FirstOrDefault(x => x.username == username);
            if (jsk != null)
            {
                return View(jsk);

            }
            return View();
        }
        public ActionResult DeleteJSK(string username)
        {
            Jobseeker jsk = context.Jobseekers.FirstOrDefault(x => x.username == username);
            if (jsk == null)
            {
                return View();
             
            }
            context.Jobseekers.DeleteOnSubmit(jsk);
            context.SubmitChanges();
            return RedirectToAction("ManageJobseeker", "Admin");
        }

    }
}