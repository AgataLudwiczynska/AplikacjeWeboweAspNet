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
	public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public DeleteModel(AppDbContext appDbContext)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null || _appDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await _appDbContext.Authors.FindAsync(id);

            if(author != null)
            {
                Author = author;
                _appDbContext.Authors.Remove(Author);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
       
    }
}
