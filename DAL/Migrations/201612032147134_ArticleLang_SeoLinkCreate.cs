namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleLang_SeoLinkCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleLangs", "SeoLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleLangs", "SeoLink");
        }
    }
}
