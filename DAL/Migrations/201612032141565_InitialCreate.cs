namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Mail = c.String(),
                        Phone = c.Double(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Clicks = c.Int(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.ArticleLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                        Article_Id = c.Int(),
                        Lang_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .ForeignKey("dbo.Langs", t => t.Lang_Id)
                .Index(t => t.Article_Id)
                .Index(t => t.Lang_Id);
            
            CreateTable(
                "dbo.Langs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Flag = c.String(),
                        Slug = c.String(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Left = c.Int(nullable: false),
                        Right = c.Int(nullable: false),
                        Depth = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Clicks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryLangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Slug = c.String(),
                        Category_Id = c.Int(),
                        Lang_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Langs", t => t.Lang_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Lang_Id);
            
            CreateTable(
                "dbo.CategoryArticles",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Article_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryLangs", "Lang_Id", "dbo.Langs");
            DropForeignKey("dbo.CategoryLangs", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.CategoryArticles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ArticleLangs", "Lang_Id", "dbo.Langs");
            DropForeignKey("dbo.Langs", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ArticleLangs", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.Articles", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.CategoryArticles", new[] { "Article_Id" });
            DropIndex("dbo.CategoryArticles", new[] { "Category_Id" });
            DropIndex("dbo.CategoryLangs", new[] { "Lang_Id" });
            DropIndex("dbo.CategoryLangs", new[] { "Category_Id" });
            DropIndex("dbo.Langs", new[] { "Account_Id" });
            DropIndex("dbo.ArticleLangs", new[] { "Lang_Id" });
            DropIndex("dbo.ArticleLangs", new[] { "Article_Id" });
            DropIndex("dbo.Articles", new[] { "Account_Id" });
            DropTable("dbo.CategoryArticles");
            DropTable("dbo.CategoryLangs");
            DropTable("dbo.Categories");
            DropTable("dbo.Langs");
            DropTable("dbo.ArticleLangs");
            DropTable("dbo.Articles");
            DropTable("dbo.Accounts");
        }
    }
}
