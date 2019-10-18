using EFExamples.Schema.LibraryAggregate;
using EFExamples.Schema.VisitorAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Samples
{
    public static class MarkBadGuys
    {
        public static void WithoutUsingSQL()
        {
            using (var ctx = new EFExamplesContext())
            {
                // указываем что книги должны быть запрошены из базы одним запросом с посетителями
                var visitorsQuery = ctx.Visitors.Include(x => x.IssuedBooks);

                foreach (var visitor in visitorsQuery.ToList()) {
                    if(visitor.IssuedBooks.Any(x => x.IssueDate.AddMonths(3) > DateTime.Now)){
                        visitor.BadGuy = true;
                    }
                }

                // при сохранении изменений команда "Update visitors set BadGuy = true" будет вызвана столько раз,
                // сколько раз, сколько посетителей должны быть промаркированы как нарушители
                ctx.SaveChanges();
            }
        }

        public static void WithSQL()
        {
            using (var ctx = new EFExamplesContext())
            {
                // указываем что книги должны быть запрошены из базы одним запросом с посетителями

                var dateToCheck = DateTime.Now.AddMonths(-3);
                var visitorsQuery = ctx.Visitors.Where(x => x.IssuedBooks.Any(book => book.IssueDate < dateToCheck))
                    .Select(visitor => visitor.Id);

                var visitorsIds = visitorsQuery.ToList();
                
                // команда вызывается один раз для всех интересующих нас записей
                ctx.Database.ExecuteSqlCommand("Update dbo.Visitors set BadGuy = 1 where id in @p0", visitorsIds);

                ctx.SaveChanges();
            }
        }
    }
}
