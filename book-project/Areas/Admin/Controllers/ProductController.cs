using book_project.data_access.Repository.IRepository;
using book_project.models;
using book_project.models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace book_project.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    { 
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll("Category").ToList();
        
        return View(objProductList);
    }

    [HttpGet]
    public IActionResult Upsert(int? id ) // Update + insert
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
            .GetAll("Category").Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

        ProductVm productVm = new()
        {
            CategoryList = CategoryList,
            Product = new Product(),
        };

        if (id is null or 0) 
        {
            return View(productVm);
        }
        else
        {
            productVm.Product = _unitOfWork.Product.Get(u => u.Id == id, "Category");
            return View(productVm);
        }
    }
    
    [HttpPost]
    public IActionResult Upsert(ProductVm productVm, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, "images", "product");

                if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                {
                    // delete old
                    var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                using (var fileStream = new FileStream(
                           Path.Combine(productPath, fileName), 
                           FileMode.Create))
                {
                     file.CopyTo(fileStream);
                }

                productVm.Product.ImageUrl = Path.Combine("/images", "product", fileName);
            }

            if (productVm.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVm.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVm.Product);
            }
            
            _unitOfWork.Save();
        
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            productVm.CategoryList = _unitOfWork.Category
                .GetAll("Category").Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
        
            return View(productVm);
        }
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound(); 
        }

        Product? productFromDb = _unitOfWork.Product.Get(i => i.Id == id, "Category");
        if (productFromDb == null)
        {
            return NotFound();
        }

        return View(productFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? productFromDb = _unitOfWork.Product.Get(i => i.Id == id, "Category");
        
        if (productFromDb == null)
        {
            return NotFound();
        }

        _unitOfWork.Product.Remove(productFromDb);
        _unitOfWork.Save();
        
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }

     
    #region apiCalls

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll("Category").ToList();

        return Json(new { data = objProductList });
    }

    #endregion
}