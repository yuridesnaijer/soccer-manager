namespace SoccerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "CoachId", "dbo.UserProfile");
            DropIndex("dbo.Teams", new[] { "CoachId" });
            AlterColumn("dbo.Teams", "CoachId", c => c.Int());
            AddForeignKey("dbo.Teams", "CoachId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Teams", "CoachId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "CoachId" });
            DropForeignKey("dbo.Teams", "CoachId", "dbo.UserProfile");
            AlterColumn("dbo.Teams", "CoachId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "CoachId");
            AddForeignKey("dbo.Teams", "CoachId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
