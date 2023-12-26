using book_project.data_access.Data;
using book_project.data_access.Repository.IRepository;
using book_project.models;
using Microsoft.AspNetCore.Mvc;

namespace book_project.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository db)
    { 
        _categoryRepository = db;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
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
        
        _categoryRepository.Add(obj);
        _categoryRepository.Save();
        
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

        Category? categoryFromDb = _categoryRepository.Get((i => i.Id == id));
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
            _categoryRepository.Update(obj);
            _categoryRepository.Save();
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

        Category? categoryFromDb = _categoryRepository.Get(i => i.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryFromDb = _categoryRepository.Get(i => i.Id == id);
        
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        _categoryRepository.Remove(categoryFromDb);
        _categoryRepository.Save();
        
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}