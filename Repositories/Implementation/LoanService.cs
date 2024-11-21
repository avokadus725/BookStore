using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly DatabaseContext context;
        public LoanService(DatabaseContext context)
        {
            this.context = context;
        }
		public bool Add(Loan model)
		{
			context.Loans.Add(model);
			context.SaveChanges();
			return true;
		}


		public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;

                context.Loans.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Loan FindById(int id)
        {
            return context.Loans.Find(id);
        }

        public IEnumerable<Loan> GetAll()
        {
            var data = (from Loan in context.Loans 
                        join Reader in context.Readers on Loan.id_reader equals Reader.id_reader
                        join Book in context.Books on Loan.id_book equals Book.id_book
                        select new Loan
                        {
                            id_loan = Loan.id_loan,
							id_book = Book.id_author,
							id_reader = Reader.id_reader,
							loan_date = Loan.loan_date,
                            return_date = Loan.return_date,
							title = Book.title,
							first_name = Reader.first_name,
							last_name = Reader.last_name
						}
						).ToList();
            return data;
        }

        public bool Update(Loan model)
        {
            try
            {
                context.Loans.Update(model);
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
