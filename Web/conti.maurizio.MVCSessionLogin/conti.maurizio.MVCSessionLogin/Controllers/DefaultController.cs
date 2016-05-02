using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using conti.maurizio.MVCSessionLogin.Models;

namespace conti.maurizio.MVCSessionLogin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            // Session è una variabile che viene mantenuta valorizzata di pagina in pagina
            // Usa i Cookies in modo automatico per rendere StateFul il protocollo HTTP !
            if (Session["Utente"] != null)
            {
                // Regola (inventata da noi) 
                // Se qualcuno prima di noi ha valorizzato Session["Utente"] ...
                // Possiamo dire che l'Utente è autenticato

                // Quindi facciamo quello che abbiamo sempre fatto
                return View();
            }

            // Novità!
            // Ridirezionamo il flusso verso la Action "Login" del Controller "Default"
            return RedirectToAction("Login", "Default");
        }

        // Novita: Le annotazioni
        [HttpGet]
        public ActionResult Login()
        {
            // Questa action non andrebb ein realtà annotata...
            // Il verbo di default infatti è HTTP GET
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Utente u)
        {
            // Questa versione di action Login risponde al verbo HTTP POST
            // Nota1: POST è il verbo usato di default dalle View (lo si può modificare dentro a @Html.BeginForm()
            // Nota2: La View sul "Submit" ci passa un oggetto di quelli che abbiamo definito in cima al sorgente con @model
            if (u != null)
            {
                // Qui la verifica andrebbe fatta usando un DB
                if (u.Email == "123")
                    Session["Utente"] = u;

                // Ecco un'altro redirect che ci riporta sul flusso originale della Index...
                return RedirectToAction("Index", "Default");
            }

            // Se cadi qui... richiami la tua view quindi rimani nella pagina di Login!!
            return View();
        }
    }
}