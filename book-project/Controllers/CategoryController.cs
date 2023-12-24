using book_project.Data;
using book_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace book_project.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    { 
        _db = db;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "Name and Display Order cannot match ");
        }
        
        if (!ModelState.IsValid) return View();
        
        _db.Categories.Add(obj);
        _db.SaveChanges();
        
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound(); 
        }

        Category? categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
        }

        TempData["success"] = "Category edited successfully";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound(); 
        }

        Category? categoryFromDb = _db.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryFromDb = _db.Categories.Find(id);
        
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        _db.Remove(categoryFromDb);
        _db.SaveChanges();
        
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}