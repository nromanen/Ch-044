namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutingInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ParserTaskId = c.Int(nullable: false),
                        GoodUrl = c.String(nullable: false),
                        ErrorMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParserTasks", t => t.ParserTaskId, cascadeDelete: true)
                .Index(t => t.ParserTaskId);
            
            CreateTable(
                "dbo.PriceHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExecutingInfoes", "ParserTaskId", "dbo.ParserTasks");
            DropIndex("dbo.ExecutingInfoes", new[] { "ParserTaskId" });
            DropTable("dbo.PriceHistories");
            DropTable("dbo.ExecutingInfoes");
        }
    }
}
