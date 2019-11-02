using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
        public IList<Scripture> Scripture1 { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
    

        public IList<Scripture> Scriptures { get; set; }
        public async Task OnGetAsync(string sortOrder, string SearchString, string SearchNote)
        {
            IQueryable<Scripture> scriptures = from s in _context.Scripture
                         select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Book.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(SearchNote))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(SearchNote));
            }

           

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            //IQueryable<Scripture> scriptures = from s in _context.Scripture1
                                          //   select s;

            switch (sortOrder)
            {
                case "name_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    scriptures = scriptures.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Date);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
            }

            Scripture = await scriptures.AsNoTracking().ToListAsync();
        }
    
    }
}
