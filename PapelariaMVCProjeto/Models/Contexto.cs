using PapelariaMVCProjeto.Models;
using System.Data.Entity;

public class Contexto : DbContext
{
    public Contexto() : base("PapelariaDB") { }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}