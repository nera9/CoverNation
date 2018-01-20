namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unsubrsibeOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscribers", "DateOfUnsubscription", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscribers", "DateOfUnsubscription", c => c.DateTime(nullable: false));
        }
    }
}
