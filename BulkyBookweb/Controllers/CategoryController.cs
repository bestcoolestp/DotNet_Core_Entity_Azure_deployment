using BulkyBookweb.Data;
using BulkyBookweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoriesList = _db.Categories;
            return View(objCategoriesList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {   
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder must not match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		//GET
		public IActionResult Edit(int? id)
		{   
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var CategoryFromSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (CategoryFromDb == null) 
            {
                return NotFound();
            }
            return View(CategoryFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder must not match the Name.");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
			}
			return View(obj);
		}

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var CategoryFromSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {   
                var obj = _db.Categories.Find(id);
                if (obj == null)
                {
                return NotFound();
                }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
