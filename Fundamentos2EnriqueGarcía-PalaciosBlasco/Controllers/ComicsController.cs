using Fundamentos2EnriqueGarcía_PalaciosBlasco.Models;
using Fundamentos2EnriqueGarcía_PalaciosBlasco.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fundamentos2EnriqueGarcía_PalaciosBlasco.Controllers
{
    public class ComicsController : Controller
    {
        private IRepositoryComics repo;
        public ComicsController(IRepositoryComics repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comic comic)
        {
            this.repo.InsertComics(comic);
            return RedirectToAction("Index");
        }
    }
}
