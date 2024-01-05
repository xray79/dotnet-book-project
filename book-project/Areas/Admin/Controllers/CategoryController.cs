using book_project.data_access.Repository.IRepository;
using book_project.models;
using book_project.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace book_project.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    { 
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
        
        _unitOfWork.Category.Add(obj);
        _unitOfWork.Save();
        
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

        Category? categoryFromDb = _unitOfWork.Category.Get((i => i.Id == id));
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
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
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

        Category? categoryFromDb = _unitOfWork.Category.Get(i => i.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryFromDb = _unitOfWork.Category.Get(i => i.Id == id);
        
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(categoryFromDb);
        _unitOfWork.Save();
        
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}