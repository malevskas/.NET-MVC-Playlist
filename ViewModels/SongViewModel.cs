using Microsoft.AspNetCore.Mvc.Rendering;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.ViewModels
{
    public class SongViewModel
    {
        public IList<Song> Songs { get; set; }
        public SelectList Albums { get; set; }
        public SelectList Artists { get; set; }
        public string SearchName { get; set; }
        public string SearchAlbum { get; set; }
        public string SearchArtist { get; set; }
    }
}
