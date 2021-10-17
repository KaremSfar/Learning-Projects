namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipData : DbMigration
    {
        public override void Up()
        {
            Sql("Update Memberships SET Name='Pay As You Go' where id=1");
            Sql("Update Memberships SET Name='Monthly' where id=2");
            Sql("Update Memberships SET Name='Trimestrial' where id=3");
            Sql("Update Memberships SET Name='Yearly' where id=4");
        }
        
        public override void Down()
        {
        }
    }
}
