using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YB.Data.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetByIdDapper(int id);
        Task<T> GetByIdAsync(int id);
        void Insert(T obj);
        void InsertRange(IEnumerable<T> entities);
        void Update(T obj);
        //void Delete(object id);
        Task SaveAsync();

        void Delete(int Id);
        void DeleteRange(IEnumerable<T> entities);

        IQueryable<T> QueryAll();

        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        //DatabaseFacade Database { get; }
    }

}
