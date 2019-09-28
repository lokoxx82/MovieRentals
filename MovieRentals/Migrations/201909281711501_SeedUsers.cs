namespace MovieRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        { 

            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'114e69e8-ca85-492d-947b-c54905580f5b', N'guest@movierentals.com', 0, N'AJbNFD8uVnQh/brLWFLTHYoJSJkJOBxLe9w++PG/PAjLTdvAq32W8B73n0Zs4lOC6g==', N'1e8a4067-951b-4a5c-91f7-b7d2f39faddb', NULL, 0, 0, NULL, 1, 0, N'guest@movierentals.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7396d563-1241-4b9e-bc9b-d90b6ed29b04', N'lokoxx@gmail.com', 0, N'AAao6DGhlioe4j1t7MNZ2uNZ1mWp2lLQ/UMmB+N4EA0HUEMvxoy/GnQ0Ka5WX47hwg==', N'9f0ea0ec-3121-4bef-ba2b-d9126cbbaf92', NULL, 0, 0, NULL, 1, 0, N'lokoxx@gmail.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5b867031-68ac-445b-ac79-b5e57f3c5a51', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7396d563-1241-4b9e-bc9b-d90b6ed29b04', N'5b867031-68ac-445b-ac79-b5e57f3c5a51')
                ");

        }

    public override void Down()
        {
        }
    }
}
