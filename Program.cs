using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuncoastMovies
{

    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public long ContactPhoneNumber { get; set; }
    }
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
    class Song
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Duraction { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
    // Define a database context for our RGGY database.
    // It derives from (has a parent of) DbContext so we get all the
    // abilities of a database context from EF Core.
    class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYouDatabase");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var keepGoing = true;
            Console.WriteLine("Welcome to Victory Music Company");

            while (keepGoing)
            {
                Console.WriteLine("Do you want to (D)o Something or (Q)uit?");
                var menuResponse = Console.ReadLine().ToUpper();

                switch (menuResponse)
                {
                    case "Q":
                        Console.WriteLine("See ya!");
                        keepGoing = false;
                        break;
                    case "D":
                        Console.WriteLine("okie dokie");
                        break;
                    default:
                        break;
                }

            }

        }
    }
}
