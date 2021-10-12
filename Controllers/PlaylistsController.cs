using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;
using NewProject.ViewModels;

namespace NewProject.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly NewProjectContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment webHostEnvironment;

        public PlaylistsController(NewProjectContext context, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var newProjectContext = _context.Playlist.Include(p => p.User);
            return View(await newProjectContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Minutes,Seconds,UserId,ProfileImage")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(playlist);
                playlist.ProfilePicture = uniqueFileName;
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username", playlist.UserId);
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username");
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Minutes,Seconds,UserId,ProfileImage")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(playlist);
                    playlist.ProfilePicture = uniqueFileName;
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username", playlist.UserId);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlist.FindAsync(id);
            _context.Playlist.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSongs(int id)
        {
            var playlist = _context.Playlist.Where(m => m.Id == id).Include(m => m.Songs).First();
            if (playlist == null)
            {
                return NotFound();
            }
            var songs = _context.Song.AsEnumerable();
            songs = songs.OrderBy(s => s.Name);
            AddSongsViewModel viewmodel = new AddSongsViewModel
            {
                Playlist = playlist,
                SongList = new MultiSelectList(songs, "Id", "Name"),
                SelectedSongs = playlist.Songs.Select(sa => sa.SongId)
            };
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username");
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSongs(int id, AddSongsViewModel viewmodel)
        {
            if (id != viewmodel.Playlist.Id) 
            { 
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Playlist);
                    await _context.SaveChangesAsync();

                    int minutes = 0, seconds = 0;
                    IEnumerable<int> listSongs = viewmodel.SelectedSongs;
                    IQueryable<SongPlaylist> toBeRemoved = _context.SongPlaylist.Where(s => !listSongs.Contains(s.SongId) && s.PlaylistId == id);
                    _context.SongPlaylist.RemoveRange(toBeRemoved);
                    IEnumerable<int> toRemove = toBeRemoved.Select(s => s.SongId);
                    foreach (int songId in toRemove)
                    {
                        var song = await _context.Song.FirstOrDefaultAsync(m => m.Id == songId);
                        minutes += song.Minutes;
                        seconds += song.Seconds;
                    }
                    if(toRemove.Any())
                    {
                        if (seconds > 59)
                        {
                            int over = seconds / 60;
                            minutes += over;
                            seconds %= 60;
                        }

                        if(seconds > viewmodel.Playlist.Seconds)
                        {
                            seconds -= viewmodel.Playlist.Seconds;
                            viewmodel.Playlist.Seconds = 60 - seconds;
                            viewmodel.Playlist.Minutes -= 1;
                        }
                        else
                        {
                            viewmodel.Playlist.Seconds -= seconds;
                        }

                        viewmodel.Playlist.Minutes -= minutes;
                    }

                    IEnumerable<int> existSongs = _context.SongPlaylist.Where(s => listSongs.Contains(s.SongId) && s.PlaylistId == id).Select(s => s.SongId);
                    IEnumerable<int> newSongs = listSongs.Where(s => !existSongs.Contains(s));
                    foreach (int songId in newSongs)
                    {
                        _context.SongPlaylist.Add(new SongPlaylist { SongId = songId, PlaylistId = id });

                        var song = await _context.Song.FirstOrDefaultAsync(m => m.Id == songId);
                        minutes = viewmodel.Playlist.Minutes;
                        seconds = viewmodel.Playlist.Seconds;
                        seconds += song.Seconds;
                        if (seconds > 59)
                        {
                            int over = seconds / 60;
                            minutes += over;
                            seconds %= 60;
                        }
                        viewmodel.Playlist.Minutes = minutes + song.Minutes;
                        viewmodel.Playlist.Seconds = seconds;
                    }
                        
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(viewmodel.Playlist.Id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Username", viewmodel.Playlist.UserId);
            return View(viewmodel);
        }

        public async Task<IActionResult> Songs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<SongPlaylist> songs = _context.SongPlaylist.AsQueryable();
            songs = songs.Where(x => x.PlaylistId == id).Include(x => x.Song).ThenInclude(x => x.Album).ThenInclude(x => x.Artist).OrderBy(x => x.Song.Name);
            IList<SongPlaylist> Songs = await songs.ToListAsync();

            var playlist = await _context.Playlist
                //.Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["Album"] = playlist.Name;

            return View(Songs);
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songPlaylist = await _context.SongPlaylist
                .Include(s => s.Playlist)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songPlaylist == null)
            {
                return NotFound();
            }

            return View(songPlaylist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var songPlaylist = await _context.SongPlaylist.FindAsync(id);
            _context.SongPlaylist.Remove(songPlaylist);

            var playlist = await _context.Playlist.FirstOrDefaultAsync(m => m.Id == songPlaylist.PlaylistId);
            var song = await _context.Song.FirstOrDefaultAsync(m => m.Id == songPlaylist.SongId);

            int minutes = song.Minutes, seconds = song.Seconds;

            if (seconds > playlist.Seconds)
            {
                seconds -= playlist.Seconds;
                playlist.Seconds = 60 - seconds;
                playlist.Minutes -= 1;
            }
            else
            {
                playlist.Seconds -= seconds;
            }

            playlist.Minutes -= minutes;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(Playlist model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlist.Any(e => e.Id == id);
        }
    }
}