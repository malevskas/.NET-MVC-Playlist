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
    public class AlbumsController : Controller
    {
        private readonly NewProjectContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment webHostEnvironment;

        public AlbumsController(NewProjectContext context, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Albums
        public async Task<IActionResult> Index(string searchArtist, string searchName)
        {
            IQueryable<Album> albums = _context.Album.AsQueryable();
            IQueryable<string> artistQuery = _context.Album.Include(a => a.Artist).OrderBy(m => m.Artist.Username).Select(m => m.Artist.Username).Distinct();
            if (!string.IsNullOrEmpty(searchName))
            {
                albums = albums.Where(s => s.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchArtist))
            {
                albums = albums.Where(x => x.Artist.Username.Contains(searchArtist));
            }
            albums = albums.OrderBy(s => s.Name).Include(m => m.Artist);
            var albumVM = new AlbumViewModel
            {
                Artists = new SelectList(await artistQuery.ToListAsync()),
                Albums = await albums.ToListAsync()
            };
            return View(albumVM);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Set<User>(), "Id", "Username");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArtistId,Minutes,Seconds,ProfileImage")] Album album)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(album);
                album.ProfilePicture = uniqueFileName;
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<User>(), "Id", "Username", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<User>(), "Id", "Username", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ArtistId,Minutes,Seconds,ProfileImage")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(album);
                    album.ProfilePicture = uniqueFileName;
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Set<User>(), "Id", "Username", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Album.FindAsync(id);
            _context.Album.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Songs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Song> songs = _context.Song.AsQueryable();
            songs = songs.Where(x => x.AlbumId == id).OrderBy(x => x.Name);
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
            albums = albums.Where(x => x.ArtistId == id).OrderBy(x => x.Name);
            IList<Album> Albums = await albums.ToListAsync();

            var artist = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["Artist"] = artist.Username;

            return View(Albums);
        }

        private string UploadedFile(Album model)
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

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.Id == id);
        }
    }
}
