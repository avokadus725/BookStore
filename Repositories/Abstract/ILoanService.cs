using BookStore.Models.Domain;
namespace BookStore.Repositories.Abstract
{
	public interface ILoanService
	{
		bool Add(Loan model);
		bool Update(Loan model);
		bool Delete(int id);
		Loan FindById(int id);
		IEnumerable<Loan> GetAll();
	}
}
