using EFExamples.EntityConfigurations;
using EFExamples.Schema.BookAggregate;
using EFExamples.Schema.LibraryAggregate;
using EFExamples.Schema.VisitorAggregate;
using System.Data.Entity;

namespace EFExamples
{
    public class EFExamplesContext : DbContext 
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<BookInLibrary> BooksInLibrary { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public EFExamplesContext() : base("EFExamplesContext") {

            // указываем что база данных должна быть создана если не существует.
            // существуют также DropCreateDatabaseIfModelChanges и DropCreateDatabaseAlways описанные по ссылке
            // https://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
            Database.SetInitializer(new CreateDatabaseIfNotExists<EFExamplesContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // таблица для Book Сконфигурирована с помощью DataAnnotations (смотрите код класса Book для деталей)
            // больше инфы и разных аннотаций по ссылке 
            // https://www.entityframeworktutorial.net/code-first/dataannotation-in-code-first.aspx

            // конфигурируем таблицы для Libray и BookInLibrary с помощью специфических классов - конфигураций
            modelBuilder.Configurations.Add(new LibraryEntityConfiguration());
            modelBuilder.Configurations.Add(new BookInLibraryEntityConfiguration());

            // конфигурируем таблицу для Visitor c помощью FluentAPI прям здесь
            // больше инфы по ссылке https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties
            modelBuilder.Entity<Visitor>()
                .HasMany(x => x.IssuedBooks)
                .WithRequired(x => x.Visitor)
                   .Map(cs =>
                   {
                       // переопределяем имя колонки которая будет служить как удалённый ключ от таблицы IssuedBooks 
                       // к таблице Visitors
                       cs.MapKey("VisitorId");
                   });

        }
    }
}
