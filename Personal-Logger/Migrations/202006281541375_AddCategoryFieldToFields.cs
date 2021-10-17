namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryFieldToFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fields", "CategoryFieldId", c => c.Int(nullable: false));
            CreateIndex("dbo.Fields", "CategoryFieldId");
            AddForeignKey("dbo.Fields", "CategoryFieldId", "dbo.CategoryFields", "Id");
            DropColumn("dbo.Fields", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fields", "Name", c => c.String());
            DropForeignKey("dbo.Fields", "CategoryFieldId", "dbo.CategoryFields");
            DropIndex("dbo.Fields", new[] { "CategoryFieldId" });
            DropColumn("dbo.Fields", "CategoryFieldId");
        }
    }
}
