namespace PapelariaMVCProjeto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Tamanho { get; set; }
        public string Peso { get; set; }
        public string Imagem { get; set; }
    }
}