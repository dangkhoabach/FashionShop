namespace ClothStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressColumnToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
