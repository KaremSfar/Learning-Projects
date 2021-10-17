namespace PersonalLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateFieldTypes : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into FieldTypes(TypeName) Values ('Int')");
            Sql("Insert into FieldTypes(TypeName) Values ('String')");
            Sql("Insert into FieldTypes(TypeName) Values ('Decimal')");
            Sql("Insert into FieldTypes(TypeName) Values ('TimeSpan')");
        }
        
        public override void Down()
        {
        }
    }
}
