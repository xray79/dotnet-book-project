using book_project.data_access.Data;
using book_project.data_access.Repository.IRepository;
using book_project.models;
using Microsoft.EntityFrameworkCore;

namespace book_project.data_access.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category obj)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}