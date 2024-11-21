using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Humanizer.Localisation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;
        public BookService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Book model)
        {
            try
            {
                context.Books.Add(model);
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

                context.Books.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book FindById(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            var data = (from book in context.Books
                        join author in context.Authors on book.id_author equals author.id_author
                        join genre in context.Genres on book.id_genre equals genre.id_genre
						join loan in context.Loans on book.id_book equals loan.id_book into bookLoans
						from loan in bookLoans.DefaultIfEmpty()
						group loan by new
						{
							book.id_book,
							book.id_author,
							book.id_genre,
							book.title,
							book.published_year,
							book.description_book,
							genre.name_genre,
							author.first_name,
							author.last_name
						} into bookGroup
						select new Book
						{
							id_book = bookGroup.Key.id_book,
							id_author = bookGroup.Key.id_author,
							id_genre = bookGroup.Key.id_genre,
							title = bookGroup.Key.title,
							published_year = bookGroup.Key.published_year,
							description_book = bookGroup.Key.description_book,
							name_genre = bookGroup.Key.name_genre,
							first_name = bookGroup.Key.first_name,
							last_name = bookGroup.Key.last_name,
							LoanCount = bookGroup.Count(loan => loan != null) 
						}).ToList();
			return data;
        }

        public bool Update(Book model)
        {
            try
            {
                context.Books.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
		public string SetPopularity(string name_genre)
		{
			var messages = new List<string>();

			try
			{
				var parameter = new SqlParameter("@genre", name_genre);
				context.Database.ExecuteSqlRaw("EXEC set_popularity_status @genre", parameter);
			}
			catch (Microsoft.Data.SqlClient.SqlException ex)
			{
				messages.Add(ex.Message);
			}

			return messages.Any() ? string.Join(Environment.NewLine, messages) : "Операція виконана успішно";
		}

	}
}
