namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likeRaiting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Likes", "Raiting", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Likes", "Raiting");
        }
    }
}
