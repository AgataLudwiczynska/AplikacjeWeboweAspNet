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
	public class DetailsModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public DetailsModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

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
    }
}
