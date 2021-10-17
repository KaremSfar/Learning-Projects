namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldTypeAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FieldTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            DropColumn("dbo.CategoryFields", "FieldType");
            AddColumn("dbo.CategoryFields", "FieldTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CategoryFields", "FieldTypeId");
            AddForeignKey("dbo.CategoryFields", "FieldTypeId", "dbo.FieldTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryFields", "FieldTypeId", "dbo.FieldTypes");
            DropIndex("dbo.CategoryFields", new[] { "FieldTypeId" });
            DropColumn("dbo.CategoryFields", "FieldTypeId");
            DropTable("dbo.FieldTypes");
            AddColumn("dbo.CategoryFields", "FieldType", c => c.String());
        }
    }
}
