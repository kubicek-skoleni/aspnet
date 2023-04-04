using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    .Include(person => person.Constracts)
                    .OrderBy(person => person.Id)
                    //.OrderByDescending(person => person.Constracts.Count())
                    .Take(100).ToList();

            // 2. zobrazit view
            return View(top100);
        }

        public IActionResult Detail(int id)
        {
            // 1. ziskat data
            var person = _context.Persons.Find(id);

            //if (person == null)
            //    return NotFound();

            if (person == null)
                return View("NotFound");

            // 2. zobrazit view
            return View(person);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            // ulozit do db
            _context.Persons.Add(person);
            var changed = _context.SaveChanges();

            // navrat gui
            return Redirect($"/person/detail/{person.Id}");
        }
    }
}
