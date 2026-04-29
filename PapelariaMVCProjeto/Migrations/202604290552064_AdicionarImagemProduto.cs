namespace PapelariaMVCProjeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarImagemProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produtoes", "Imagem", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produtoes", "Imagem");
        }
    }
}
