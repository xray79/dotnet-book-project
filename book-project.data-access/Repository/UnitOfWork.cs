using book_project.data_access.Data;
using book_project.data_access.Repository.IRepository;

namespace book_project.data_access.Repository;

public class UnitOfWork: IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}