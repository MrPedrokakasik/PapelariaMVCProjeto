using System;
using System.Linq;
using System.Web.Mvc;
using PapelariaMVCProjeto.Models;
using PapelariaMVCProjeto.Helpers;
using PapelariaMVCProjeto;

[SessionAuthorize]
public class UsuarioController : Controller
{
    private Contexto db = new Contexto();

    public ActionResult Index()
    {
        return View(db.Usuarios.ToList());
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Usuario usuario)
    {
        try
        {
            if (db.Usuarios.Any(u => u.Login == usuario.Login))
            {
                ModelState.AddModelError("", "Este login já está em uso.");
                return View(usuario);
            }
            if (ModelState.IsValid)
            {
                usuario.Senha = HashHelper.GerarHash(usuario.Senha);
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        catch (Exception)
        {
            return View("Error");
        }
        return View(usuario);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null) return new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        Usuario usuario = db.Usuarios.Find(id);
        if (usuario == null) return HttpNotFound();
        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }
}