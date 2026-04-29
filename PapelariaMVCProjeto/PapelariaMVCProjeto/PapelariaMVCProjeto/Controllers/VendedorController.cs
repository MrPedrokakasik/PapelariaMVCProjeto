using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PapelariaMVCProjeto.Models;

public class VendedorController : Controller
{
    private Contexto db = new Contexto();

    public ActionResult Index()
    {
        return View(db.Vendedores.ToList());
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Vendedor vendedor)
    {
        try
        {
            if (db.Vendedores.Count() >= 4)
            {
                ModelState.AddModelError("", "Máximo de 4 vendedores atingido!");
                return View(vendedor);
            }
            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        catch (Exception)
        {
            return View("Error");
        }
        return View(vendedor);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Vendedor vendedor = db.Vendedores.Find(id);
        if (vendedor == null) return HttpNotFound();
        return View(vendedor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            Vendedor vendedor = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }
}