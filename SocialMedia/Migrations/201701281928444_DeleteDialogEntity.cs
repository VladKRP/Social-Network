namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDialogEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialogs", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "UserId", "dbo.Users");
            DropIndex("dbo.Dialogs", new[] { "CreatorId" });
            DropIndex("dbo.Dialogs", new[] { "UserId" });
            DropIndex("dbo.Dialogs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserMessages", new[] { "Dialog_Id" });
            AddColumn("dbo.UserMessages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserMessages", "ApplicationUser_Id");
            AddForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.Users", "Id");
            DropColumn("dbo.UserMessages", "Dialog_Id");
            DropTable("dbo.Dialogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserMessages", "Dialog_Id", c => c.Int());
            DropForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserMessages", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserMessages", "ApplicationUser_Id");
            CreateIndex("dbo.UserMessages", "Dialog_Id");
            CreateIndex("dbo.Dialogs", "ApplicationUser_Id");
            CreateIndex("dbo.Dialogs", "UserId");
            CreateIndex("dbo.Dialogs", "CreatorId");
            AddForeignKey("dbo.Dialogs", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs", "Id");
            AddForeignKey("dbo.Dialogs", "CreatorId", "dbo.Users", "Id");
            AddForeignKey("dbo.Dialogs", "ApplicationUser_Id", "dbo.Users", "Id");
        }
    }
}
