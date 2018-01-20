namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class instrument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        InstrumentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PictureURL = c.String(),
                    })
                .PrimaryKey(t => t.InstrumentId);
            
            AddColumn("dbo.MusicianEducations", "InstrumentId", c => c.Int(nullable: false));
            CreateIndex("dbo.MusicianEducations", "InstrumentId");
            AddForeignKey("dbo.MusicianEducations", "InstrumentId", "dbo.Instruments", "InstrumentId");
            DropColumn("dbo.MusicianEducations", "Instrument");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicianEducations", "Instrument", c => c.String());
            DropForeignKey("dbo.MusicianEducations", "InstrumentId", "dbo.Instruments");
            DropIndex("dbo.MusicianEducations", new[] { "InstrumentId" });
            DropColumn("dbo.MusicianEducations", "InstrumentId");
            DropTable("dbo.Instruments");
        }
    }
}
