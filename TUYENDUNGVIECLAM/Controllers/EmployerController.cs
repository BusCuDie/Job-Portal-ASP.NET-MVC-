using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class EmployerController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();

        // GET: Employer
        public ActionResult AllEmployer()
        {
            var ls = context.Employers.ToList();
            return View(ls);

        }
        public ActionResult JobPosted()
        {
            if (Session["userid"] != null)
            {
                string id = Session["userid"].ToString();
                var ls = context.Jobs.Where(x => x.employer_usename == id).OrderByDescending(x => x.job_postdate).ToList();
                return View(ls);
            }
            else
            {
                return RedirectToAction("Index", "Job");
            }
        }
        public ActionResult EmployerProfile(string id)
        {

            var em = context.Employers.FirstOrDefault(x => x.username == id);
            if (em != null)
            {
                return View(em);

            }
            return View();
        }
        public ActionResult EditProfile_Emp(string id)
        {

            Employer em = context.Employers.FirstOrDefault(x => x.username == id);
            if (Request.Form.Count == 0)
            {
                return View(em);
            }
            em.name = Request.Form["name"].Trim();
            em.aboutus = Request.Form["aboutus"].Trim();
            em.tel = Request.Form["tel"].Trim();
            em.location = Request.Form["location"].Trim();
            em.address = Request.Form["address"].Trim();
            em.webiste = Request.Form["webiste"].Trim();
            em.amount_employee = Request.Form["amount_employee"].Trim();
            em.contact_person = Request.Form["contact_person"].Trim();
            em.contact_phone = Request.Form["contact_phone"].Trim();

            Employer test = context.Employers.FirstOrDefault(x => x.email == em.email);
            if (String.IsNullOrEmpty(em.name) || String.IsNullOrEmpty(em.aboutus) ||
                String.IsNullOrEmpty(em.tel) ||  String.IsNullOrEmpty(em.address) || String.IsNullOrEmpty(em.webiste) ||
                String.IsNullOrEmpty(em.amount_employee) || String.IsNullOrEmpty(em.contact_person) ||
                String.IsNullOrEmpty(em.contact_phone))
            {
                ViewData["em_error"] = "THÔNG TIN KHÔNG ĐƯỢC ĐỂ TRỐNG,VUI LÒNG NHẬP ĐẦY ĐỦ";
            }
            else
            {

                
                context.SubmitChanges();
                return RedirectToAction("EmployerProfile", "Employer", new { id = Session["userid"].ToString()});
            }

            return View(em);
        }
        public ActionResult ChangePassword(string id)
        {
            Employer em = context.Employers.FirstOrDefault(x => x.username == id);
            if (Request.Form.Count > 0)
            {
                em.password = Request.Form["password"];
                string repass = Request.Form["repass"];

                if (String.IsNullOrEmpty(em.password) || String.IsNullOrEmpty(repass))
                {
                    ViewData["err_empty"] = "KHÔNG ĐƯỢC ĐỂ TRỐNG";



                }
                else if (!em.password.Equals(repass))
                {

                    ViewData["pass_err"] = "MẬT KHẨU KHÔNG KHỚP,VUI LÒNG THỬ LẠI";


                }
                else
                {

                    context.SubmitChanges();
                    return RedirectToAction("Index", "Job");
                }
            }
            return View(em);
        }
        public ActionResult ListApplyMyJob(int job_id)
        {
            ViewBag.ListApplyMyJob = context.ListApplyMyJob(job_id).ToList();
            return View();
        }
    }
}