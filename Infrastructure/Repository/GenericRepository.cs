using Domain;
using Domain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext dbContext;
        private DbSet<T> entities;
        public GenericRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            entities = dbContext.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entry Missing");
            }
            else
            {
                entities.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public T GetT(long id)
        {
            return entities.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entry Missing");
            }
            else
            {
                entities.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entry Missing");
            }
            else
            {
                entities.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void Savechanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entry Missing");
            }
            else
            {
                entities.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
