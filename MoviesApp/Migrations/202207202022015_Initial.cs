namespace MoviesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Director",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Director_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Director", t => t.Director_Id, cascadeDelete: true)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.MovieGenre",
                c => new
                    {
                        GenreRefId = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreRefId, t.Genre_Id })
                .ForeignKey("dbo.Movie", t => t.GenreRefId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.GenreRefId)
                .Index(t => t.Genre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Movie_Id", "dbo.Movie");
            DropForeignKey("dbo.MovieGenre", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.MovieGenre", "GenreRefId", "dbo.Movie");
            DropForeignKey("dbo.Movie", "Director_Id", "dbo.Director");
            DropIndex("dbo.MovieGenre", new[] { "Genre_Id" });
            DropIndex("dbo.MovieGenre", new[] { "GenreRefId" });
            DropIndex("dbo.Review", new[] { "Movie_Id" });
            DropIndex("dbo.Movie", new[] { "Director_Id" });
            DropTable("dbo.MovieGenre");
            DropTable("dbo.Review");
            DropTable("dbo.Movie");
            DropTable("dbo.Genre");
            DropTable("dbo.Director");
        }
    }
}
