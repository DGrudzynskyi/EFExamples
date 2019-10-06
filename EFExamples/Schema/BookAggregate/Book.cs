using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Schema.BookAggregate
{
    // Для книги конфигурируем свойства с помощью DataAnnotations, не рекомендую так делать в реальном коде 
    // Из за нарушения SingleResponsibility
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Author { get; set; }

        [Column(TypeName = "NVARCHAR")]
        public string Content { get; set; }
    }
}
