namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestForSettingsMigrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "GoogleAnaly", c => c.String());
            DropColumn("dbo.Settings", "GoogleAnal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "GoogleAnal", c => c.String());
            DropColumn("dbo.Settings", "GoogleAnaly");
        }
    }
}
