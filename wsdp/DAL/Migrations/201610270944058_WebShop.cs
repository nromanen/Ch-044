namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WebShop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebShops",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.Int(nullable: false),
                    Path = c.String(nullable: false),
                    LogoPath = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.WebShops");
        }
    }
}