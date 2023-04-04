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
            {
                ViewData["id"] = id;
                return View("NotFound");
            }

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

        public IActionResult Edit(int id)
        {
            // najit osobu z db
            var person = _context.Persons.Find(id);

            // zobrazit editacni form
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person form_person)
        {
            if (!ModelState.IsValid)
            {
                //nevalidni data, vratit error message
            }

            // najit osobu z db
            var db_person = _context.Persons.Find(form_person.Id);

            // upravit hodnoty v db dle inputu z formu
            // 1. možnost zápisu
            db_person.FirstName = form_person.FirstName;
            db_person.LastName = form_person.LastName;
            db_person.Email = form_person.Email;
            db_person.DateOfBirth = form_person.DateOfBirth;

            // 2. možnost zápisu
            //_context.Entry(db_person).CurrentValues.SetValues(form_person);

            // 3. možnost
            //_context.Entry(form_person).State = EntityState.Modified;
            

            _context.SaveChanges();

            // view
            ViewData["success_message"] = "Uloženo do databáze";
            return View(db_person);
        }

        public IActionResult GetByEmail(string email)
        {
            //vyhledat osobu
            var person = _context.Persons
                        .Where(person => person.Email.ToUpper() == email.ToUpper())
                        .FirstOrDefault();

            if(person == null)
                return NotFound();

            return View("Detail", person);
                        
        }
    }
}
