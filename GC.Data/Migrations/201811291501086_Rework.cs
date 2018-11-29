namespace GameC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rework : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "RatingId", "dbo.Ratings");
            DropForeignKey("dbo.Movies", "WriterId", "dbo.Writers");
            DropIndex("dbo.Movies", new[] { "RatingId" });
            DropIndex("dbo.Movies", new[] { "WriterId" });
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ReleaseYear = c.String(nullable: false),
                        GenreId = c.Int(),
                        RatingId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Ratings", t => t.RatingId)
                .Index(t => t.GenreId)
                .Index(t => t.RatingId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Ratings", "Value", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Ratings", "Description", c => c.String(maxLength: 400));
            DropColumn("dbo.Ratings", "RatingValue");
            DropTable("dbo.Movies");
            DropTable("dbo.Writers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 150),
                        LastName = c.String(nullable: false, maxLength: 150),
                        UserName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        ReleaseDate = c.DateTime(),
                        Country = c.String(maxLength: 300),
                        Description = c.String(maxLength: 4000),
                        RatingId = c.Int(),
                        WriterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ratings", "RatingValue", c => c.String(nullable: false, maxLength: 5));
            DropForeignKey("dbo.Games", "RatingId", "dbo.Ratings");
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropIndex("dbo.Games", new[] { "RatingId" });
            DropIndex("dbo.Games", new[] { "GenreId" });
            DropColumn("dbo.Ratings", "Description");
            DropColumn("dbo.Ratings", "Value");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            CreateIndex("dbo.Movies", "WriterId");
            CreateIndex("dbo.Movies", "RatingId");
            AddForeignKey("dbo.Movies", "WriterId", "dbo.Writers", "Id");
            AddForeignKey("dbo.Movies", "RatingId", "dbo.Ratings", "Id");
        }
    }
}
