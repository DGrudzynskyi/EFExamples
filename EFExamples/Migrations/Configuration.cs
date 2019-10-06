namespace EFExamples.Migrations
{
    using EFExamples.Schema.BookAggregate;
    using EFExamples.Schema.LibraryAggregate;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFExamples.EFExamplesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EFExamples.EFExamplesContext context)
        {
            //  этот метод вызывается после запуска миграций для набивки базы значениеми по умолчанию


            var sapiens = new Book() {
                Id = 1,
                Name = "Sapiens",
                Author = "Yuval Noah Harari",
                Content = "Что то там про историю человечества",
            };

            var lordOfTheRings = new Book()
            {
                Id = 2,
                Name = "The Lord of the rings",
                Author = "John R.R. Tolkien",
                Content = "Что то там про историю нелюдей",
            };

            var mathSkanavi = new Book()
            {
                Id = 3,
                Name = "Сборник задач по математике",
                Author = "Mark Skanavi",
                Content = "Что то там про задачи и математику",
            };


            context.Books.AddOrUpdate(sapiens);
            context.Books.AddOrUpdate(lordOfTheRings);
            context.Books.AddOrUpdate(mathSkanavi);

            var currentDate = DateTime.Now;

            context.Libraries.AddOrUpdate(new Library()
            {
                Id = 1,
                Name = "Big library",
                Address = "Kiev",
                OpenAt = new DateTime(2010, 1, 1, 8, 0, 0),
                OpenTill = new DateTime(2010, 1, 1, 16, 0, 0),
                Racks = new List<Rack>() {
                    new Rack(){
                        Id = 1,
                        Floor = 1,
                        Books = new List<BookInLibrary>(){
                            new BookInLibrary() {
                                Id = 1,
                                BookId = sapiens.Id,
                                PlacedInRackDate = currentDate,
                            },
                            new BookInLibrary() {
                                Id = 2,
                                BookId = sapiens.Id,
                                PlacedInRackDate = currentDate,
                            },
                            new BookInLibrary() {
                                Id = 3,
                                BookId = sapiens.Id,
                                PlacedInRackDate = currentDate,
                            },
                            new BookInLibrary() {
                                Id = 4,
                                BookId = lordOfTheRings.Id,
                                PlacedInRackDate = currentDate,
                            }
                        }
                    },
                    new Rack() {
                        Id = 2,
                        Floor = 2,
                        Books = new List<BookInLibrary>() {
                            new BookInLibrary() {
                                Id = 5,
                                BookId = lordOfTheRings.Id,
                                PlacedInRackDate = currentDate,
                            }
                        }
                    }
                }
            });

            context.Libraries.AddOrUpdate(new Library()
            {
                Id = 1,
                Name = "Small library",
                Address = "}|{merinka",
                OpenAt = new DateTime(2010, 1, 1, 6, 30, 0),
                OpenTill = new DateTime(2010, 1, 1, 15, 30, 0),
                Racks = new List<Rack>() {
                    new Rack(){
                        Id = 3,
                        Floor = 1,
                        Books = new List<BookInLibrary>(){
                            new BookInLibrary() {
                                Id = 6,
                                BookId = mathSkanavi.Id,
                                PlacedInRackDate = currentDate,
                            },
                            new BookInLibrary() {
                                Id = 7,
                                BookId = mathSkanavi.Id,
                                PlacedInRackDate = currentDate,
                            },
                        }
                    },
                    new Rack() {
                        Id = 4,
                        Floor = 1,
                        Books = new List<BookInLibrary>() {
                            new BookInLibrary() {
                                Id = 8,
                                BookId = mathSkanavi.Id,
                                PlacedInRackDate = currentDate,
                            },
                            new BookInLibrary() {
                                Id = 9,
                                BookId = mathSkanavi.Id,
                                PlacedInRackDate = currentDate,
                            }
                        }
                    }
                }
            });

        }
    }
}
