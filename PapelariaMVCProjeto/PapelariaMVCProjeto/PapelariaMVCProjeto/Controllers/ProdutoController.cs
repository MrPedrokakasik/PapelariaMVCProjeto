using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PapelariaMVCProjeto.Models;
using PapelariaMVCProjeto;

[SessionAuthorize]
public class ProdutoController : Controller
{
    private Contexto db = new Contexto();

    // LISTAR
    public ActionResult Index()
    {
        return View(db.Produtos.ToList());
    }

    // DETALHES
    public ActionResult Details(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Produto produto = db.Produtos.Find(id);
        if (produto == null) return HttpNotFound();
        return View(produto);
    }

    // CREATE (GET)
    public ActionResult Create()
    {
        return View();
    }

    // CREATE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Produto produto, HttpPostedFileBase imagemFile)
    {
        try
        {
            if (imagemFile != null && imagemFile.ContentLength > 0)
            {
                string[] extensoesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string extensao = Path.GetExtension(imagemFile.FileName).ToLower();

                if (!Array.Exists(extensoesPermitidas, e => e == extensao))
                {
                    ModelState.AddModelError("", "Formato de imagem inválido. Use JPG, PNG ou GIF.");
                    return View(produto);
                }

                if (imagemFile.ContentLength > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "A imagem deve ter no máximo 5MB.");
                    return View(produto);
                }

                string pasta = AppDomain.CurrentDomain.BaseDirectory + "Content\\Imagens\\";
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                string nomeArquivo = Guid.NewGuid().ToString() + extensao;
                imagemFile.SaveAs(Path.Combine(pasta, nomeArquivo));
                produto.Imagem = nomeArquivo;
            }

            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return View("Error");
        }
        return View(produto);
    }

    // EDIT (GET)
    public ActionResult Edit(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Produto produto = db.Produtos.Find(id);
        if (produto == null) return HttpNotFound();
        return View(produto);
    }

    // EDIT (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Produto produto, HttpPostedFileBase imagemFile)
    {
        try
        {
            if (imagemFile != null && imagemFile.ContentLength > 0)
            {
                string[] extensoesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string extensao = Path.GetExtension(imagemFile.FileName).ToLower();

                if (!Array.Exists(extensoesPermitidas, e => e == extensao))
                {
                    ModelState.AddModelError("", "Formato de imagem inválido. Use JPG, PNG ou GIF.");
                    return View(produto);
                }

                string pasta = AppDomain.CurrentDomain.BaseDirectory + "Content\\Imagens\\";
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                if (!string.IsNullOrEmpty(produto.Imagem))
                {
                    string caminhoAntigo = Path.Combine(pasta, produto.Imagem);
                    if (System.IO.File.Exists(caminhoAntigo))
                        System.IO.File.Delete(caminhoAntigo);
                }

                string nomeArquivo = Guid.NewGuid().ToString() + extensao;
                imagemFile.SaveAs(Path.Combine(pasta, nomeArquivo));
                produto.Imagem = nomeArquivo;
            }

            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return View("Error");
        }
        return View(produto);
    }

    // DELETE (GET)
    public ActionResult Delete(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        Produto produto = db.Produtos.Find(id);
        if (produto == null) return HttpNotFound();
        return View(produto);
    }

    // DELETE (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            Produto produto = db.Produtos.Find(id);

            if (!string.IsNullOrEmpty(produto.Imagem))
            {
                string pasta = AppDomain.CurrentDomain.BaseDirectory + "Content\\Imagens\\";
                string caminho = Path.Combine(pasta, produto.Imagem);
                if (System.IO.File.Exists(caminho))
                    System.IO.File.Delete(caminho);
            }

            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return View("Error");
        }
    }
}