namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendModelWithRental_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        DateRented = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rentals");
        }
    }
}
