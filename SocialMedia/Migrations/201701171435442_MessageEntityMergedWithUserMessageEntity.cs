namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageEntityMergedWithUserMessageEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMessages", "MessageId", "dbo.Messages");
            DropIndex("dbo.UserMessages", new[] { "MessageId" });
            AddColumn("dbo.UserMessages", "Text", c => c.String());
            AlterColumn("dbo.UserMessages", "PostedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserMessages", "MessageId");
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserMessages", "MessageId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserMessages", "PostedDate", c => c.DateTime());
            DropColumn("dbo.UserMessages", "Text");
            CreateIndex("dbo.UserMessages", "MessageId");
            AddForeignKey("dbo.UserMessages", "MessageId", "dbo.Messages", "Id", cascadeDelete: true);
        }
    }
}
