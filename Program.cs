using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuncoastMovies
{
    // Make band class
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
    // Make album class
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
    // Make song class
    class Song
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Duraction { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
    // Connect c# code to database
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
                Console.WriteLine();
                Console.WriteLine("What would you like to do:");
                Console.WriteLine("[A] View all bands");
                Console.WriteLine("[B] View signed bands");
                Console.WriteLine("[C] View unsigned bands");
                Console.WriteLine("[D] View albums for a band");
                Console.WriteLine("[E] View albums by release date");
                Console.WriteLine("[F] View songs for an album");
                Console.WriteLine("[G] Add a band");
                Console.WriteLine("[H] Add an album");
                Console.WriteLine("[I] Add a song");
                Console.WriteLine("[K] Let a band go");
                Console.WriteLine("[L] Resign a band");
                Console.WriteLine("[Q]uit");
                Console.WriteLine("[Z] delete this line later");
                var menuResponse = Console.ReadLine().ToUpper();

                switch (menuResponse)
                {
                    case "Q":
                        Console.WriteLine("See ya!");
                        keepGoing = false;
                        break;
                    case "Z":
                        Console.WriteLine("okie dokie");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Sorry, I don't understand. Please try again.");
                        break;
                }

            }

        }
    }
}
