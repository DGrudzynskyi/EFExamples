using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.VisitorAggregate
{
    public class IssuedBook
    {
        public int Id { get; set; }

        public Visitor Visitor { get; set; }

        public int BookId { get; set; }

        public int LibraryId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
