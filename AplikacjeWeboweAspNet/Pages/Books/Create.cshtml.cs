using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Data;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AplikacjeWeboweAspNet.Pages.Books
{
	public class CreateModel : AuthorNamePageModel
    {
        private readonly AppDbContext _appDbContext;

        public CreateModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult OnGet()
        {
            PopulateAuthorsDropdownList(_appDbContext);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {

            var emptyBook = new Book();

            if (await TryUpdateModelAsync<Book>(
                 emptyBook,
                 "book",  
                 s => s.ID, s => s.AuthorID, s => s.Title, s => s.PublishingHouse, s => s.ReleaseDate, s => s.NumberOfPage))
            {

                _appDbContext.Books.Add(emptyBook);
                await _appDbContext.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
          
            PopulateAuthorsDropdownList(_appDbContext, emptyBook.AuthorID);
            return Page();
        }
    }
    
}
