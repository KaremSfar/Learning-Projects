namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoriesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogCategories", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.LogCategories", "ApplicationUserId");
            AddForeignKey("dbo.LogCategories", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogCategories", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.LogCategories", new[] { "ApplicationUserId" });
            DropColumn("dbo.LogCategories", "ApplicationUserId");
        }
    }
}
