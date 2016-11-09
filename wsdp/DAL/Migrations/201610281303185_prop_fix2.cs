namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class prop_fix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "Category_Id1", "dbo.Categories");
            DropIndex("dbo.Properties", new[] { "Category_Id1" });
            DropColumn("dbo.Properties", "Category_Id");
            RenameColumn(table: "dbo.Properties", name: "Category_Id1", newName: "Category_Id");
            AlterColumn("dbo.Properties", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "Category_Id");
            AddForeignKey("dbo.Properties", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Properties", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Properties", new[] { "Category_Id" });
            AlterColumn("dbo.Properties", "Category_Id", c => c.Int());
            RenameColumn(table: "dbo.Properties", name: "Category_Id", newName: "Category_Id1");
            AddColumn("dbo.Properties", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "Category_Id1");
            AddForeignKey("dbo.Properties", "Category_Id1", "dbo.Categories", "Id");
        }
    }
}