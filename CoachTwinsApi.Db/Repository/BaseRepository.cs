using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Attribute;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db.Repository
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : class
    {
        protected readonly CoachTwinsDbContext _context;
        protected readonly EntityEncryptor _encryptor;
        protected readonly DbSet<T> _dbSet;
        
        public BaseRepository(CoachTwinsDbContext context, EntityEncryptor encryptor)
        {
            _context = context;
            _encryptor = encryptor;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task<T> Get(TId id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return null;
            
            return entity;
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}