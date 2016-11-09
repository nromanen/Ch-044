namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class properties_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Description = c.String(),
                    Type = c.Int(nullable: false),
                    Prefix = c.String(),
                    Sufix = c.String(),
                    Characteristic_Id = c.Int(nullable: false),
                    Category_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Properties", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Properties", new[] { "Category_Id" });
            DropTable("dbo.Properties");
        }
    }
}