using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;

namespace NewProject.Data
{
    public class NewProjectContext : DbContext
    {
        public NewProjectContext (DbContextOptions<NewProjectContext> options)
            : base(options)
        {
        }

        public DbSet<NewProject.Models.Song> Song { get; set; }

        public DbSet<NewProject.Models.Album> Album { get; set; }

        public DbSet<NewProject.Models.Playlist> Playlist { get; set; }

        public DbSet<NewProject.Models.User> User { get; set; }

        public DbSet<NewProject.Models.SongPlaylist> SongPlaylist { get; set; }
    }
}
