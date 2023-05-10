using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_viewmodel;
using System.Net.Mail;
using System.Net;
using PMO_Monitoring.viewmodel;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace PMO_Monitoring.Controllers
{
    public class LoginPageController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly LoginPageRepo loginPageRepo;
        public LoginPageController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.loginPageRepo = new LoginPageRepo(db);
        }
            public IActionResult Index()
        {
            return View();
        }
        public JsonResult LogIn(VMMstUser dataView)
        {
            VMResponse respon = loginPageRepo.SignIn(dataView);
            if (respon.Success)
            {
                VMMstUser user = (VMMstUser)respon.Entity;

                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Name", user.Nama);
                HttpContext.Session.SetInt32("IdUser", user.Id);
                HttpContext.Session.SetInt32("IdRole", user.Role);
                HttpContext.Session.SetInt32("IdDivisi", user.Divisi);


            }
            return Json(respon);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LoginPage");
        }
        public JsonResult CheckEmail(string Email)
        {
            bool data = loginPageRepo.CheckEmail(Email);
            return Json(data);

        }
        public IActionResult SendOTP()
        {
            return View();
        }
        public IActionResult GetOTP(VMMstUser dataView)
        {
            VMMstUser data = loginPageRepo.VerifikasiEmail(dataView);
            Random rnd = new Random();
            int otp = rnd.Next(1000, 9999);
            ViewData["msgotp"] = otp;
            data.Token = otp.ToString();

            
            string msg = "your otp from Admin KBI is " + otp;
            bool f = SendEmail("projectxaa@gmail.com", data.Email, "Subjected to OTP", msg, data.Username);
            if (f)
            {
                return Json(new { status = "success", data = data });
            }
            else
            {
                return Json(new { status = "error" });
            }
            return View();
        }
        public bool SendEmail(string from, string to, string subject, string body, string name)
        {
            bool f = false;
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.EnableSsl = true;

                MailAddress dari = new MailAddress(from, "Admin KBI", System.Text.Encoding.UTF8);
                MailAddress untuk = new MailAddress(to, name);
                MailMessage message = new MailMessage(dari, untuk);
                message.Subject = subject;
                message.Body = body;
                NetworkCredential myCreds = new NetworkCredential(from, "gpvhqokzruxqcfff", "");

                client.Credentials = myCreds;
                var userState = "test1";
                client.SendAsync(message, userState);
                f = true;
            }
            catch (Exception e)
            {
                f = false;
            }
            return f;
        }
        public IActionResult InputOTP(VMMstUser data)
        {
            return View(data);
        }
        public IActionResult SetPassword(VMMstUser data)
        {
            return View(data);
        }
        [HttpPost]
        public JsonResult NewPassword(VMMstUser dataView)
        {
            VMResponse data = loginPageRepo.SetPassword(dataView);
            return Json(data);
        }
        public List<VMMstMenu> GetMenu(int? IdRole, int? IdUser)
        {
            List<VMMstMenu> data = loginPageRepo.GetMenu(IdRole, IdUser);
            return data;
        }
    }
}
