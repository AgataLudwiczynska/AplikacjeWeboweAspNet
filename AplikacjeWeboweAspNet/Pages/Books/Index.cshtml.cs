using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Data;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AplikacjeWeboweAspNet.Pages.Books
{
	public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string ReleaseDateSort { get; set; }
        public string CurrentFilter { get; set; }

        public IList<Book> ListBooks { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            if (_appDbContext.Books != null)
            {
                TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
                AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
                ReleaseDateSort = sortOrder == "Date" ? "date_desc" : "Date";

                CurrentFilter = searchString;

                IQueryable<Book> booksIQ = from b in _appDbContext.Books select b;

                if (!String.IsNullOrEmpty(searchString))
                {
                    booksIQ = booksIQ.Where(b => b.Title.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "title_desc":
                        booksIQ = booksIQ.OrderByDescending(b => b.Title);
                        break;
                    case "author_desc":
                        booksIQ = booksIQ.OrderByDescending(b =>b.Author.LastName);
                        break;
                    case "Author":
                        booksIQ = booksIQ.OrderBy(b =>b.Author.LastName);
                        break;
                    case "Date":
                        booksIQ = booksIQ.OrderBy(b => b.ReleaseDate);
                        break;
                    case "date_desc":
                        booksIQ = booksIQ.OrderByDescending(b => b.ReleaseDate);
                        break;
                    default:
                        booksIQ = booksIQ.OrderBy(b => b.Title);
                        break;
                }

                ListBooks = await booksIQ.Include(c => c.Author)
                .AsNoTracking()
                .ToListAsync();
            }

        }
    }
}
