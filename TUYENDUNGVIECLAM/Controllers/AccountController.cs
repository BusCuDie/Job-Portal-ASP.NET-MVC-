using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUYENDUNGVIECLAM.Models;
namespace TUYENDUNGVIECLAM.Controllers
{
    public class AccountController : Controller
    {
        TUYENDUNGDataContext context = new TUYENDUNGDataContext();
        // GET: Account
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection collection)
        {

            string username = collection["username"].Trim();
            string pass = collection["password"];
            if (String.IsNullOrEmpty(username))
            {
                ViewData["err_email"] = "Tên người dùng không được để trống";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["err_pass"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                String role = collection["role"];
                if (role.Equals("Quản trị viên"))
                {
                    Admin ad = context.Admins.Where(s => s.username == username && s.password == pass).FirstOrDefault();
                    if (ad != null)
                    {
                        ViewData["choose_role"] = "Bạn đang đăng nhập với tư cách nhà tuyển dụng";
                
                        Session["username"] = ad.username;
                        Session["userRole"] = "Admin";
                        return RedirectToAction("ListJob", "Admin");
                    }
                    else
                    {
                        ViewData["checklogin"] = "Email hoặc mật khẩu không chính xác";
                    }
                }
                else if (role.Equals("Nhà tuyển dụng"))
                {
                    Employer em = context.Employers.Where(s => s.username == username && s.password == pass).FirstOrDefault();
                    if (em != null)
                    {
                        ViewData["choose_role"] = "Bạn đang đăng nhập với tư cách nhà tuyển dụng";
                        Session["userid"] = em.username;
                        Session["username"] = em.name;
                        Session["userRole"] = "Employer";
                        return RedirectToAction("Index", "Job");
                    }
                    else
                    {
                        ViewData["checklogin"] = "Email hoặc mật khẩu không chính xác";
                    }
                }
                else if (role.Equals("Người tìm việc"))
                {
                    Jobseeker em = context.Jobseekers.Where(s => s.username == username && s.password == pass).FirstOrDefault();
                    if (em != null)
                    {
                        ViewData["choose_role"] = "Bạn đang đăng nhập với tư cách nhà tuyển dụng";
                        Session["userid"] = em.username;
                        Session["username"] = em.name;
                        Session["userRole"] = "Jobseeker";
                        return RedirectToAction("Index", "Job");
                    }
                    else
                    {
                        ViewData["checklogin"] = "Email hoặc mật khẩu không chính xác";
                    }
                }


            }


            return View();
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Job");
        }
        public ActionResult EmployerSignUp()
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
                else if (test !=null && !test.email.Equals(em.email))
                {
                  
                    ViewData["em_email"] = "EMAIL NGƯỜI DÙNG ĐÃ TỒN TẠI, VUI LÒNG NHẬP EMAIL KHÁC";
                }
                else if (!em.password.Equals(RenterPass))
                {
                    ViewData["em_err_pass"] = "MẬT KHẨU KHÔNG TRÙNG KHỚP";
                }
                else
                {

                    context.Employers.InsertOnSubmit(em);
                    context.SubmitChanges();
                    return RedirectToAction("login", "Account");
                }


            }

            return View(em);
        }
        public ActionResult Jobseeker_signup()
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
                    return RedirectToAction("login", "Account");
                }

            }
            return View(jobseeker);

        }
    }
}