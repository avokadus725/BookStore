using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
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
                        select new Book
                        {
                            id_book = book.id_book,
							id_author = author.id_author,
							id_genre = genre.id_genre,
							title = book.title,
                            published_year = book.published_year,
							description_book = book.description_book,
							name_genre = genre.name_genre,
							last_name = author.last_name
						}
						).ToList();
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
    }
}
