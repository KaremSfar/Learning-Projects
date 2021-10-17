namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogsUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyLogs", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MyLogs", "ApplicationUserId");
            AddForeignKey("dbo.MyLogs", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyLogs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MyLogs", new[] { "ApplicationUserId" });
            DropColumn("dbo.MyLogs", "ApplicationUserId");
        }
    }
}
