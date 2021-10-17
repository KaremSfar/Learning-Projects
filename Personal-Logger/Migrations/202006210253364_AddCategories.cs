namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldName = c.String(),
                        FieldType = c.String(),
                        LogCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogCategories", t => t.LogCategory_Id,cascadeDelete:true)
                .Index(t => t.LogCategory_Id);
            
            AddColumn("dbo.MyLogs", "LogCategoryId", c => c.Int());
            CreateIndex("dbo.MyLogs", "LogCategoryId");
            AddForeignKey("dbo.MyLogs", "LogCategoryId", "dbo.LogCategories", "Id",cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyLogs", "LogCategoryId", "dbo.LogCategories");
            DropForeignKey("dbo.CategoryFields", "LogCategoryId", "dbo.LogCategories");
            DropIndex("dbo.MyLogs", new[] { "LogCategoryId" });
            DropIndex("dbo.CategoryFields", new[] { "LogCategoryId" });
            DropColumn("dbo.MyLogs", "LogCategoryId");
            DropTable("dbo.CategoryFields");
            DropTable("dbo.LogCategories");
        }
    }
}
