using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
