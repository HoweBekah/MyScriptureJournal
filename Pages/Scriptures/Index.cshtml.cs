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
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
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
        public string CurrentSort { get; set; }

        public IList<Scripture> Scriptures { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            var scriptures = from m in _context.Scripture
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Book.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(SearchNote))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(SearchNote));
            }

            Scripture = await scriptures.ToListAsync();

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> JournalInfo = from s in _context.Scripture
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    JournalInfo = JournalInfo.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    JournalInfo = JournalInfo.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    JournalInfo = JournalInfo.OrderByDescending(s => s.Date);
                    break;
                default:
                    JournalInfo = JournalInfo.OrderBy(s => s.Book);
                    break;
            }

            Scripture= await JournalInfo.AsNoTracking().ToListAsync();
        }
    
    }
}
