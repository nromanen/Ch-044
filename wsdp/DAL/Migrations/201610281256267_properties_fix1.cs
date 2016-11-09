namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class properties_fix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Properties", new[] { "Category_Id" });
            AddColumn("dbo.Properties", "DefaultValue", c => c.String());
            AddColumn("dbo.Properties", "Category_Id1", c => c.Int());
            AlterColumn("dbo.Properties", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "Category_Id1");
            AddForeignKey("dbo.Properties", "Category_Id1", "dbo.Categories", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Properties", "Category_Id1", "dbo.Categories");
            DropIndex("dbo.Properties", new[] { "Category_Id1" });
            AlterColumn("dbo.Properties", "Category_Id", c => c.Int());
            DropColumn("dbo.Properties", "Category_Id1");
            DropColumn("dbo.Properties", "DefaultValue");
            CreateIndex("dbo.Properties", "Category_Id");
            AddForeignKey("dbo.Properties", "Category_Id", "dbo.Categories", "Id");
        }
    }
}