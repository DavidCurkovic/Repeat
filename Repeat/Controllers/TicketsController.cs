using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Repeat.Data;
using Repeat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repeat.Controllers
{
    public class TicketsController : Controller
    {
        private readonly EventContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketsController(EventContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Events/EventTickets/5
        [Authorize]
        public async Task<IActionResult> EventTickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/EventTickets
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EventTickets(int id, int numberTickets)
        {
            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (numberTickets > @event.Tickets || @event.Tickets < 0)
                    {
                        TempData["MsgChangeStatus"] = "You can't buy that many tickets!";
                    }
                    else
                    {
                        @event.Tickets -= numberTickets;
                        ViewData["TotalPrice"] = @event.Price * numberTickets;
                        string message = "Greetings! " +
                            "To continue shopping, please click on the following link: https://paypal.me  "+
                            "(Total price: " + ViewData["TotalPrice"].ToString() + ",00 kn)";
                        SendMailForPayment(message);
                        _context.Update(@event);
                        await _context.SaveChangesAsync();
                    }
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
                return RedirectToAction("EventTickets", "Tickets");
            }
            return RedirectToAction("EventTickets", "Tickets");
        }
        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }

        public void SendMailForPayment(string message)
        {
            TempData["MsgChangeStatus"] = "Confirmation mail has been sent to your e-mail address!";
            var applicationUser = _userManager.GetUserAsync(User);
            string userEmail = applicationUser.Result.Email;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("repeat.team.service@gmail.com", "Admin_2021"),
                EnableSsl = true
            };
            client.Send("repeat.team.service@gmail.com", userEmail, "Payment Confirmation", message);
        }
    }
}
