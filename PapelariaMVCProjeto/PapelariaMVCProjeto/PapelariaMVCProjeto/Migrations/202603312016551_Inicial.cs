namespace PapelariaMVCProjeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        VendedorId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .ForeignKey("dbo.Vendedors", t => t.VendedorId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.VendedorId);
            
            CreateTable(
                "dbo.Vendedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "VendedorId", "dbo.Vendedors");
            DropForeignKey("dbo.Vendas", "ProdutoId", "dbo.Produtoes");
            DropIndex("dbo.Vendas", new[] { "VendedorId" });
            DropIndex("dbo.Vendas", new[] { "ProdutoId" });
            DropTable("dbo.Vendedors");
            DropTable("dbo.Vendas");
            DropTable("dbo.Produtoes");
        }
    }
}
