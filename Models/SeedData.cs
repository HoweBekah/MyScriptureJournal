using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyScriptureJournal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>());
            // Look for any scriptures.
            if (context.Scripture.Any())
            {
                return;   // DB has been seeded
            }

            context.Scripture.AddRange(
                new Scripture
                {
                    Book = "Helaman",
                    Chapter = 5,
                    Verse = "12",
                    Notes = "He is the ONLY foundation that will not fall.",
                    Date = DateTime.Parse("2013-12-12")
                },
                  new Scripture
                  {
                      Book = "2 Nephi",
                      Chapter = 2,
                      Verse = "24",
                      Notes = "God knows all. He has a plan for me and I need to follow Christ to live that plan.",
                      Date = DateTime.Parse("2019-9-2")
                  },
                    new Scripture
                    {
                        Book = "Alma",
                        Chapter = 33,
                        Verse = "11",
                        Notes = "God hears us because of Jesus Christ. It is through Him that we can find joy in our afflictions.",
                        Date = DateTime.Parse("2014-7-5")
                    }
        );
            context.SaveChanges();
        }
    }
}
