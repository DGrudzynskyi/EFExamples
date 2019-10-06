using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.LibraryAggregate
{
    public class Rack
    {
        public int Id { get; set; }

        public int Floor { get; set; }

        public Library Library { get; set; }

        public List<BookInLibrary> Books { get; set; }
    }
}
