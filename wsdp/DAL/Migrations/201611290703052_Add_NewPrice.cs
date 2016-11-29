namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NewPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "NewPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "NewPrice");
        }
    }
}
