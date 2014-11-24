namespace SoccerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        price = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Money = c.Int(nullable: false),
                        CoachId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.UserProfile", t => t.CoachId, cascadeDelete: true)
                .Index(t => t.CoachId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "CoachId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropForeignKey("dbo.Teams", "CoachId", "dbo.UserProfile");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.UserProfile");
        }
    }
}
