using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomerError", "can't set name to DO");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj); // queues it up/doesn't submit to db
            _db.SaveChanges(); // submit to db
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        //Category category = _db.Categories.SingleOrDefault(category => category.ID == id);
        //Category category = _db.Categories.FirstOrDefault(category => category.ID == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomerError", "can't set name to DO");
        }
        if (!ModelState.IsValid) return View(obj);
        _db.Categories.Update(obj); // queues it up/doesn't submit to db
        _db.SaveChanges(); // submit to db
        TempData["success"] = "Category Edited Successfully";
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        //_db.Categories.Remove(categoryFromDb); // queues it up/doesn't submit to db
        //_db.SaveChanges(); // submit to db
        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        _db.Categories.Remove(categoryFromDb); // queues it up/doesn't submit to db
        _db.SaveChanges(); // submit to db
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
    }

}
