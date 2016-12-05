namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_NewPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goods", "NewPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "NewPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
