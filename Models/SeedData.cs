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
                        DateOfBirth = DateTime.Parse("1999-2-06"),
                        Type = "User",
                        ProfilePicture = "girl1.jpg"
                    },

                    new User
                    {
                        FirstName = "Ana",
                        LastName = "Petrusevska",
                        Username = "Ana",
                        DateOfBirth = DateTime.Parse("1998-8-31"),
                        Type = "User",
                        ProfilePicture = "girl2.jpg"
                    },

                    new User
                    {
                        FirstName = "Tyler",
                        LastName = "Okonma",
                        Username = "Tyler, The Creator",
                        DateOfBirth = DateTime.Parse("1991-3-06"),
                        Type = "Artist",
                        ProfilePicture = "tyler.jfif"
                    },

                    new User
                    {
                        FirstName = "Malcolm",
                        LastName = "McCormick",
                        Username = "Mac Miller",
                        DateOfBirth = DateTime.Parse("1992-1-19"),
                        Type = "Artist",
                        ProfilePicture = "mac miller.jfif"
                    },

                    new User
                    {
                        FirstName = "Olly",
                        LastName = "Alexander",
                        Username = "Years & Years",
                        DateOfBirth = DateTime.Parse("1990-7-15"),
                        Type = "Artist",
                        ProfilePicture = "olly.jfif"
                    },

                    new User
                    {
                        FirstName = "Matty",
                        LastName = "Healy",
                        Username = "The 1975",
                        DateOfBirth = DateTime.Parse("1989-4-8"),
                        Type = "Artist",
                        ProfilePicture = "the 1975 pp.jfif"
                    },

                    new User
                    {
                        FirstName = "Jeremy",
                        LastName = "Zucker",
                        Username = "Jeremy Zucker",
                        DateOfBirth = DateTime.Parse("1996-3-3"),
                        Type = "Artist",
                        ProfilePicture = "jeremy zucker.jfif"
                    }
                );
                context.SaveChanges();

                context.Album.AddRange(
                    new Album
                    {
                        Name = "Flower Boy",
                        ArtistId = context.User.Single(t => t.Username == "Tyler, The Creator").Id,
                        Minutes = 16,
                        Seconds = 19,
                        ProfilePicture = "flower boy.jfif"
                    },

                    new Album
                    {
                        Name = "CALL ME IF YOU GET LOST",
                        ArtistId = context.User.Single(t => t.Username == "Tyler, The Creator").Id,
                        Minutes = 20,
                        Seconds = 24,
                        ProfilePicture = "call me if you get lost.jfif"
                    },

                    new Album
                    {
                        Name = "Swimming",
                        ArtistId = context.User.Single(t => t.Username == "Mac Miller").Id,
                        Minutes = 24,
                        Seconds = 55,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Album
                    {
                        Name = "Circles",
                        ArtistId = context.User.Single(t => t.Username == "Mac Miller").Id,
                        Minutes = 12,
                        Seconds = 1,
                        ProfilePicture = "circles.jfif"
                    },

                    new Album
                    {
                        Name = "The 1975",
                        ArtistId = context.User.Single(t => t.Username == "The 1975").Id,
                        Minutes = 13,
                        Seconds = 16,
                        ProfilePicture = "the 1975.jfif"
                    },

                    new Album
                    {
                        Name = "ILIWYS",
                        ArtistId = context.User.Single(t => t.Username == "The 1975").Id,
                        Minutes = 17,
                        Seconds = 50,
                        ProfilePicture = "ILIWYS.jfif"
                    },

                    new Album
                    {
                        Name = "love is not dying",
                        ArtistId = context.User.Single(t => t.Username == "Jeremy Zucker").Id,
                        Minutes = 9,
                        Seconds = 2,
                        ProfilePicture = "love is not dying.jfif"
                    },

                    new Album
                    {
                        Name = "supercuts - Single",
                        ArtistId = context.User.Single(t => t.Username == "Jeremy Zucker").Id,
                        Minutes = 3,
                        Seconds = 26,
                        ProfilePicture = "supercuts.jfif"
                    },

                    new Album
                    {
                        Name = "Communion",
                        ArtistId = context.User.Single(t => t.Username == "Years & Years").Id,
                        Minutes = 15,
                        Seconds = 41,
                        ProfilePicture = "communion.jfif"
                    }
                );
                context.SaveChanges();

                context.Song.AddRange(
                    new Song
                    {
                        Name = "See You Again (feat. Kali Uchis)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 3,
                        Seconds = 0,
                        ProfilePicture = "flower boy.jfif"
                    },

                    new Song
                    {
                        Name = "Boredom (feat. Rex Orange County)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 5,
                        Seconds = 20,
                        ProfilePicture = "flower boy.jfif"
                    },

                    new Song
                    {
                        Name = "911 / Mr. Lonely (feat. Frank Ocean)",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 4,
                        Seconds = 15,
                        ProfilePicture = "flower boy.jfif"
                    },

                    new Song
                    {
                        Name = "Glitter",
                        AlbumId = context.Album.Single(t => t.Name == "Flower Boy").Id,
                        Minutes = 3,
                        Seconds = 44,
                        ProfilePicture = "flower boy.jfif"
                    },

                    new Song
                    {
                        Name = "WUSYANAME",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 2,
                        Seconds = 1,
                        ProfilePicture = "call me if you get lost.jfif"
                    },

                    new Song
                    {
                        Name = "SWEET / I THOUGHT YOU WANTED TO DANCE",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 9,
                        Seconds = 48,
                        ProfilePicture = "call me if you get lost.jfif"
                    },

                    new Song
                    {
                        Name = "WILSHIRE",
                        AlbumId = context.Album.Single(t => t.Name == "CALL ME IF YOU GET LOST").Id,
                        Minutes = 8,
                        Seconds = 35,
                        ProfilePicture = "call me if you get lost.jfif"
                    },

                    new Song
                    {
                        Name = "Hurt Feelings",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 5,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Song
                    {
                        Name = "Self Care",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 5,
                        Seconds = 45,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Song
                    {
                        Name = "Ladders",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 47,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Song
                    {
                        Name = "Small Worlds",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 4,
                        Seconds = 31,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Song
                    {
                        Name = "2009",
                        AlbumId = context.Album.Single(t => t.Name == "Swimming").Id,
                        Minutes = 5,
                        Seconds = 47,
                        ProfilePicture = "swimming.jfif"
                    },

                    new Song
                    {
                        Name = "Circles",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 2,
                        Seconds = 50,
                        ProfilePicture = "circles.jfif"
                    },

                    new Song
                    {
                        Name = "Blue World",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 3,
                        Seconds = 29,
                        ProfilePicture = "circles.jfif"
                    },

                    new Song
                    {
                        Name = "Good News",
                        AlbumId = context.Album.Single(t => t.Name == "Circles").Id,
                        Minutes = 5,
                        Seconds = 42,
                        ProfilePicture = "circles.jfif"
                    },

                    new Song
                    {
                        Name = "The 1975",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 1,
                        Seconds = 19,
                        ProfilePicture = "the 1975.jfif"
                    },

                    new Song
                    {
                        Name = "Chocolate",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 3,
                        Seconds = 44,
                        ProfilePicture = "the 1975.jfif"
                    },

                    new Song
                    {
                        Name = "Settle Down",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 3,
                        Seconds = 59,
                        ProfilePicture = "the 1975.jfif"
                    },

                    new Song
                    {
                        Name = "Robbers",
                        AlbumId = context.Album.Single(t => t.Name == "The 1975").Id,
                        Minutes = 4,
                        Seconds = 14,
                        ProfilePicture = "the 1975.jfif"
                    },

                    new Song
                    {
                        Name = "The 1975",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 1,
                        Seconds = 23,
                        ProfilePicture = "ILIWYS.jfif"
                    },

                    new Song
                    {
                        Name = "If I Believe You",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 6,
                        Seconds = 20,
                        ProfilePicture = "ILIWYS.jfif"
                    },

                    new Song
                    {
                        Name = "Somebody Else",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 5,
                        Seconds = 47,
                        ProfilePicture = "ILIWYS.jfif"
                    },

                    new Song
                    {
                        Name = "Loving Someone",
                        AlbumId = context.Album.Single(t => t.Name == "ILIWYS").Id,
                        Minutes = 4,
                        Seconds = 20,
                        ProfilePicture = "ILIWYS.jfif"
                    },

                    new Song
                    {
                        Name = "lakehouse",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 3,
                        Seconds = 42,
                        ProfilePicture = "love is not dying.jfif"
                    },

                    new Song
                    {
                        Name = "not ur friend",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 2,
                        Seconds = 54,
                        ProfilePicture = "love is not dying.jfif"
                    },

                    new Song
                    {
                        Name = "always, i'll care",
                        AlbumId = context.Album.Single(t => t.Name == "love is not dying").Id,
                        Minutes = 2,
                        Seconds = 26,
                        ProfilePicture = "love is not dying.jfif"
                    },

                    new Song
                    {
                        Name = "supercuts",
                        AlbumId = context.Album.Single(t => t.Name == "supercuts - Single").Id,
                        Minutes = 3,
                        Seconds = 26,
                        ProfilePicture = "supercuts.jfif"
                    },

                    new Song
                    {
                        Name = "Shine",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 4,
                        Seconds = 15,
                        ProfilePicture = "communion.jfif"
                    },

                    new Song
                    {
                        Name = "Worship",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 41,
                        ProfilePicture = "communion.jfif"
                    },

                    new Song
                    {
                        Name = "Ties",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 46,
                        ProfilePicture = "communion.jfif"
                    },

                    new Song
                    {
                        Name = "Gold",
                        AlbumId = context.Album.Single(t => t.Name == "Communion").Id,
                        Minutes = 3,
                        Seconds = 59,
                        ProfilePicture = "communion.jfif"
                    }
                );
                context.SaveChanges();

                context.Playlist.AddRange(
                    new Playlist
                    {
                        Name = "My First Playlist",
                        Minutes = 7,
                        Seconds = 27,
                        UserId = context.User.Single(t => t.Username == "Ana").Id,
                        ProfilePicture = "playlist.jpg"
                    }
                );
                context.SaveChanges();

                context.SongPlaylist.AddRange(
                    new SongPlaylist
                    {
                        SongId = context.Song.Single(t => t.Name == "Worship").Id,
                        PlaylistId = context.Playlist.Single(t => t.Name == "My First Playlist").Id
                    },

                    new SongPlaylist
                    {
                        SongId = context.Song.Single(t => t.Name == "Ties").Id,
                        PlaylistId = context.Playlist.Single(t => t.Name == "My First Playlist").Id
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
