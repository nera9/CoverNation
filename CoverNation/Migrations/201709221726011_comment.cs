namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CoverId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentText = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Covers", t => t.CoverId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CoverId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "CoverId", "dbo.Covers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "CoverId" });
            DropTable("dbo.Comments");
        }
    }
}
