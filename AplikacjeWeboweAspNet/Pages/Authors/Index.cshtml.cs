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

        public IList<Author> ListAuthors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if(_appDbContext.Authors != null)
            {
                ListAuthors = await _appDbContext.Authors.ToListAsync();
            }
        }
    }
}
