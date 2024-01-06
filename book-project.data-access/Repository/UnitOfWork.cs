using book_project.data_access.Data;
using book_project.data_access.Repository.IRepository;
using book_project.models;

namespace book_project.data_access.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        Company = new CompanyRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}