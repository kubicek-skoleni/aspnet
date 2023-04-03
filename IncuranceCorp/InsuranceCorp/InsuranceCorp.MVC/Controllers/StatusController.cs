using InsuranceCorp.Data;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.MVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly InsCorpDbContext _context;

        /// <summary>
        /// konstruktor
        /// </summary>
        public StatusController(InsCorpDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
