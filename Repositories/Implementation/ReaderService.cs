using BookStore.Models.Domain;
using BookStore.Models.DTO;
using BookStore.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class ReaderService : IReaderService
    {
        private readonly DatabaseContext context;
        public ReaderService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Reader model)
        {
            try
            {
                context.Readers.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;

                context.Readers.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Reader FindById(int id)
        {
            return context.Readers.Find(id);
        }

        public IEnumerable<Reader> GetAll()
        {
            return context.Readers.ToList();
        }

        public bool Update(Reader model)
        {
            try
            {
                context.Readers.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public (IEnumerable<Reader> Readers, int readerCount) GetReadersByDomain(string domain)
        {
            try
            {
                var parameter = new SqlParameter("@domain", domain);
                var readers = context.Readers
                    .FromSqlRaw("SELECT * FROM dbo.get_readers_by_domain(@domain)", parameter)
                    .ToList();

                var readerCount = context.Database
                    .ExecuteSqlRaw("SELECT dbo.scalar_get_readers_by_domain({0})", domain);

                return (readers, readerCount);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                throw new Exception("Error calling database functions: " + ex.Message);
            }
        }

    }
}
