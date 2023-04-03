using InsuranceCorp.Data;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly InsCorpDbContext _context;

        public PersonController(InsCorpDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // 1. ziskat data
            var top100 = _context.Persons
                    .OrderBy(person => person.Id)
                    .Take(100).ToList();

            // 2. zobrazit view
            return View(top100);
        }
    }
}
