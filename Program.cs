using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
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
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
    // Make musician class
    class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instrument { get; set; }
    }
    // make bandmusician class (connecting musicians to bands)
    class BandMusician
    {
        public int Id { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
        public int MusicianId { get; set; }
        public Musician Musician { get; set; }
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
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<BandMusician> BandMusicians { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYouDatabase");
        }
    }
    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);

            return Console.ReadLine();

        }
        static long PromptForLong(string prompt)
        {
            Console.Write(prompt);
            long userInput;
            var isThisLong = long.TryParse(Console.ReadLine(), out userInput);
            if (isThisLong)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Not valid number. Entering 0.");
                return 0;
            }
        }
        static DateTime PromptForDate(string prompt)
        {
            Console.Write(prompt);
            DateTime userInput;
            var isThisDate = DateTime.TryParse(Console.ReadLine(), out userInput);
            if (isThisDate)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, I don't understand. Entering January 1, 2000.");
                return DateTime.Parse("2000-01-01");
            }
        }
        static TimeSpan PromptForTime(string prompt)
        {
            Console.Write(prompt);
            string inputTime = "00:" + Console.ReadLine();
            TimeSpan userInput;
            var isThisTime = TimeSpan.TryParse(inputTime, out userInput);
            if (isThisTime)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, I don't understand. Entering 1 minute.");
                return TimeSpan.Parse("00:01:00");
            }
        }
        static int PromptForInt(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisInteger = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isThisInteger)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Not a number. Entering 1.");
                return 1;
            }
        }
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();
            var bands = context.Bands;
            var albums = context.Albums.Include(a => a.Band);
            var songs = context.Songs.Include(s => s.Album);
            var musicians = context.Musicians;
            var bandMusicians = context.BandMusicians.Include(b => b.Band).Include(m => m.Musician);

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
                Console.WriteLine("[J] Let a band go");
                Console.WriteLine("[K] Resign a band");
                Console.WriteLine("[L] View members of a band");
                Console.WriteLine("[M] View albums in a genre");
                //Console.WriteLine("[N] Add musician to band");
                Console.WriteLine("[Q]uit");
                var menuResponse = Console.ReadLine().ToUpper();
                Console.WriteLine();

                switch (menuResponse)
                {
                    case "Q":
                        Console.WriteLine("See ya!");
                        keepGoing = false;
                        break;
                    case "A":
                        foreach (var band in bands)
                        {
                            Console.WriteLine($"{band.Name}");
                            Console.WriteLine($"   From {band.CountryOfOrigin}; {band.NumberOfMembers} members; {band.Style} style; website: {band.Website}; contact is {band.ContactName} at {band.ContactPhoneNumber}");
                        }
                        if (bands.Count() == 0)
                        {
                            Console.WriteLine("No bands");
                        }
                        break;
                    case "B":
                        foreach (var band in bands)
                        {
                            if (band.IsSigned == true)
                            {
                                Console.WriteLine($"{band.Name}");
                                Console.WriteLine($"   From {band.CountryOfOrigin}; {band.NumberOfMembers} members; {band.Style} style; website: {band.Website}; contact is {band.ContactName} at {band.ContactPhoneNumber}");
                            }
                        }
                        break;
                    case "C":
                        foreach (var band in bands)
                        {
                            if (band.IsSigned == false)
                            {
                                Console.WriteLine($"{band.Name}");
                                Console.WriteLine($"   From {band.CountryOfOrigin}; {band.NumberOfMembers} members; {band.Style} style; website: {band.Website}; contact is {band.ContactName} at {band.ContactPhoneNumber}");
                            }
                        }
                        break;
                    case "D":
                        Console.WriteLine("Pick a band: ");
                        foreach (var band in bands)
                        {
                            Console.WriteLine($"{band.Name}");
                        }
                        var bandForAlbum = Console.ReadLine().ToLower();
                        Console.WriteLine();
                        var bandAlbumList = albums.Where(a => a.Band.Name.ToLower() == bandForAlbum);
                        foreach (var album in bandAlbumList)
                        {
                            Console.WriteLine($"{album.Title}");
                        }
                        if (bandAlbumList.Count() == 0)
                        {
                            Console.WriteLine("No albums");
                        }
                        break;
                    case "E":
                        var albumsInOrder = albums.OrderBy(album => album.ReleaseDate);
                        foreach (var album in albumsInOrder)
                        {
                            Console.WriteLine($"{album.Title} by {album.Band.Name}");
                        }
                        break;
                    case "F":
                        Console.WriteLine("Pick a band: ");
                        foreach (var band in bands)
                        {
                            Console.WriteLine($"{band.Name}");
                        }
                        var bandForSongs = Console.ReadLine().ToLower();
                        Console.WriteLine();
                        var albumList = albums.Where(a => a.Band.Name.ToLower() == bandForSongs);
                        Console.WriteLine("Pick an album");
                        foreach (var album in albumList)
                        {
                            Console.WriteLine($"{album.Title}");
                        }
                        var albumForSongs = Console.ReadLine().ToLower();
                        Console.WriteLine();
                        var songsFromAlbum = songs.Where(s => s.Album.Title.ToLower() == albumForSongs).OrderBy(s => s.TrackNumber);
                        foreach (var song in songsFromAlbum)
                        {
                            Console.WriteLine($"{song.TrackNumber}. {song.Title}");
                        }
                        if (songsFromAlbum.Count() == 0)
                        {
                            Console.WriteLine("No songs");
                        }

                        break;
                    case "G":
                        // add a band
                        // Name
                        var newBandName = PromptForString("What is the band's name? ");
                        // CountryOfOrigin
                        var newBandCountry = PromptForString("What country are they from ?");
                        // NumberOfMembers
                        var newBandMembers = PromptForInt("How many members are there? ");
                        // Website
                        var newBandWebsite = PromptForString("What is their website? ");
                        // Style
                        var newBandStyle = PromptForString("What is their style? ");
                        // IsSigned 
                        // ContactName
                        var newBandContactName = PromptForString("Who is their contact? ");
                        //ContactPhoneNumber
                        var newBandContactPhone = PromptForLong("What is their phone number? ");

                        var newBand = new Band
                        {
                            Name = newBandName,
                            CountryOfOrigin = newBandCountry,
                            NumberOfMembers = newBandMembers,
                            Website = newBandWebsite,
                            Style = newBandStyle,
                            IsSigned = true,
                            ContactName = newBandContactName,
                            ContactPhoneNumber = newBandContactPhone
                        };
                        context.Bands.Add(newBand);
                        context.SaveChanges();

                        break;
                    case "H":
                        //add an album
                        // BandId
                        var newAlbumBandName = PromptForString("What band is this for? ");
                        var newAlbumBand = bands.First(band => band.Name.ToLower() == newAlbumBandName.ToLower());
                        var newAlbumBandId = newAlbumBand.Id;
                        // Title
                        var newAlbumTitle = PromptForString("What is the title? ");
                        // IsExplicit
                        Console.Write("Is it explicit? Y/N ");
                        var isItExplicit = Console.ReadLine().ToUpper();
                        var explicitContent = true;
                        if (isItExplicit == "N")
                        {
                            explicitContent = false;
                        }
                        else if (isItExplicit == "Y")
                        {
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that is not a valid answer. Entering explicit.");
                        }
                        // ReleaseDate
                        var newAlbumReleaseDate = PromptForDate("When was it released? Please enter as YYYY-MM-DD ");

                        var newAlbum = new Album
                        {
                            Title = newAlbumTitle,
                            IsExplicit = explicitContent,
                            ReleaseDate = newAlbumReleaseDate,
                            BandId = newAlbumBandId
                        };
                        context.Albums.Add(newAlbum);
                        context.SaveChanges();
                        break;
                    case "I":
                        // add a song
                        // AlbumId
                        var newSongAlbumTitle = PromptForString("What is album is this on? ");
                        var newSongAlbum = albums.First(album => newSongAlbumTitle.ToLower() == album.Title.ToLower());
                        var newSongAlbumId = newSongAlbum.Id;
                        // Title
                        var newSongTitle = PromptForString("What is the title of the song? ");
                        // TrackNumber
                        var newSongTrack = PromptForInt("What track is it? ");
                        // Duration
                        var newSongDuration = PromptForTime("How long is it? Please enter as mm:ss ");

                        var newSong = new Song
                        {
                            TrackNumber = newSongTrack,
                            Title = newSongTitle,
                            Duration = newSongDuration,
                            AlbumId = newSongAlbumId
                        };
                        context.Songs.Add(newSong);
                        context.SaveChanges();
                        break;
                    case "J":
                        // let a band go
                        Console.WriteLine("Which band would you like to let go?");
                        var bandToLetGo = Console.ReadLine().ToLower();
                        var goodbyeBand = bands.First(b => b.Name.ToLower() == bandToLetGo);
                        if (goodbyeBand.IsSigned == true)
                        {
                            goodbyeBand.IsSigned = false;
                            Console.WriteLine($"{goodbyeBand.Name} was let go.");
                        }
                        else
                        {
                            Console.WriteLine("Band already not signed.");
                        }
                        break;
                    case "K":
                        //resign a band
                        Console.WriteLine("Which band would you like to resign?");
                        var bandToResign = Console.ReadLine().ToLower();
                        var welcomeBack = bands.FirstOrDefault(b => b.Name.ToLower() == bandToResign);
                        if (welcomeBack.IsSigned == false)
                        {
                            welcomeBack.IsSigned = true;
                            Console.WriteLine($"{welcomeBack.Name} was resigned.");
                        }
                        else
                        {
                            Console.WriteLine("Band was already signed.");
                        }
                        break;
                    case "L":
                        Console.WriteLine("Pick a band");
                        Console.WriteLine();
                        foreach (var band in bands)
                        {
                            Console.WriteLine(band.Name);
                        }
                        var bandForMembers = Console.ReadLine().ToLower();
                        var thisBandName = bands.First(b => b.Name.ToLower() == bandForMembers);
                        var membersOfBand = bandMusicians.Where(x => x.BandId == thisBandName.Id);
                        Console.WriteLine();

                        foreach (var member in membersOfBand)
                        {
                            Console.WriteLine(member.Musician.Name + " on " + member.Musician.Instrument);
                        }
                        if (membersOfBand.Count() == 0)
                        {
                            Console.WriteLine("There are no members in the system.");
                        }
                        break;
                    case "M":
                        //view albums by genre
                        var genre = PromptForString("What genre would you like to view? ");
                        var genreAlbums = albums.Where(album => album.Band.Style.ToLower() == genre.ToLower());
                        foreach (var album in genreAlbums)
                        {
                            Console.WriteLine(album.Title + " by " + album.Band.Name);
                        }
                        if (genreAlbums.Count() == 0)
                        {
                            Console.WriteLine($"There are no {genre} albums.");
                        }
                        break;
                    default:
                        Console.WriteLine("Sorry, I don't understand. Please try again.");
                        break;
                }

            }

        }
    }
}
