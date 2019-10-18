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


            // раскоментируйте строки ниже для демонстрации примера с lazy loading
            //this.HasRequired(x => x.Book).WithMany().HasForeignKey(x => x.BookId);
            //this.HasOptional(x => x.IssuedToVisitor).WithMany().HasForeignKey(x => x.IssuedToVisitorId);
        }
    }
}
