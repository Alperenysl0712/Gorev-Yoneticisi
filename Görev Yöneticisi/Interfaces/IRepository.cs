using System.Linq.Expressions;

namespace Görev_Yöneticisi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void RemoveAll(IEnumerable<T> entities);

    }
}
