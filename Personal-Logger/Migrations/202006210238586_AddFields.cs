namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Decimal(precision: 18, scale: 2),
                        Value1 = c.Int(),
                        Value2 = c.String(),
                        Value3 = c.Time(precision: 7),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        MyLog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MyLogs", t => t.MyLog_Id, cascadeDelete: true)
                .Index(t => t.MyLog_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fields", "MyLog_Id", "dbo.MyLogs");
            DropIndex("dbo.Fields", new[] { "MyLog_Id" });
            DropTable("dbo.Fields");
        }
    }
}
