using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NSAPI.Data;
using NSAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace NSAPI.Repository
{
    public class APIRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public APIRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
            await SaveAsync();

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> temp = dbset;
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> temp = dbset;
            if (tracked == true)
            {
                temp = temp.AsNoTracking();
            }
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _db.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
