namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypesTableValues : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes set Name = (CASE WHEN Id = 1 THEN 'Pay as You Go' WHEN Id = 2 THEN 'Monthly' WHEN Id = 3 THEN 'Quarterly' WHEN Id = 4 THEN 'Annual' END)");
            Sql("UPDATE MembershipTypes set DiscountRate = (CASE WHEN Id = 1 THEN '0' WHEN Id = 2 THEN '10' WHEN Id = 3 THEN '15' WHEN Id = 4 THEN '20' END)");
            Sql("UPDATE MembershipTypes set DurationInMonths = (CASE WHEN Id = 1 THEN '0' WHEN Id = 2 THEN '1' WHEN Id = 3 THEN '3' WHEN Id = 4 THEN '12' END)");
        }
        
        public override void Down()
        {
        }
    }
}
