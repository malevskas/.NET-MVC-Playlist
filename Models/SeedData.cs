using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NewProjectContext(serviceProvider.GetRequiredService<DbContextOptions<NewProjectContext>>()))
            {
                if (context.Song.Any() || context.User.Any() || context.Album.Any() || context.Playlist.Any())
                {
                    return;
                }

                context.User.AddRange(
                    new User
                    {
                        FirstName = "Simona",
                        LastName = "Malevska",
                        Username = "Mona",
                        DateOfBirth = DateTime.Parse("1999-2-06")
                    },

                    new User
                    {
                        FirstName = "Tijana",
                        LastName = "Nastevska",
                        Username = "Tijana",
                        DateOfBirth = DateTime.Parse("1998-8-31")
                    },

                    new User
                    {
                        FirstName = "Tyler",
                        LastName = "Okonma",
                        Username = "Tyler, The Creator",
                        DateOfBirth = DateTime.Parse("1991-3-06")
                    },

                    new User
                    {
                        FirstName = "Malcolm",
                        LastName = "McCormick",
                        Username = "Mac Miller",
                        DateOfBirth = DateTime.Parse("1992-1-19")
                    },

                    new User
                    {
                        FirstName = "Olly",
                        LastName = "Alexander",
                        Username = "Years & Years",
                        DateOfBirth = DateTime.Parse("1990-7-15")
                    },

                    new User
                    {
                        FirstName = "Matty",
                        LastName = "Healy",
                        Username = "The 1975",
                        DateOfBirth = DateTime.Parse("1989-4-8")
                    },

                    new User
                    {
                        FirstName = "Jeremy",
                        LastName = "Zucker",
                        Username = "Jeremy Zucker",
                        DateOfBirth = DateTime.Parse("1996-3-3")
                    }
                );
                context.SaveChanges();

                context.Album.AddRange(
                    new Album
                    {
                        Name = "Flower Boy",
                        ArtistId = context.User.Single(t => t.Username == "Tyler, The Creator").Id,
                        Minutes = 16,
                        Seconds = 19
                    },

                    new Album
                    {
                        Name = "CALL ME IF YOU GET LOST",
                        ArtistId = context.User.Single(t => t.Username == "Tyler, The Creator").Id,
                        Minutes = 20,
                        Seconds = 24
                    },

                    new Album
                    {
                        Name = "Swimming",
                        ArtistId = context.User.Single(t => t.Username == "Mac Miller").Id,
                        Minutes = 24,
                        Seconds = 55
                    },

                    new Album
                    {
                        Name = "Circles",
                        ArtistId = context.User.Single(t => t.Username == "Mac Miller").Id,
                        Minutes = 12,
                        Seconds = 1
                    },

                    new Album
                    {
                        Name = "The 1975",
                        ArtistId = context.User.Single(t => t.Username == "The 1975").Id,
                        Minutes = 13,
                        Seconds = 16
                    },

                    new Album
                    {
                        Name = "ILIWYS",
                        ArtistId = context.User.Single(t => t.Username == "The 1975").Id,
                        Minutes = 17,
                        Seconds = 50
                    },

                    new Album
                    {
                        Name = "love is not dying",
                        ArtistId = context.User.Single(t => t.Username == "Jeremy Zucker").Id,
                        Minutes = 9,
                        Seconds = 2
                    },

                    new Album
                    {
                        Name = "supercuts - Single",
                        ArtistId = context.User.Single(t => t.Username == "Jeremy Zucker").Id,
                        Minutes = 3,
                        Seconds = 26
                    },

                    new Album
                    {
                        Name = "Communion",
                        ArtistId = context.User.Single(t => t.Username == "Years & Years").Id,
                        Minutes = 15,
                        Seconds = 41
                    }
                );
                context.SaveChanges();

                context.Song.AddRange(
                    new Song
                    {
                        Name = "See You Again (feat. Kali Uchis)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 3,
                        Seconds = 0
                    },

                    new Song
                    {
                        Name = "Boredom (feat. Rex Orange County)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 5,
                        Seconds = 20
                    },

                    new Song
                    {
                        Name = "911 / Mr. Lonely (feat. Frank Ocean)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 4,
                        Seconds = 15
                    },

                    new Song
                    {
                        Name = "Glitter",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 3,
                        Seconds = 44
                    },

                    new Song
                    {
                        Name = "WUSYANAME",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 2,
                        Seconds = 1
                    },

                    new Song
                    {
                        Name = "SWEET / I THOUGHT YOU WANTED TO DANCE",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 9,
                        Seconds = 48
                    },

                    new Song
                    {
                        Name = "WILSHIRE",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 8,
                        Seconds = 35
                    },

                    new Song
                    {
                        Name = "Hurt Feelings",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 5
                    },

                    new Song
                    {
                        Name = "Self Care",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 5,
                        Seconds = 45
                    },

                    new Song
                    {
                        Name = "Ladders",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 47
                    },

                    new Song
                    {
                        Name = "Small Worlds",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 31
                    },

                    new Song
                    {
                        Name = "2009",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 5,
                        Seconds = 47
                    },

                    new Song
                    {
                        Name = "Circles",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 2,
                        Seconds = 50
                    },

                    new Song
                    {
                        Name = "Blue World",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 3,
                        Seconds = 29
                    },

                    new Song
                    {
                        Name = "Good News",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 5,
                        Seconds = 42
                    },

                    new Song
                    {
                        Name = "The 1975",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 1,
                        Seconds = 19
                    },

                    new Song
                    {
                        Name = "Chocolate",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 3,
                        Seconds = 44
                    },

                    new Song
                    {
                        Name = "Settle Down",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 3,
                        Seconds = 59
                    },

                    new Song
                    {
                        Name = "Robbers",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 4,
                        Seconds = 14
                    },

                    new Song
                    {
                        Name = "The 1975",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 1,
                        Seconds = 23
                    },

                    new Song
                    {
                        Name = "If I Believe You",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 6,
                        Seconds = 20
                    },

                    new Song
                    {
                        Name = "Somebody Else",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 5,
                        Seconds = 47
                    },

                    new Song
                    {
                        Name = "Loving Someone",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 4,
                        Seconds = 20
                    },

                    new Song
                    {
                        Name = "lakehouse",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 3,
                        Seconds = 42
                    },

                    new Song
                    {
                        Name = "not ur friend",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 2,
                        Seconds = 54
                    },

                    new Song
                    {
                        Name = "always, i'll care",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 2,
                        Seconds = 26
                    },

                    new Song
                    {
                        Name = "supercuts",
                        AlbumId = context.Album.Single(t => t.Name == "supercuts - Single").Id,
                        Minutes = 3,
                        Seconds = 26
                    },

                    new Song
                    {
                        Name = "Shine",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 4,
                        Seconds = 15
                    },

                    new Song
                    {
                        Name = "Worship",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 41
                    },

                    new Song
                    {
                        Name = "Ties",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 46
                    },

                    new Song
                    {
                        Name = "Gold",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 59
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
