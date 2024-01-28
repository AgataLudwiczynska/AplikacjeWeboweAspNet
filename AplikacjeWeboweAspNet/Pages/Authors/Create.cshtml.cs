using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Data;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AplikacjeWeboweAspNet.Pages.Authors
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
        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid || _appDbContext.Authors == null || Author == null)
            {        
                return Page();
            }

            _appDbContext.Authors.Add(Author);
            await _appDbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
