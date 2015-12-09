using Microsoft.AspNet.Mvc;
using The_World.Services;
using The_World.ViewModels;

namespace The_World.Controllers.Web
{

    public class AppController : Controller
    {
        private IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
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