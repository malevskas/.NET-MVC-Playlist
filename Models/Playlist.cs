using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Minutes { get; set; }
        [Range(0, 59)]
        public int Seconds { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<SongPlaylist> Songs { get; set; }
        public string Length
        {
            get
            {
                if (Seconds < 10)
                {
                    return Minutes + ":0" + Seconds;
                }
                return Minutes + ":" + Seconds;
            }
        }
    }
}
