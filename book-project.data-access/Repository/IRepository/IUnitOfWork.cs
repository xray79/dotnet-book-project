namespace book_project.data_access.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }

    void Save();
}