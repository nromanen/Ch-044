namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Deleted_Charac_Id : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Properties", "Characteristic_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Properties", "Characteristic_Id", c => c.Int(nullable: false));
        }
    }
}