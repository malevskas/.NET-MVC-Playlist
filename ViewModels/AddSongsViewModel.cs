using Microsoft.AspNetCore.Mvc.Rendering;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.ViewModels
{
    public class AddSongsViewModel
    {
        public Playlist Playlist { get; set; }
        public IEnumerable<int> SelectedSongs { get; set; }
        public IEnumerable<SelectListItem> SongList { get; set; }

    }
}
