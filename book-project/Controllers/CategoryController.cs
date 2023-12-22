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
    
    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
}