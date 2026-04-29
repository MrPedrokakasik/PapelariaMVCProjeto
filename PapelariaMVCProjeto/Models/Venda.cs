using System;

namespace PapelariaMVCProjeto.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int VendedorId { get; set; }
        public virtual Vendedor Vendedor { get; set; }

        public int Quantidade { get; set; }
        public decimal Total { get; set; }
    }
}