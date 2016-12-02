namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastChangeForParserTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParserTasks", "LastChange", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParserTasks", "LastChange");
        }
    }
}
