using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BookStore.Repositories.Implementation;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        public BookController(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.genreService = genreService;
        }

        //Add method
        public IActionResult Add()
        {
            var model = new Book();
			model.AuthorList = authorService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_author.ToString()
							}).ToList();

			model.GenreList = genreService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.name_genre,
							   Value = a.id_genre.ToString()
						   }).ToList();
			return View(model);
        }
        
        [HttpPost]
        public IActionResult Add(Book model)
        {
			model.AuthorList = authorService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_author.ToString(),
								Selected = a.id_author == model.id_author
							}).ToList();

			model.GenreList = genreService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.name_genre,
							   Value = a.id_genre.ToString(),
							   Selected = a.id_genre == model.id_genre
						   }).ToList();

			if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
				var result = bookService.Add(model);
				if (result)
				{
					TempData["msg"] = "Added Successfully!";
					return RedirectToAction(nameof(Add));

				}
			}
            catch (DbUpdateException ex)
            {
				if (ex.InnerException is SqlException sqlEx)
				{
					TempData["msg"] = sqlEx.Message;
				}
				else
				{
					TempData["msg"] = "An unexpected database error occurred.";
				}
			}
			
            return View(model);
        }

        //Update method
        public IActionResult Update(int id)
        {
            var model = bookService.FindById(id);
			model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = $"{a.first_name} {a.last_name}", Value = a.id_author.ToString(), Selected = a.id_author == model.id_author }).ToList();
			model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.name_genre, Value = a.id_genre.ToString(), Selected = a.id_genre == model.id_genre }).ToList();
			return View(model);
        }
        [HttpPost]
        public IActionResult Update(Book model)
        {
			model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = $"{a.first_name} {a.last_name}", Value = a.id_author.ToString(), Selected = a.id_author == model.id_author }).ToList();
			model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.name_genre, Value = a.id_genre.ToString(), Selected = a.id_genre == model.id_genre }).ToList();
			if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Update(model);
            if (result)
            {
				return RedirectToAction("GetAll");
			}
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        //Delete method
        public IActionResult Delete(int id)
        {
            var result = bookService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = bookService.GetAll();
            return View(data);
        }


		[HttpGet]
		public IActionResult SetPopularity()
		{
			var genres = genreService.GetAll()
				.Select(a => new SelectListItem
				{
					Text = a.name_genre,
					Value = a.name_genre
				}).ToList();
			ViewBag.GenreList = genres;
			return View();
		}

		[HttpPost]
		public IActionResult SetPopularity(string name_genre)
		{
			var genres = genreService.GetAll()
				.Select(a => new SelectListItem
				{
					Text = a.name_genre,
					Value = a.name_genre
				}).ToList();
			ViewBag.GenreList = genres;

			try
			{
				string resultMessage = bookService.SetPopularity(name_genre);
				TempData["msg"] = resultMessage;
			}
			catch (Exception ex)
			{
				TempData["msg"] = $"Помилка: {ex.Message}";
			}

			return View();
		}


	}
}
