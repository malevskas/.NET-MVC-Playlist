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
    public class SongsController : Controller
    {
        private readonly NewProjectContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment webHostEnvironment;

        public SongsController(NewProjectContext context, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string searchName, string searchAlbum, string searchArtist)
        {
            IQueryable<Song> songs = _context.Song.AsQueryable();
            songs = songs.Include(m => m.Album).ThenInclude(m => m.Artist);
            IQueryable<string> albumQuery = _context.Album.OrderBy(m => m.Name).Select(m => m.Name).Distinct();
            IQueryable<string> artistQuery = _context.Album.Include(a => a.Artist).OrderBy(m => m.Artist.Username).Select(m => m.Artist.Username).Distinct();
            if (!string.IsNullOrEmpty(searchName))
            {
                songs = songs.Where(s => s.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchAlbum))
            {
                songs = songs.Where(x => x.Album.Name == searchAlbum);
            }
            if (!string.IsNullOrEmpty(searchArtist))
            {
                songs = songs.Where(x => x.Album.Artist.Username == searchArtist);
            }
            songs = songs.OrderBy(s => s.Name);
            var songVM = new SongViewModel
            {
                Albums = new SelectList(await albumQuery.ToListAsync()),
                Artists = new SelectList(await artistQuery.ToListAsync()),
                Songs = await songs.ToListAsync()
            };
            return View(songVM);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>().OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AlbumId,Minutes,Seconds")] Song song)
        {
            if (ModelState.IsValid)
            {
                var album = await _context.Album.FindAsync(song.AlbumId);
                song.ProfilePicture = album.ProfilePicture;
                _context.Add(song);

                int minutes = album.Minutes;
                int seconds = album.Seconds;
                seconds += song.Seconds;
                if (seconds > 59)
                {
                    int over = seconds / 60;
                    minutes += over;
                    seconds %= 60;
                }
                album.Minutes = minutes + song.Minutes;
                album.Seconds = seconds;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AlbumId,Minutes,Seconds")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var album = await _context.Album.FindAsync(song.AlbumId);
                    string uniqueFileName = UploadedFile(song);
                    song.ProfilePicture = uniqueFileName;
                    _context.Update(song);

                    int minutes = album.Minutes;
                    int seconds = album.Seconds;
                    seconds += song.Seconds;
                    if (seconds > 59)
                    {
                        int over = seconds / 60;
                        minutes += over;
                        seconds %= 60;
                    }
                    album.Minutes = minutes + song.Minutes;
                    album.Seconds = seconds;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Set<Album>(), "Id", "Name", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            var album = await _context.Album.FindAsync(song.AlbumId);

            int minutes = song.Minutes, seconds = song.Seconds;

            if (seconds > album.Seconds)
            {
                seconds -= album.Seconds;
                album.Seconds = 60 - seconds;
                album.Minutes -= 1;
            }
            else
            {
                album.Seconds -= seconds;
            }

            album.Minutes -= minutes;
            _context.Song.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Album(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Song> songs = _context.Song.AsQueryable();
            songs = songs.Where(x => x.AlbumId == id);
            IList<Song> Songs = await songs.ToListAsync();

            var album = await _context.Album
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["Album"] = album.Name;
            ViewData["Artist"] = album.Artist.Username;

            return View(Songs);
        }

        public async Task<IActionResult> Artist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Album> albums = _context.Album.AsQueryable();
            albums = albums.Where(x => x.ArtistId == id);
            IList<Album> Albums = await albums.ToListAsync();

            var artist = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["Artist"] = artist.Username;

            return View(Albums);
        }

        private string UploadedFile(Song model)
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

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
