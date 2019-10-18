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
    public static class VisitorSearchForBookAndTakeIt
    {

        public static void DoItTwoQueries() {
            using (var ctx = new EFExamplesContext()) {
                var visitor = new Visitor() {
                    Name = "Oleg",
                    IssuedBooks = new List<IssuedBook>(),
                };

                var currentDate = DateTime.Now;
                // посетитель смотрит какие книги есть в наличии в библиотеке
                var booksQuery = ctx.Libraries.Include(x => x.Racks)
                    // только в библиотеке Big Library
                    .Where(x => x.Name == "Big library")
                    // выбираем смотрим на все реки и из каждого выбираем группу обьектов
                    .SelectMany(x => x.Racks.SelectMany(rack => rack.Books))
                    .Where(x => x.IssueDate == null);

                // терминальный оператор, по вызову которого запрос отправляется в базу и мы таки получаем книги
                var booksInLibrary = booksQuery.ToList();
                var presentBookIds = booksInLibrary.Select(x => x.BookId).ToList();

                // запрашиваем названия и авторов только книг в наличии в библиотеке
                var booksPresentQuery = ctx.Books.Where(x => presentBookIds.Contains(x.Id));

                // где то здесь посетитель решает что ему надо и выбирает первую книгу (для простоты)
                var booksPresent = booksPresentQuery.ToList();
                var bookOfIntereset = booksPresent.First();

                visitor.IssuedBooks.Add(new IssuedBook()
                {
                    BookId = bookOfIntereset.Id,
                    IssueDate = DateTime.Now,
                    LibraryId = ctx.Libraries.Where(x => x.Name == "Big library").SingleOrDefault().Id,
                });

                ctx.Visitors.Add(visitor);

                // сохраняем изменения потому что айди посетителя автогенерируется базой
                ctx.SaveChanges();

                // сохраняем запись в библиотеке, которая покажет что книгу забрали
                // сначала находим запись о книге в библиотеке
                var bookInLibrary = booksInLibrary.FirstOrDefault(x => x.BookId == bookOfIntereset.Id);

                // проверяем что ентити трекается контекстом
                var state = ctx.Entry(bookInLibrary).State;

                // здесь энтити фреймворк автоматически трекает изменение полей
                bookInLibrary.IssueDate = DateTime.Now;
                bookInLibrary.IssuedToVisitorId = visitor.Id;

                // проверяем все ентити, которые были изменены
                var changes = ctx.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

                ctx.SaveChanges();
            }
        }

        public static void DoItSingleQuery()
        {
            using (var ctx = new EFExamplesContext())
            {
                var visitor = new Visitor()
                {
                    Name = "Oleg",
                    IssuedBooks = new List<IssuedBook>(),
                };

                // посетитель смотрит какие книги есть в наличии в библиотеке
                var booksQuery = ctx.Libraries
                    // только в библиотеке Big Library
                    .Where(x => x.Name == "Big library")
                    // выбираем смотрим на все реки и из каждого выбираем группу обьектов
                    .SelectMany(x => x.Racks.SelectMany(rack =>
                    // явно указываем поля, которые хотим выбрать
                    rack.Books.Select(book => new { book.Id, book.BookId, book.IssueDate, book.Rack, book.PlacedInRackDate })));

                // запрос на получение книг в библиотеке в связке с книгами
                var query2 = ctx.Books.Join(booksQuery,
                    book => book.Id,
                    bookInLibrary => bookInLibrary.BookId,
                    (book, bookInLibrary) => new { book, bookInLibrary })
                    .Where(x => x.bookInLibrary.IssueDate == null);

                var presentBooks = query2.ToList();

                // где то здесь посетитель решает что ему надо и выбирает первую книгу (для простоты)
                var bookOfIntereset = presentBooks.First();

                visitor.IssuedBooks.Add(new IssuedBook()
                {
                    BookId = bookOfIntereset.book.Id,
                    IssueDate = DateTime.Now,
                    LibraryId = ctx.Libraries.Where(x => x.Name == "Big library").SingleOrDefault().Id,
                });

                // сохраняем изменения потому что айди посетителя автогенерируется базой
                ctx.SaveChanges();

                // этот обьект создан вручную и НЕ трекается ChangeTracker'om ентити фреймворка
                var bookInLibraryToModify = new BookInLibrary() {
                    Id = bookOfIntereset.bookInLibrary.Id,
                    BookId = bookOfIntereset.bookInLibrary.BookId,
                    IssueDate = DateTime.Now,
                    IssuedToVisitorId = visitor.Id,
                    Rack = bookOfIntereset.bookInLibrary.Rack,
                    PlacedInRackDate = bookOfIntereset.bookInLibrary.PlacedInRackDate,
                };

                // проверяем:
                var state = ctx.Entry(bookInLibraryToModify).State;

                ctx.BooksInLibrary.Attach(bookInLibraryToModify);
                               
                // проверяем после аттача:
                state = ctx.Entry(bookInLibraryToModify).State;

                ctx.Entry(bookInLibraryToModify).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        // для запуска этого примера нужно раскоментировать указанные строки в файлe BookInLibrary, BookInLibraryEntityConfiguration
        // после чего создать миграцию и запустить её (это нужно для создания удалённых ключей в базе данных)
        /*public static void DoItLazyLoading()
        {

            using (var ctx = new EFExamplesContext())
            {
                var visitor = new Visitor()
                {
                    Name = "Oleg",
                    IssuedBooks = new List<IssuedBook>(),
                };

                var currentDate = DateTime.Now;
                // посетитель смотрит какие книги есть в наличии в библиотеке
                var booksQuery = ctx.Libraries.Include(x => x.Racks)
                    // только в библиотеке Big Library
                    .Where(x => x.Name == "Big library")
                    // выбираем смотрим на все реки и из каждого выбираем группу обьектов
                    .SelectMany(x => x.Racks.SelectMany(rack => rack.Books))
                    .Where(x => x.IssueDate == null);

                // терминальный оператор, по вызову которого запрос отправляется в базу и мы таки получаем книги
                var booksInLibrary = booksQuery.ToList();

                foreach (var bookInLibrary in booksInLibrary)
                {
                    // в строке ниже происходит SQL запрос, необходимый для создания обьекта типа Book, загружаемого как ассоциация
                    // обьекта bookInLibrary с помощью lazy loading
                    var visitorLikeThisBook = bookInLibrary.Book.Name.ToLowerInvariant().Contains("lord");
                    if (visitorLikeThisBook)
                    {
                        visitor.IssuedBooks.Add(new IssuedBook()
                        {
                            // при повторном обращении к bookInLibrary.Book - запись не будет заново загружена из базы данных
                            // т.к. свойство bookInLibrary.Book уже инициализировано
                            BookId = bookInLibrary.Book.Id,
                            IssueDate = DateTime.Now,
                            LibraryId = ctx.Libraries.Where(x => x.Name == "Big library").SingleOrDefault().Id,
                        });

                        bookInLibrary.IssueDate = DateTime.Now;
                        // здесь нам не нужно сохранять посетителя "до" сохранения книги.
                        // EF понимает что посетитель только создаётся и его айди будет получин ДО обновления книги
                        bookInLibrary.IssuedToVisitor = visitor;
                        break;
                    }
                }

                ctx.SaveChanges();
            }
        }*/
    }
}
