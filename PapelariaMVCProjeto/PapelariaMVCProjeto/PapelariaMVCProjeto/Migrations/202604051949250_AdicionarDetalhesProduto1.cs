namespace PapelariaMVCProjeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarDetalhesProduto1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtoes", "Descricao", c => c.String());
            AddColumn("dbo.Produtoes", "Tamanho", c => c.String());
            AddColumn("dbo.Produtoes", "Peso", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtoes", "Peso");
            DropColumn("dbo.Produtoes", "Tamanho");
            DropColumn("dbo.Produtoes", "Descricao");
        }
    }
}
