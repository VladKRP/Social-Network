namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserWallPostEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserWallPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        UserId = c.String(maxLength: 128),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserWallPosts", "UserId", "dbo.Users");
            DropIndex("dbo.UserWallPosts", new[] { "UserId" });
            DropTable("dbo.UserWallPosts");
        }
    }
}
