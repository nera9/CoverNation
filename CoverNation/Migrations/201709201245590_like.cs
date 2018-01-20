namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class like : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CoverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Covers", t => t.CoverId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CoverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "CoverId", "dbo.Covers");
            DropIndex("dbo.Likes", new[] { "CoverId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Likes");
        }
    }
}
