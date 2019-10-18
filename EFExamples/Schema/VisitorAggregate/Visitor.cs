using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.VisitorAggregate
{
    public class Visitor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IssuedBook> IssuedBooks { get; set; }

        public bool BadGuy { get; set; }
    }
}
