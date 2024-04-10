/***
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
***/

using MVCClass.Models;
using MVCClass.Models.DTO;
using MVCClass.Models.DAO;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace MVCClass.Controllers
{
    public class AuthenticationController : Controller
    {
        private AuthenticationDAO authDAO = new AuthenticationDAO();

        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthenticationProcess(UserDTO user)
        {


            /*if (user.UserName == "x" && user.Password == "y")
            {
                return View("LoginSuccess", user);
            }
            return View("LoginFailure", user);*/

            try
            {
                authDAO.login(user);           
                return View("LoginSuccess", user);

            }
            catch
            {
                return View("LoginFailure", user);
            }

        }
    }
}
