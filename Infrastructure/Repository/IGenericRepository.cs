using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
       public interface IGenericRepository<T> where T : BaseEntity
        {
            IEnumerable<T> GetAll();
            T GetT(long id);

            void Insert(T entity);
            void Update(T entity);
            void Delete(T entity);
            void Remove(T entity);
            void Savechanges();
        }
    }



