using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Data;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AplikacjeWeboweAspNet.Pages.Authors
{
	public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }
        public string CurrentFilter { get; set; }

        public IList<Author> ListAuthors { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            if(_appDbContext.Authors != null)
            {
                FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
                LastNameSort = sortOrder == "LastName" ? "last_name_desc" : "LastName";

                CurrentFilter = searchString;

                IQueryable<Author> authorsIQ = from a in _appDbContext.Authors select a;

                if (!String.IsNullOrEmpty(searchString))
                {
                    authorsIQ = authorsIQ.Where(a => a.LastName.Contains(searchString) || a.FirstName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "first_name_desc":
                        authorsIQ = authorsIQ.OrderByDescending(a => a.FirstName);
                        break;
                    case "last_name_desc":
                        authorsIQ = authorsIQ.OrderByDescending(a => a.LastName);
                        break;
                    case "LastName":
                        authorsIQ = authorsIQ.OrderBy(a => a.LastName); 
                        break;
                    default:
                        authorsIQ = authorsIQ.OrderBy(a=> a.FirstName);
                        break;
                }

                ListAuthors = await authorsIQ.ToListAsync();
            }
        }
    }
}
