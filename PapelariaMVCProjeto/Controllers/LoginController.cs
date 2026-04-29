using System;
using System.Linq;
using System.Web.Mvc;
using PapelariaMVCProjeto.Models;
using PapelariaMVCProjeto.Helpers;

namespace PapelariaMVCProjeto.Controllers
{
    public class LoginController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string usuario, string senha)
        {
            string senhaHash = HashHelper.GerarHash(senha);
            var user = db.Usuarios.FirstOrDefault(u => u.Login == usuario && u.Senha == senhaHash);
            if (user != null)
            {
                user.UltimoLogin = DateTime.Now;
                db.SaveChanges();

                Session["usuario"] = user.Login;
                Session["usuarioId"] = user.Id;
                Session.Timeout = 30; 
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}