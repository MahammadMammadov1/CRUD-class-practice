using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using PustokSliderCRUD.Models;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _appDb;

        public BookController(AppDbContext appDb)
        {
            _appDb = appDb;
        }
        public IActionResult Index()
        {
           
            var book = _appDb.Books.ToList();
            return View(book);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _appDb.Authors.ToList();
            ViewBag.Genres = _appDb.Genres.ToList();
            ViewBag.Tags = _appDb.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _appDb.Authors.ToList();
            ViewBag.Genres = _appDb.Genres.ToList();
            if (!ModelState.IsValid) return View(book);
            if (!_appDb.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!!!");
                return View();
            }
            if (!_appDb.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not found!!!");
                return View();
            }
            var check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_appDb.Tags.Any(x => x.Id == tagId))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(book);
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = book,
                            TagId = tagId
                        };
                        _appDb.BookTags.Add(bookTag);
                    }
                }
            }

            _appDb.Books.Add(book);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Authors = _appDb.Authors.ToList();
            ViewBag.Genres = _appDb.Genres.ToList();
            ViewBag.Tags = _appDb.Tags.ToList();

            if (!ModelState.IsValid) return View();
            var existBook = _appDb.Books.FirstOrDefault(x => x.Id == id);
            return View(existBook);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            ViewBag.Authors = _appDb.Authors.ToList();
            ViewBag.Genres = _appDb.Genres.ToList();

            var existBook = _appDb.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existBook == null) return NotFound();
            if (!ModelState.IsValid) return View(book);
            if (!_appDb.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!!!");
                return View();
            }
            if (!_appDb.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not found!!!");
                return View();
            }

            var check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_appDb.Tags.Any(x => x.Id == tagId))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(book);
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = book,
                            TagId = tagId
                        };
                        _appDb.BookTags.Add(bookTag);
                    }
                }
            }
            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.CostPrice = book.CostPrice;
            existBook.DiscountedPrice = book.DiscountedPrice;
            existBook.Code = book.Code;
            existBook.SalePrice = book.SalePrice;
            existBook.Tax = book.Tax;
            existBook.IsAvailable = book.IsAvailable;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _appDb.Books.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            var wanted = _appDb.Books.FirstOrDefault(x => x.Id == book.Id);
            if (wanted == null) return NotFound();
            _appDb.Books.Remove(wanted);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
