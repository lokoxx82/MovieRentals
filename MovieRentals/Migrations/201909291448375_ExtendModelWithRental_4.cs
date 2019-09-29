namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendModelWithRental_4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "MovieI_Id", newName: "Movie_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_MovieI_Id", newName: "IX_Movie_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_Id", newName: "IX_MovieI_Id");
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "MovieI_Id");
        }
    }
}
