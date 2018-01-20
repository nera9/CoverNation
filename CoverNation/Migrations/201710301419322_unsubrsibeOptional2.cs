namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unsubrsibeOptional2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subscribers", "DateOfUnsubscription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscribers", "DateOfUnsubscription", c => c.DateTime());
        }
    }
}
