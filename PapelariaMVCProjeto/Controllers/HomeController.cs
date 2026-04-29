using PapelariaMVCProjeto.Models;
using System.Linq;
using System.Web.Mvc;
using PapelariaMVCProjeto;

namespace PapelariaMVCProjeto.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            ViewBag.TotalProdutos = db.Produtos.Count();
            ViewBag.TotalVendas = db.Vendas.Count();
            ViewBag.TotalVendedores = db.Vendedores.Count();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}