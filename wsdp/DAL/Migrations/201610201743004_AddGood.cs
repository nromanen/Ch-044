namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddGood : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Category = c.Int(nullable: false),
                    XmlData = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Goods");
        }
    }
}