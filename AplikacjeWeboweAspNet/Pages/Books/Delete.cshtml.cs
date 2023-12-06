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
	public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public DeleteModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _appDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            Book = book;
            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _appDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await _appDbContext.Books.FindAsync(id);

            if(book != null)
            {
                Book = book;
                _appDbContext.Books.Remove(Book);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
