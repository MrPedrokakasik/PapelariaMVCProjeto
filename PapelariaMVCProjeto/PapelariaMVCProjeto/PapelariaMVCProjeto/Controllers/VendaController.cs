using PapelariaMVCProjeto;
using PapelariaMVCProjeto.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

[SessionAuthorize]
public class VendaController : Controller
{
    private Contexto db = new Contexto();

    public ActionResult Index()
    {
        var vendas = db.Vendas.Include(v => v.Produto).Include(v => v.Vendedor);
        return View(vendas.ToList());
    }

    public ActionResult Create()
    {
        ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome");
        ViewBag.VendedorId = new SelectList(db.Vendedores, "Id", "Nome");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Venda venda)
    {
        try
        {
            var produto = db.Produtos.Find(venda.ProdutoId);
            if (produto == null)
            {
                ModelState.AddModelError("", "Produto não encontrado.");
            }
            else if (produto.Quantidade >= venda.Quantidade)
            {
                produto.Quantidade -= venda.Quantidade;
                venda.Total = produto.Preco * venda.Quantidade;
                venda.Data = DateTime.Now;
                db.Vendas.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Estoque insuficiente!");
            }
        }
        catch (Exception)
        {
            return View("Error");
        }
        ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome", venda.ProdutoId);
        ViewBag.VendedorId = new SelectList(db.Vendedores, "Id", "Nome", venda.VendedorId);
        return View(venda);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Venda venda = db.Vendas.Include(v => v.Produto).Include(v => v.Vendedor).FirstOrDefault(v => v.Id == id);
        if (venda == null) return HttpNotFound();
        return View(venda);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            Venda venda = db.Vendas.Find(id);
            var produto = db.Produtos.Find(venda.ProdutoId);
            if (produto != null)
            {
                produto.Quantidade += venda.Quantidade;
            }
            db.Vendas.Remove(venda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }
}