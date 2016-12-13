using Microsoft.AspNetCore.Mvc;

using ASPNet_Core_ConnectedServices.Models;
using ASPNet_Core_ConnectedServices.Repositories;


namespace ASPNet_Core_ConnectedServices.Controllers
{
    public class TableStorageController : Controller
    {
        ITableRepositories serv;
        public TableStorageController(ITableRepositories s)
        {
            serv = s;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var books = serv.GetBooks();
            return View(books);
        }

        public IActionResult Create()
        {
            var book = new Book();
            return View(book);
        }

        [HttpPost]
        public IActionResult Create(Book bk)
        {
            serv.CreateBook(bk);
            return RedirectToAction("Index");
        }

    }
}
