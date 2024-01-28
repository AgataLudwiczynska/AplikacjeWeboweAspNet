using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Data;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AplikacjeWeboweAspNet.Pages.Books
{
    public class AuthorNamePageModel : PageModel
    {
        public SelectList AuthorNameSL { get; set; }

        public void PopulateAuthorsDropdownList(AppDbContext _appDbContext, object selectedAuthor = null)
        {
            var authorsQuery = from d in _appDbContext.Authors
                                   orderby d.LastName // Sort by name.
                                   select d;

            AuthorNameSL = new SelectList(authorsQuery.AsNoTracking(), nameof(Author.ID), nameof(Author.LastName), selectedAuthor);
            var authors = AuthorNameSL.GetEnumerator();
        }
    }
}

