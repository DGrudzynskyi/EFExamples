using EFExamples.Schema.LibraryAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.EntityConfigurations
{
    // класс, описывающий конфигурацию свойств обьекта BookInLibrary c помощью FluentAPI
    public class BookInLibraryEntityConfiguration : EntityTypeConfiguration<BookInLibrary>
    {
        public BookInLibraryEntityConfiguration() {

            // переоределяем имя таблицы в которой будут лежать записи BookInLibrary т.к. по умолчанию 
            // название таблицы будет BookInLibraries что нам не подходит
            this.ToTable("BooksInLibrary");

            // IsConcurrencyToken никак не влияет на столбец таблицы, созданный для поля PlacedInRackDate
            // но EF каждый раз записывая изменения с помощью SaveChanges будет проверять не изменилось ли это поле
            // относительно того, что было записано ДО на момент получения записи и если изменилось - выбрасывать исключение
            // this.Property(p => p.PlacedInRackDate).IsConcurrencyToken();
        }
    }
}
