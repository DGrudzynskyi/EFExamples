namespace EFExamples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        Author = c.String(maxLength: 256),
                        Content = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Libraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        OpenAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OpenTill = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Racks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Floor = c.Int(nullable: false),
                        Library_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .Index(t => t.Library_Id);
            
            CreateTable(
                "dbo.BooksInLibrary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ReturnedFromVisitorId = c.Int(),
                        PlacedInRackDate = c.DateTime(nullable: false),
                        IssuedToVisitorId = c.Int(),
                        IssueDate = c.DateTime(),
                        Rack_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Racks", t => t.Rack_Id)
                .Index(t => t.Rack_Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssuedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        LibraryId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        VisitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssuedBooks", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.Racks", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.BooksInLibrary", "Rack_Id", "dbo.Racks");
            DropIndex("dbo.IssuedBooks", new[] { "VisitorId" });
            DropIndex("dbo.BooksInLibrary", new[] { "Rack_Id" });
            DropIndex("dbo.Racks", new[] { "Library_Id" });
            DropIndex("dbo.Libraries", new[] { "Name" });
            DropIndex("dbo.Books", new[] { "Name" });
            DropTable("dbo.IssuedBooks");
            DropTable("dbo.Visitors");
            DropTable("dbo.BooksInLibrary");
            DropTable("dbo.Racks");
            DropTable("dbo.Libraries");
            DropTable("dbo.Books");
        }
    }
}
