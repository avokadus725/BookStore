using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;

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
    }
}
