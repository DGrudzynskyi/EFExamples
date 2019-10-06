using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.LibraryAggregate
{
    public class Library
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpenAt { get; set; }

        public DateTime OpenTill { get; set; }

        public string Address { get; set; }

        public List<Rack> Racks { get; set; }

    }
}
