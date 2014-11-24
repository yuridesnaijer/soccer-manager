namespace SoccerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_news_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Message = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
