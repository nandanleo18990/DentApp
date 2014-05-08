using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BCrypt.Net;

namespace DentAppSys.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RegAndLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegAndLogin(Models.RegAndLog User)
        {
            if (User.RegisterModel != null)
            {
                if (ModelState.IsValid)
                {
                    using (var db = new MaindbModelDataContext())
                    {
                        var Person = db.Patients.FirstOrDefault(u => u.Email == User.RegisterModel.Email);
                        if (Person == null)
                        {
                            string Hash = BCrypt.Net.BCrypt.HashPassword(User.RegisterModel.Password);
                            var MyUser = new Patient();
                            MyUser.Name = User.RegisterModel.Firstname;
                            MyUser.Surname = User.RegisterModel.Lastname;
                            MyUser.Birthday = User.RegisterModel.Birthday;
                            MyUser.Email = User.RegisterModel.Email;
                            MyUser.Password = Hash;
                            MyUser.PatientNo = Guid.NewGuid().GetHashCode();
                            db.Patients.InsertOnSubmit(MyUser);
                            db.SubmitChanges();

                            Session["UserEmail"] = User.RegisterModel.Email;
                            return RedirectToAction("Index", "Patient", User.RegisterModel);
                        }
                        else
                        {
                            ModelState.AddModelError("", "There is a user with this Email. Please enter another Email !!!");
                            return View();
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is incorrect !!!");
                }
            }
            else
            {
                if (ModelState.IsValid && IsValid(User.LoginModel.Email, User.LoginModel.Password))
                {
                    
                    var TempUser = new Models.RegisterModel();
                    Session["UserEmail"] = User.LoginModel.Email;
                    using (var db = new MaindbModelDataContext())
                    {
                        var person = db.Patients.FirstOrDefault(u => u.Email == User.LoginModel.Email);
                        TempUser.Firstname = person.Name;
                        TempUser.Lastname = person.Surname;
                        //TempUser.RegisterModel.Birthday = (DateTime)person.BirthDate;
                        TempUser.Email = person.Email;


                    }
                    return RedirectToAction("Index", "Patient", TempUser);
                 }
                else
                {
                    ModelState.AddModelError("", "Check your E-mail or Password then try again !!!");
                }
            }
            return View();
        }

        private bool IsValid(string email, string password)
        {
            bool isvalid = false;
            using (var db = new MaindbModelDataContext())
            {
                var Person = db.Patients.First(u => u.Email == email);
                if (Person != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, Person.Password))
                    {
                        isvalid = true;
                    }
                }
            }
            return isvalid;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();  
            return RedirectToAction("Index", "Home");
        }

    }
}
