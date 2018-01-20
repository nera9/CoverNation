namespace CoverNation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nestio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        ProfilePictureURL = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageURL = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        SubscriberId = c.Int(nullable: false, identity: true),
                        BasicUserId = c.Int(nullable: false),
                        MusicianId = c.Int(nullable: false),
                        DateOfSubscription = c.DateTime(nullable: false),
                        DateOfUnsubscription = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriberId)
                .ForeignKey("dbo.BasicUsers", t => t.BasicUserId)
                .ForeignKey("dbo.Musicians", t => t.MusicianId)
                .Index(t => t.BasicUserId)
                .Index(t => t.MusicianId);
            
            CreateTable(
                "dbo.MusicianEducations",
                c => new
                    {
                        MusicianEducationId = c.Int(nullable: false, identity: true),
                        MusicianId = c.Int(nullable: false),
                        DateOfLearning = c.DateTime(nullable: false),
                        Instrument = c.String(),
                        InstitutionName = c.String(),
                        DetailsAboutEducation = c.String(),
                    })
                .PrimaryKey(t => t.MusicianEducationId)
                .ForeignKey("dbo.Musicians", t => t.MusicianId)
                .Index(t => t.MusicianId);
            
            CreateTable(
                "dbo.MusicianGenres",
                c => new
                    {
                        MusicianGenreId = c.Int(nullable: false, identity: true),
                        MusicianId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MusicianGenreId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Musicians", t => t.MusicianId)
                .Index(t => t.MusicianId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                        GenreImage = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        CoverId = c.Int(nullable: false, identity: true),
                        CoverName = c.String(),
                        DateOfPosting = c.DateTime(nullable: false),
                        VideoURL = c.String(),
                        Thumbnail = c.String(),
                        GenreId = c.Int(nullable: false),
                        MusicianId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoverId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Musicians", t => t.MusicianId)
                .Index(t => t.GenreId)
                .Index(t => t.MusicianId);
            
            CreateTable(
                "dbo.CoversToPlaylists",
                c => new
                    {
                        CoversToPlaylistId = c.Int(nullable: false, identity: true),
                        CoverId = c.Int(nullable: false),
                        PlaylistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoversToPlaylistId)
                .ForeignKey("dbo.Covers", t => t.CoverId)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId)
                .Index(t => t.CoverId)
                .Index(t => t.PlaylistId);
            
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        NumOfCovers = c.Int(nullable: false),
                        DateOfCreating = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaylistId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PreferredGenres",
                c => new
                    {
                        PreferredGenreId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PreferredGenreId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        temp = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BasicUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PersonalInfo = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Musicians",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Musicians", "UserId", "dbo.Users");
            DropForeignKey("dbo.BasicUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Administrators", "UserId", "dbo.Users");
            DropForeignKey("dbo.PreferredGenres", "UserId", "dbo.Users");
            DropForeignKey("dbo.PreferredGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.CoversToPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.Playlists", "UserId", "dbo.Users");
            DropForeignKey("dbo.CoversToPlaylists", "CoverId", "dbo.Covers");
            DropForeignKey("dbo.Covers", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.Covers", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Artists", "UserId", "dbo.Users");
            DropForeignKey("dbo.Subscribers", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.MusicianGenres", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.MusicianGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.MusicianEducations", "MusicianId", "dbo.Musicians");
            DropForeignKey("dbo.Subscribers", "BasicUserId", "dbo.BasicUsers");
            DropIndex("dbo.Musicians", new[] { "UserId" });
            DropIndex("dbo.BasicUsers", new[] { "UserId" });
            DropIndex("dbo.Administrators", new[] { "UserId" });
            DropIndex("dbo.PreferredGenres", new[] { "GenreId" });
            DropIndex("dbo.PreferredGenres", new[] { "UserId" });
            DropIndex("dbo.Playlists", new[] { "UserId" });
            DropIndex("dbo.CoversToPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.CoversToPlaylists", new[] { "CoverId" });
            DropIndex("dbo.Covers", new[] { "MusicianId" });
            DropIndex("dbo.Covers", new[] { "GenreId" });
            DropIndex("dbo.MusicianGenres", new[] { "GenreId" });
            DropIndex("dbo.MusicianGenres", new[] { "MusicianId" });
            DropIndex("dbo.MusicianEducations", new[] { "MusicianId" });
            DropIndex("dbo.Subscribers", new[] { "MusicianId" });
            DropIndex("dbo.Subscribers", new[] { "BasicUserId" });
            DropIndex("dbo.Artists", new[] { "UserId" });
            DropTable("dbo.Musicians");
            DropTable("dbo.BasicUsers");
            DropTable("dbo.Administrators");
            DropTable("dbo.PreferredGenres");
            DropTable("dbo.Playlists");
            DropTable("dbo.CoversToPlaylists");
            DropTable("dbo.Covers");
            DropTable("dbo.Genres");
            DropTable("dbo.MusicianGenres");
            DropTable("dbo.MusicianEducations");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Artists");
            DropTable("dbo.Users");
        }
    }
}
