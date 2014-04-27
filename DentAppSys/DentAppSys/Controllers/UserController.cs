using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Security.Cryptography;

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
                            var crypto = new SimpleCrypto.PBKDF2();
                            var CrypPass = crypto.Compute(User.RegisterModel.Password);
                            var MyUser = new Patient();
                            MyUser.Name = User.RegisterModel.Firstname;
                            MyUser.Surname = User.RegisterModel.Lastname;
                            MyUser.Birthday = User.RegisterModel.Birthday;
                            MyUser.Email = User.RegisterModel.Email;
                            MyUser.Password = CrypPass;
                            MyUser.PasswordSalt = crypto.Salt;
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

                    Session["UserEmail"] = User.LoginModel.Email;
                    using (var db = new MaindbModelDataContext())
                    {
                        var person = db.Patients.FirstOrDefault(u => u.Email == User.LoginModel.Email);
                        User.RegisterModel.Firstname = person.Name;
                        User.RegisterModel.Lastname = person.Surname;
                        //User.RegisterModel.Birthday = (DateTime)person.BirthDate;
                        User.RegisterModel.Email = person.Email;


                    }
                    return RedirectToAction("Index", "Patient", User.RegisterModel);


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
            var crypto = new SimpleCrypto.PBKDF2();
            using (var db = new MaindbModelDataContext())
            {
                var Person = db.Patients.First(u => u.Email == email);
                if (Person != null)
                {
                    var temp = crypto.Compute(password, Person.PasswordSalt);
                    if (Person.Password == password)
                    {
                        isvalid = true;
                    }
                }
            }
            return isvalid;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            
            return RedirectToAction("Index", "Home");
        }

    }
}
