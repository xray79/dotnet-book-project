using book_project.data_access.Data;
using book_project.data_access.Repository.IRepository;
using book_project.models;

namespace book_project.data_access.Repository;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _db;
    
    public CompanyRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Company obj)
    {
        _db.Companies.Update(obj);
    }
}