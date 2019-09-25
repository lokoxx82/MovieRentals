namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreTypesNr2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(1,'Action')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(2,'Sci-fi')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(3,'Comedy')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(4,'Drama')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(5,'Horror')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(6,'Thriller')");
            Sql("INSERT INTO GenreTypes(Id, Name) VALUES(7,'Fable')");
            
        }
        
        public override void Down()
        {
        }
    }
}
