namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ExecutingInfo : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExecutingInfoes", "ParserTaskId", "dbo.ParserTasks");
            DropIndex("dbo.ExecutingInfoes", new[] { "ParserTaskId" });
            DropTable("dbo.ExecutingInfoes");
        }
    }
}
