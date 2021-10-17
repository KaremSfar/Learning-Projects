namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMyLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyLogs");
        }
    }
}
