namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nekoIme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrators", "nesto2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Administrators", "nesto2");
        }
    }
}
