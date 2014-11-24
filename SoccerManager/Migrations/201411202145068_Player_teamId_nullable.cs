namespace SoccerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Player_teamId_nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamId" });
            AlterColumn("dbo.Players", "TeamId", c => c.Int());
            AddForeignKey("dbo.Players", "TeamId", "dbo.Teams", "TeamId");
            CreateIndex("dbo.Players", "TeamId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            AlterColumn("dbo.Players", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Players", "TeamId");
            AddForeignKey("dbo.Players", "TeamId", "dbo.Teams", "TeamId", cascadeDelete: true);
        }
    }
}
