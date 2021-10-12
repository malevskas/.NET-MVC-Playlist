using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;

namespace NewProject.Controllers
{
    public class SongPlaylistsController : Controller
    {
        private readonly NewProjectContext _context;

        public SongPlaylistsController(NewProjectContext context)
        {
            _context = context;
        }

        // GET: SongPlaylists
        public async Task<IActionResult> Index()
        {
            var newProjectContext = _context.SongPlaylist.Include(s => s.Playlist).Include(s => s.Song);
            return View(await newProjectContext.ToListAsync());
        }

        // GET: SongPlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: SongPlaylists/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name");
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name");
            return View();
        }

        // POST: SongPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,PlaylistId")] SongPlaylist songPlaylist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songPlaylist);
                await _context.SaveChangesAsync();

                var playlist = await _context.Playlist.FirstOrDefaultAsync(m => m.Id == songPlaylist.PlaylistId);
                var song = await _context.Song.FirstOrDefaultAsync(m => m.Id == songPlaylist.SongId);
                int minutes = playlist.Minutes;
                int seconds = playlist.Seconds;
                seconds += song.Seconds;
                if (seconds > 59)
                {
                    int over = seconds / 60;
                    minutes += over;
                    seconds %= 60;
                }
                playlist.Minutes = minutes + song.Minutes;
                playlist.Seconds = seconds;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songPlaylist.SongId);
            return View(songPlaylist);
        }

        // GET: SongPlaylists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songPlaylist = await _context.SongPlaylist.FindAsync(id);
            if (songPlaylist == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songPlaylist.SongId);
            return View(songPlaylist);
        }

        // POST: SongPlaylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,PlaylistId")] SongPlaylist songPlaylist)
        {
            if (id != songPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongPlaylistExists(songPlaylist.Id))
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
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songPlaylist.SongId);
            return View(songPlaylist);
        }

        // GET: SongPlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: SongPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songPlaylist = await _context.SongPlaylist.FindAsync(id);
            _context.SongPlaylist.Remove(songPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongPlaylistExists(int id)
        {
            return _context.SongPlaylist.Any(e => e.Id == id);
        }
    }
}
