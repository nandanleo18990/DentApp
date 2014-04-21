using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult RegAndLogin(Models.RegAndLog User)
        {
            if (ModelState.IsValid)
            {
                using(var db = new MaindbModelDataContext())
                {
                    var Person = from u in db.Patients
                                 where u.Email == User.RegisterModel.Email
                                 select u;

                    if (Person == null)
                    {
                        var Crypto = new SimpleCrypto.PBKDF2();
                        var CrypPass = Crypto.Compute(User.RegisterModel.Password);
                        var MyUser = new Patient();
                        MyUser.Name = User.RegisterModel.Firstname;
                        MyUser.Surname = User.RegisterModel.Lastname;
                        MyUser.Email = User.RegisterModel.Email;
                        MyUser.Password = CrypPass;
                        MyUser.PasswordSalt = Crypto.Salt;
                        MyUser.PatientNo = Guid.NewGuid().GetHashCode();
                        db.Patients.InsertOnSubmit(MyUser);
                        db.SubmitChanges();

                        return RedirectToAction("Index", "Home");
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
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }

    }
}
