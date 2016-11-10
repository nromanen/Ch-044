namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class fix_name_and_columns_for_parser_task : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Parsers", newName: "ParserTasks");
            AlterColumn("dbo.ParserTasks", "IteratorSettings", c => c.String());
            AlterColumn("dbo.ParserTasks", "GrabberSettings", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.ParserTasks", "GrabberSettings", c => c.String(nullable: false));
            AlterColumn("dbo.ParserTasks", "IteratorSettings", c => c.String(nullable: false));
            RenameTable(name: "dbo.ParserTasks", newName: "Parsers");
        }
    }
}