using book_project.models;

namespace book_project.data_access.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}