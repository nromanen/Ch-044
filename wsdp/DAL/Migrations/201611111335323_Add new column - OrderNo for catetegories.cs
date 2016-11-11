namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewcolumnOrderNoforcatetegories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "OrderNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "OrderNo");
        }
    }
}
