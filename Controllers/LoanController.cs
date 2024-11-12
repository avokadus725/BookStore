using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService loanService;
        private readonly IBookService bookService;
        private readonly IReaderService readerService;
        public LoanController(ILoanService loanService, IBookService bookService, IReaderService readerService)
        {
            this.loanService = loanService;
            this.bookService = bookService;
            this.readerService = readerService;
        }

        //Add method
        public IActionResult Add()
        {
            var model = new Loan();
			model.ReaderList = readerService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_reader.ToString()
							}).ToList();

			model.BookList = bookService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.title,
							   Value = a.id_book.ToString()
						   }).ToList();
			return View(model);
        }
        
        [HttpPost]
        public IActionResult Add(Loan model)
        {
			model.ReaderList = readerService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_reader.ToString(),
                                Selected = a.id_reader == model.id_reader
							}).ToList();

			model.BookList = bookService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.title,
							   Value = a.id_book.ToString(),
                               Selected = a.id_book == model.id_book
						   }).ToList();

			if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = loanService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        //Update method
        public IActionResult Update(int id)
        {
            var model = loanService.FindById(id);
			model.ReaderList = readerService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_reader.ToString()
							}).ToList();

			model.BookList = bookService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.title,
							   Value = a.id_book.ToString()
						   }).ToList(); return View(model);
        }
        [HttpPost]
        public IActionResult Update(Loan model)
        {
			model.ReaderList = readerService.GetAll()
							.Select(a => new SelectListItem
							{
								Text = $"{a.first_name} {a.last_name}",
								Value = a.id_reader.ToString(),
								Selected = a.id_reader == model.id_reader
							}).ToList();

			model.BookList = bookService.GetAll()
						   .Select(a => new SelectListItem
						   {
							   Text = a.title,
							   Value = a.id_book.ToString(),
							   Selected = a.id_book == model.id_book
						   }).ToList();

			if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = loanService.Update(model);
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
            var result = loanService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = loanService.GetAll();
            return View(data);
        }

    }
}
