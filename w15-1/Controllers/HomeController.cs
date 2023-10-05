using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using w15_1.Models;

namespace w15_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clienti()
        {
            return View(Spedizioni.spedizioni);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utenti u)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select * from Login where Username=@Username and Password=@password", conn);
             SqlDataReader sqlreader;
      
            conn.Open();
        
            cmd1.Parameters.AddWithValue("Username",u.Username) ;
            cmd1.Parameters.AddWithValue("password", u.Password);

               sqlreader = cmd1.ExecuteReader();
            if (sqlreader.HasRows)
            {
                FormsAuthentication.SetAuthCookie(u.Username,false);
            }
            else
            {
                ViewBag.ErrorMessage = "Autentificazione non riuscita";
                return RedirectToAction("Login", "Home");
            }
            conn.Close();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
           
            return  RedirectToAction("Index","Home");
        }

        public ActionResult CercaSpedizione()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CercaSpedizione(Spedizioni S)
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.SelectSpedizione(S);

            return  RedirectToAction ("Clienti","Home");
        }

        public ActionResult SelectAggiornamento()
        {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.SelectAggiornamentiWhereId();
            return View(Aggiornamenti.ListAggiornamenti);
        }
  
    }
}