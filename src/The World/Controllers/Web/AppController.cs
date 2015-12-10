using Microsoft.AspNet.Mvc;
using The_World.Models;
using The_World.Services;
using The_World.ViewModels;
using System.Linq;

namespace The_World.Controllers.Web
{

    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _context;

        public AppController(IMailService mailService, IWorldRepository worldContext)
        {
            _context = worldContext;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var trips = _context.GetAllTrips();

            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("", model.Email, $"Contact page from {model.Name}", model.Message);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}