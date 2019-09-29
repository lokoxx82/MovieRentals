namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendModelWithRental_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "Customer_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "MovieI_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime());
            CreateIndex("dbo.Rentals", "Customer_Id");
            CreateIndex("dbo.Rentals", "MovieI_Id");
            AddForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "MovieI_Id", "dbo.Movies", "Id", cascadeDelete: true);
            DropColumn("dbo.Rentals", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rentals", "MovieI_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "MovieI_Id" });
            DropIndex("dbo.Rentals", new[] { "Customer_Id" });
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "MovieI_Id");
            DropColumn("dbo.Rentals", "Customer_Id");
        }
    }
}
