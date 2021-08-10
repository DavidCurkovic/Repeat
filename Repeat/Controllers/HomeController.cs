using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repeat.Data;
using Repeat.Models;
using Repeat.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Repeat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


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

            public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
