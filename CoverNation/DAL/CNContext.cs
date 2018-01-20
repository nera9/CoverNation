using CoverNation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CoverNation.DAL
{
    public class CNContext:DbContext
    {
        public CNContext():base("name=CNCS")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<BasicUser> BasicUsers { get; set; }
        public DbSet<MusicianEducation> Musician_Educations { get; set; }
        public DbSet<MusicianGenre> Musician_Genre { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<CoversToPlaylist> CoversToPlayLists { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<PreferredGenre> PreferredGenres { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Instrument> Instruments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        }
    }
}