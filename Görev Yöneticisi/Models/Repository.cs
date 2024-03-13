using Görev_Yöneticisi.DatabaseControl;
using Görev_Yöneticisi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Görev_Yöneticisi.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly GorevYoneticisiContext _gorevYoneticisiContext;
        internal DbSet<T> set;

        public Repository(GorevYoneticisiContext dbContext)
        {
            _gorevYoneticisiContext = dbContext;
            this.set = _gorevYoneticisiContext.Set<T>();

        }

        public void Add(T entity)
        {
            set.Add(entity);
            _gorevYoneticisiContext.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> sorgu = set;
            sorgu = sorgu.Where(filter);
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = set;
            return sorgu.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> sorgu = set.Where(filter);
            return sorgu.ToList();
        }

        public void Update(T entity)
        {
            set.Update(entity);
            _gorevYoneticisiContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            set.Remove(entity);
            _gorevYoneticisiContext.SaveChanges();
        }

        public void RemoveAll(IEnumerable<T> entities)
        {
            set.RemoveRange(entities);
            _gorevYoneticisiContext.SaveChanges();
        }
    }
}
