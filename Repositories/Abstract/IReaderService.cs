using BookStore.Models.Domain;
using BookStore.Models.DTO;

namespace BookStore.Repositories.Abstract
{
    public interface IReaderService
    {
        bool Add(Reader model);
        bool Update(Reader model);
        bool Delete(int id);
        Reader FindById(int id);
        IEnumerable<Reader> GetAll();

        (IEnumerable<Reader> Readers, int readerCount) GetReadersByDomain(string domain);
	}
}
