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
    public class EditModel : AuthorNamePageModel
    {
        private readonly AppDbContext _appDbContext;

        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _appDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await _appDbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            PopulateAuthorsDropdownList(_appDbContext, Book.AuthorID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _appDbContext.Books.FindAsync(id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                 bookToUpdate,
                 "book",
                  s => s.ID, s => s.AuthorID, s => s.Title, s => s.PublishingHouse, s => s.ReleaseDate, s => s.NumberOfPage))
            {
                await _appDbContext.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAuthorsDropdownList(_appDbContext, bookToUpdate.AuthorID);
            return Page();
        }
    }
}
