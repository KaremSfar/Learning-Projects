namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'042fe5ee-d2fe-4b74-99c0-75d4ea64a4fa', N'admin@sfar.com', 0, N'ALWJGB57TEeH9Lkls/GueogHSaYHa36E1Q4/VgK3OWnCvVfcZmzDejBfoWeFGSSNfw==', N'0d527a4c-f830-4138-a47e-87f7b8af40e0', NULL, 0, 0, NULL, 1, 0, N'admin@sfar.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cf314960-6982-4cf1-8e31-039d013c3369', N'guest@sfar.com', 0, N'AJodjJZnbVHRXY/Y/kQJSoOx/OflGLDbEYut+Zy7wRmvJkSUBa24l5FezHbzqv6hOg==', N'13189d9b-71f2-47d1-aefa-bf74be53c6d3', NULL, 0, 0, NULL, 1, 0, N'guest@sfar.com')");

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'aaf32461-e0db-4265-9a38-d588651ad900', N'CanManageMovies')");

            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'042fe5ee-d2fe-4b74-99c0-75d4ea64a4fa', N'aaf32461-e0db-4265-9a38-d588651ad900')");

            //Admin pwd is Kervekok2136$
        }
        
        public override void Down()
        {
        }
    }
}
