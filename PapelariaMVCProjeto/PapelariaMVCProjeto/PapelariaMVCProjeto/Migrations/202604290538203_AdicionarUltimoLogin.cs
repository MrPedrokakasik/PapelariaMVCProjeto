namespace PapelariaMVCProjeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarUltimoLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "UltimoLogin", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "UltimoLogin");
        }
    }
}
