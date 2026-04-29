using System;

namespace PapelariaMVCProjeto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime? UltimoLogin { get; set; }
    }
}