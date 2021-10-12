﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class Song
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Album")]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int Minutes { get; set; }
        [Range(0, 59)]
        public int Seconds { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<SongPlaylist> Playlists { get; set; }
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
