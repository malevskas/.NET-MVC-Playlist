using Microsoft.AspNetCore.Mvc.Rendering;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.ViewModels
{
    public class AlbumViewModel
    {
        public IList<Album> Albums { get; set; }
        public SelectList Artists { get; set; }
        public string SearchArtist { get; set; }
        public string SearchName { get; set; }
    }
}
