namespace Sunergizer_API.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(string? id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
