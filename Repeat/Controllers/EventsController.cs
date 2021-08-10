using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repeat.Data;
using Repeat.Models;
using Repeat.ViewModel;

namespace Repeat.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventContext _context;
        private readonly ClubContext _clbContext;
        private readonly ImageContext _imgContext;


        public EventsController(EventContext context, ClubContext clbContext, ImageContext imgContext)
        {
            _context = context;
            _clbContext = clbContext;
            _imgContext = imgContext;
        }

        // GET: Events
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            List<Club> clubs = _context.Club.ToList();
            List<Event> events = _context.Event.ToList();
            List<Image> images = _context.Image.ToList();

            var model = from e in events
                        join c in clubs on e.ClubId equals c.ClubId into table1
                        from c in table1.DefaultIfEmpty()
                        join i in images on e.ImageId equals i.ImageId into table2
                        from i in table2.DefaultIfEmpty()
                        select new HomeViewModel { clubVm = c, eventVm = e, imageVm = i };

            return View(model);
        }

        // GET: Events/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);

            ViewData["ClubName"] = (from club in _context.Club where club.ClubId == @event.ClubId select club.Name).ToList().FirstOrDefault();
            ViewData["Image"] = (from image in _context.Image where image.ImageId == @event.ImageId select image.ImageName).ToList().FirstOrDefault();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
        // GET: Events/EventDetails/5
        public async Task<IActionResult> EventDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);

            ViewData["ClubName"] = (from club in _context.Club where club.ClubId == @event.ClubId select club.Name).ToList().FirstOrDefault();
            ViewData["Image"] = (from image in _context.Image where image.ImageId == @event.ImageId select image.ImageName).ToList().FirstOrDefault();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.ClubList = _context.Club.ToList();
            ViewBag.ImageList = _context.Image.ToList();

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,Genre,Price,Tickets,Beginning,Ending,Description,ClubId,ImageId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ClubList = _context.Club.ToList();
            ViewBag.ImageList = _context.Image.ToList();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,Genre,Price,Tickets,Beginning,Ending,Description,ClubId,ImageId")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);

            ViewData["ClubName"] = (from club in _context.Club where club.ClubId == @event.ClubId select club.Name).ToList().FirstOrDefault();
            ViewData["Image"] = (from image in _context.Image where image.ImageId == @event.ImageId select image.ImageName).ToList().FirstOrDefault();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
