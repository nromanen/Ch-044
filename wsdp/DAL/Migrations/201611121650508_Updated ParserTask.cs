namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedParserTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParserTasks", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.ParserTasks", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.ParserTasks", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParserTasks", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ParserTasks", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.ParserTasks", "Priority", c => c.String(nullable: false));
        }
    }
}
