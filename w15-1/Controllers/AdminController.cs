using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w15_1.Models;

namespace w15_1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }

        //Create

        public ActionResult RegistraCliente()
        {

            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistraCliente([Bind(Include = "Nome, Indirizzo, CodiceFiscale, PartitaIva, Città, TipoCliente")] Clienti c)
        {
            if (ModelState.IsValid)
            {
                Clienti.Insert(c, ViewBag.messaggio = "Errore");
            }
            return RedirectToAction("SelectClienti", "Admin");
        }
      
        public ActionResult RegistraSpedizione()
        {
            Spedizioni.ListClienti.Clear();
            Spedizioni.DropdownClienti.Clear();
            Spedizioni.Dropdown1();
            ViewBag.drop1 = Spedizioni.DropdownClienti;
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistraSpedizione([Bind(Include = "Cliente,DataSpedizione,Peso,Città,Indirizzo,NominativoDestinatario,CostoSpedizione,DataConsegna")] Spedizioni s, int Cliente)
        {
            Spedizioni.ListClienti.Clear();
            Spedizioni.DropdownClienti.Clear();
            Spedizioni.Dropdown1();
            ViewBag.drop1 = Spedizioni.DropdownClienti;
            if (ModelState.IsValid)
            {
                Spedizioni.Insert(s, ViewBag.messaggio = "Errore", Cliente);
            }
            return RedirectToAction("SelectSpedioni", "Admin");
        }
      
        public ActionResult RegistraAggiornamenti()
        {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.DropdownAggiornamenti.Clear();
            Aggiornamenti.Dropdown();
            ViewBag.drop1 = Aggiornamenti.DropdownAggiornamenti;
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistraAggiornamenti(Aggiornamenti a, int Stato)
        {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.DropdownAggiornamenti.Clear();
            Aggiornamenti.Dropdown();
            ViewBag.drop1 = Aggiornamenti.DropdownAggiornamenti;
            if (ModelState.IsValid)
            {
                Aggiornamenti.Insert(a, ViewBag.messaggio = "Errore", Stato);
            }
            return RedirectToAction("SelectAggiornamenti", "Admin");
        }

        //Select tabelle DB
     
        public ActionResult SelectSpedioni()
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.Select();

            return View(Spedizioni.spedizioni);
        }
        public ActionResult SelectAggiornamenti()
        {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.SelectAggiornamenti();
            return View(Aggiornamenti.ListAggiornamenti);
        }
        public ActionResult SelectClienti()
        {
            Clienti.ListClienti.Clear();
            Clienti.Select();
            return View(Clienti.ListClienti);
        }
        public ActionResult SelectAziende()
        {
            Clienti.ListClienti.Clear();
            Clienti.Select();
            return View(Clienti.ListClienti);
        }
        public ActionResult SelectAggiornamentowhereid() {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.SelectAggiornamentiWhereId();
            return View(Aggiornamenti.ListAggiornamenti);
        }
        public ActionResult SelectDettagli(int Id)
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.SelectId(Id);
            return PartialView(Spedizioni.spedizioni);

        }

        //Delete
        public ActionResult DeleteSpedizioni()
        {
            Spedizioni.Elimina();
            return RedirectToAction("SelectSpedioni", "Admin");
        }
        public ActionResult DeleteAggiornamenti()
        {
            Aggiornamenti.Elimina();
            return RedirectToAction("SelectAggiornamenti", "Admin");
        }
        public ActionResult DeleteClienti()
        {
            Clienti.Elimina();
            return RedirectToAction("SelectClienti", "Admin");
        }

        //Edit
       
        public ActionResult EditSpedizioni(int id)
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.DropdownClienti.Clear();
            Spedizioni.Dropdown2(id);
            ViewBag.drop1 = Spedizioni.DropdownClienti;
            Spedizioni prod = new Spedizioni();
            Spedizioni.SelectWhereId(id);
            foreach (Spedizioni item in Spedizioni.spedizioni)
            {


                if (item.IdSpedizione == id)
                {

                    prod = item;
                    break;

                }

            }
            return View(prod);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSpedizioni(Spedizioni s)
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.DropdownClienti.Clear();
            Spedizioni.Dropdown2(s.IdSpedizione);
            ViewBag.drop1 = Spedizioni.DropdownClienti;
            Spedizioni.Modifica(s);
            return RedirectToAction("SelectSpedizioni", "Admin");
        }
       
        public ActionResult EditClienti(int id)
        {
           
            Clienti prod = new Clienti();
            Clienti.SelectWhereId(id);
            foreach (Clienti item in Clienti.ListClienti)
            {


                if (item.IdCliente == id)
                {

                    prod = item;
                    break;

                }

            }
            return View(prod);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditClienti(Clienti c,int Id)
        {
          
            Clienti.SelectWhereId(Id);
            Clienti prod = new Clienti();
            foreach (Clienti item in Clienti.ListClienti)
            {
                //   Prodotto.SelectWhereId();
                if (item.IdCliente == Id)
                {
                    Clienti.Modifica(c);
                }
            }
            return RedirectToAction("SelectClienti", "Admin");
        }
      
        public ActionResult EditAggiornamenti(int id)
        {
            Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.DropdownAggiornamenti.Clear();
            Aggiornamenti.Dropdown();
            ViewBag.drop1 = Aggiornamenti.DropdownAggiornamenti;
            Aggiornamenti prod = new Aggiornamenti();
            Aggiornamenti.SelectAggiornamentiWhereId();
            foreach (Aggiornamenti item in Aggiornamenti.ListAggiornamenti)
            {

                if (item.IdAggiornamento == id)
                {

                    prod = item;
                    break;

                }

            }
            return View(prod);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAggiornamenti(Aggiornamenti c, int Id, int Stato)
        {
            //Aggiornamenti.ListAggiornamenti.Clear();
            Aggiornamenti.DropdownAggiornamenti.Clear();
            Aggiornamenti.Dropdown();
            ViewBag.drop1 = Aggiornamenti.DropdownAggiornamenti;
            Aggiornamenti.SelectAggiornamentiWhereId();
            Aggiornamenti prod = new Aggiornamenti();
            foreach (Aggiornamenti item in Aggiornamenti.ListAggiornamenti)
            {
                if (item.IdAggiornamento == Id)
                {
                    Aggiornamenti.Modifica(c,Stato);
                }
            }
            return RedirectToAction("SelectAggiornamenti", "Admin");
        }

        //Chiamate assincrone
        public ActionResult Query()
        {
            return View();
        }
        public JsonResult SelectData()
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.SelectData();
            return Json(Spedizioni.spedizioni,JsonRequestBehavior.AllowGet);
        }
        public JsonResult SelectTotConsegna()
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.SelectTotConsegna();
            return Json(Spedizioni.spedizioni, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SelectTotCittà()
        {
            Spedizioni.spedizioni.Clear();
            Spedizioni.SelectTotConsegnaCittà();
            return Json(Spedizioni.spedizioni, JsonRequestBehavior.AllowGet);
        }

    }
}