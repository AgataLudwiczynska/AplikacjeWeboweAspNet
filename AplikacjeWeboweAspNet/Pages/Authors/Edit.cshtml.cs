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
	public class EditModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _appDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await _appDbContext.Authors.FirstOrDefaultAsync(a => a.ID == id);
            if(author == null)
            {
                return NotFound();
            }
            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _appDbContext.Attach(Author).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool AuthorExists(int id)
        {
            return (_appDbContext.Authors?.Any(a => a.ID == id)).GetValueOrDefault();
        }
    }
}
