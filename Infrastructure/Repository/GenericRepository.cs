using Domain;
using Domain.EntityFramework;
using Domain.Model.Login;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext dbContext;
        private DbSet<T> entities;
        private static string connectionString = "Server=MLBBTL-108966\\SQLEXPRESS;DataBase=ShoppingApp;Integrated Security=true";



        public GenericRepository(ApplicationDbContext _dbContext, IConfiguration config)
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

        public bool Login(UserLogin userLogin)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (connection)
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand("FetchCustomerLoginRecord", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Email", userLogin.EmailId);
                    cmd.Parameters.AddWithValue("Password", userLogin.Password);
                    var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    UserLogin customer = new UserLogin();
                    SqlDataReader rd = cmd.ExecuteReader();
                    var result = returnParameter.Value;

                    if (result != null && result.Equals(2))
                    {
                        throw new Exception("Email not registered");
                    }
                    return true;
                }
            }
        }
    }
}
