using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YB.Data.Repository.Interface;
using YB.Data.ToDo;

namespace YB.Data.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ToDoDBContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(ToDoDBContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.AsQueryable();

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.AsQueryable().ToListAsync();

        }


        public T GetById(int id)
        {
            var res = _table.Find(id);
            return res;

        }
        public virtual IQueryable<T> QueryAll()
        {
            return Query();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _table;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        //public T GetByIdDapper(int id)
        //{
        //    var connStr = _context.Database.GetDbConnection().ConnectionString;

        //    IDbConnection conn = new SqlConnection(connStr);

        //    var procedure = "[GetById]";
        //    var values = new { id = 2};
        //    var data =  conn.Query(procedure, values, commandType: CommandType.StoredProcedure);
        //    dynamic res = data.
        //        First();

        //    return res;

        //}
        public async Task<T> GetByIdAsync(int id)
        {
            var res = await _table.FindAsync(id);
            return res;

        }
        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);

        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);

        }

        public T GetByIdDapper(int id)
        {
            throw new NotImplementedException();
        }
    }

}
