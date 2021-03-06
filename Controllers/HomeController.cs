using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment5_Database.Models;
using Assignment5_Database.Models.ViewModels;

namespace Assignment5_Database.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BookRepository _repository;

        public int PageSize = 5;

        //Making sure to grab the repository class
        public HomeController(ILogger<HomeController> logger, BookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //return the data from repository class
        public IActionResult Index(string category,int page = 1)
        {
            return View(new BookListViewModel
            {
                Books = _repository.Books
                        .Where(p=>category == null|| p.Category == category)
                         .OrderBy(p => p.BookId)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalNumItems = category == null? _repository.Books.Count():
                        _repository.Books.Where(x=>x.Category==category).Count()
                },
                CurrentCategory = category
            });
        }  
             

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
