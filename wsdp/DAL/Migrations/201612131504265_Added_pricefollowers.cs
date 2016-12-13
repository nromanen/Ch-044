namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_pricefollowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceFollowers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceFollowers");
        }
    }
}
