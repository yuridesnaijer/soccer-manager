namespace SoccerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_nullable_news_model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "PostedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "PostedDate", c => c.DateTime(nullable: false));
        }
    }
}
