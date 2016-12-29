namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class DatabaseContext : DbContext
    {
        // Your context has been configured to use a 'BlogModels' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DAL.BlogModels' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BlogModels' 
        // connection string in the application configuration file.
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
            Database.SetInitializer(new VeritabaniOlusturucu());
        }


        public virtual DbSet<Lang> Langs { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryLang> CategoryLangs { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleLang> ArticleLangs { get; set; }
        public virtual DbSet<DAL.Settings> Settings { get; set; }
      
         


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>
    {
        
        protected override void Seed(DatabaseContext context)
        {
            Account kullanici1 = new Account();
            kullanici1.Username = "umut"; kullanici1.Surname = "baygut"; kullanici1.Role = "Admin";kullanici1.Phone = 5386534333;kullanici1.Password = "teknoloji";
            kullanici1.Name = "Umut"; kullanici1.Mail = "umutbaygut@hotmail.com";

            Lang lang1 = new Lang();
            lang1.Name = "Türkçe"; lang1.Slug = "tr"; lang1.Flag = "pic/lang/tr.jpg"; lang1.Account = kullanici1;

            Lang lang2 = new Lang();
            lang2.Name = "English"; lang2.Slug = "en"; lang2.Flag = "pic/lang/en.jpg"; lang2.Account = kullanici1; 

            Category kategori1 = new Category();
            kategori1.Clicks = 0;
            kategori1.IsActive = true;


            CategoryLang katlang1 = new CategoryLang();
            katlang1.Category = kategori1;
            katlang1.Lang = lang1;
            katlang1.Image = "pic/kategori/programlama/1.jpg";
            katlang1.Name = "Programlama";
            katlang1.Slug = "programlama";
            CategoryLang katlang2 = new CategoryLang();
            katlang2.Category = kategori1;
            katlang2.Lang = lang2;
            katlang2.Image = "pic/category/programming/1.jpg";
            katlang2.Name = "Programming";
            katlang2.Slug = "programming";

            Article article1 = new Article();
            article1.Account = kullanici1;
            article1.Categories = new System.Collections.Generic.List<Category>();
            article1.Categories.Add(kategori1);
            article1.Clicks = 0;
            article1.Date = DateTime.Now;
            ArticleLang articlelang1 = new ArticleLang();
            articlelang1.Article = article1;
            articlelang1.Content = "Test Türkçe";
            articlelang1.Image = "pic/yazi/hosgeldiniz/1.jpg";
            articlelang1.Lang = lang1;
            articlelang1.Title = "Hoş Geldiniz.";

            ArticleLang articlelang2 = new ArticleLang();
            articlelang2.Article = article1;
            articlelang2.Content = "Test English";
            articlelang2.Image = "pic/article/welcome/1.jpg";
            articlelang2.Lang = lang2;
            articlelang2.Title = "WelCome";
            kategori1.Articles = new System.Collections.Generic.List<Article>();
            kategori1.Articles.Add(article1);
            kategori1.CategoryLangs = new System.Collections.Generic.List<CategoryLang>();
            kategori1.CategoryLangs.Add(katlang1);
            kategori1.CategoryLangs.Add(katlang2);



            context.Accounts.Add(kullanici1);
            context.Langs.Add(lang1);
            context.Langs.Add(lang2);
            context.Articles.Add(article1);
            context.ArticleLangs.Add(articlelang1);
            context.ArticleLangs.Add(articlelang2);
            context.Categories.Add(kategori1);
            context.CategoryLangs.Add(katlang1);
            context.CategoryLangs.Add(katlang2);
           
            try { context.SaveChanges(); }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null)
                        ? ex.InnerException.Message
                        : "";
            }
          
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    
}