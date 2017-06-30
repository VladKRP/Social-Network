namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDialogEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserMessages", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CreatorId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.UserMessages", "Dialog_Id", c => c.Int());
            CreateIndex("dbo.UserMessages", "Dialog_Id");
            AddForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs", "Id");
            DropColumn("dbo.UserMessages", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserMessages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Dialogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Dialogs", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserMessages", new[] { "Dialog_Id" });
            DropIndex("dbo.Dialogs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Dialogs", new[] { "UserId" });
            DropIndex("dbo.Dialogs", new[] { "CreatorId" });
            DropColumn("dbo.UserMessages", "Dialog_Id");
            DropTable("dbo.Dialogs");
            CreateIndex("dbo.UserMessages", "ApplicationUser_Id");
            AddForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.Users", "Id");
        }
    }
}
