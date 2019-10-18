using EFExamples.Schema.BookAggregate;
using EFExamples.Schema.VisitorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.LibraryAggregate
{
    public class BookInLibrary
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        // раскоментируйте, создайте миграцию для создания удалённого ключа, после чего
        // можете посмотреть на пример VisitorSearchForBookAndTakeIt.DoItLazyLoading
        // public virtual Book Book {get;set;}

        public Rack Rack { get; set; }

        public int? ReturnedFromVisitorId { get; set; }

        public DateTime PlacedInRackDate { get; set; }

        public int? IssuedToVisitorId { get; set; }

        // раскоментируйте, создайте миграцию для создания удалённого ключа, после чего
        // можете посмотреть на пример VisitorSearchForBookAndTakeIt.DoItLazyLoading
        // public virtual Visitor IssuedToVisitor { get; set; }

        public DateTime? IssueDate { get; set; }


    }
}
