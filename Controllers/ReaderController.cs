using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Domain;
using BookStore.Repositories.Implementation;

namespace BookStore.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService service;
        public ReaderController(IReaderService service)
        {
            this.service = service;
        }

        //Add method
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(Reader model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
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
            var record = service.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Reader model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
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
            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }

		public IActionResult GetReadersByDomain()
		{
			return View();
		}

		[HttpPost]
        public IActionResult GetReadersByDomain(string domain)
        {
            var result = service.GetReadersByDomain(domain);

            ViewBag.readerCount = result.readerCount;

            return View(result.Readers);
        }


    }
}
