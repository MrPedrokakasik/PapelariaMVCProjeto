namespace PapelariaMVCProjeto.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PapelariaMVCProjeto.Models;
    using PapelariaMVCProjeto.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto context)
        {
            context.Usuarios.AddOrUpdate(
                u => u.Login,
                new Usuario
                {
                    Login = "admin",
                    Senha = HashHelper.GerarHash("123456")
                }
            );

            context.SaveChanges();
        }
    }
}