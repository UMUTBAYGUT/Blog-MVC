namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryParentChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentCategory_Id", c => c.Int());
            CreateIndex("dbo.Categories", "ParentCategory_Id");
            AddForeignKey("dbo.Categories", "ParentCategory_Id", "dbo.Categories", "Id");
            DropColumn("dbo.Categories", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ParentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "ParentCategory_Id", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentCategory_Id" });
            DropColumn("dbo.Categories", "ParentCategory_Id");
        }
    }
}
