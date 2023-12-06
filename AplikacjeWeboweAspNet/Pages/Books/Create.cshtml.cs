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
	public class CreateModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public CreateModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _appDbContext.Books == null || Book == null)
            {
                return Page();
            }

            _appDbContext.Books.Add(Book);
            await _appDbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    
}
