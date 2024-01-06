using book_project.data_access.Repository.IRepository;
using book_project.models;
using book_project.models.ViewModels;
using book_project.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace book_project.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    { 
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        
        return View(objCompanyList);
    }

    [HttpGet]
    public IActionResult Upsert(int? id ) // Update + insert
    {
        if (id is null or 0) 
        {
            return View(new Company());
        }
        else
        {
            Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
            return View(companyObj);
        }
    }
    
    [HttpPost]
    public IActionResult Upsert(Company companyObj)
    {
        if (ModelState.IsValid)
        {
            

            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Add(companyObj);
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);
            }
            
            _unitOfWork.Save();
        
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return View(companyObj);
        }
    }
     
    #region apiCalls

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = objCompanyList });
    }
    
    public IActionResult Delete(int? id)
    {
        Company CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
        if (CompanyToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Company.Remove(CompanyToBeDeleted);
        _unitOfWork.Save();
        
        return Json(new { success = true, message = "Delete Successful" });
    }

    #endregion
}