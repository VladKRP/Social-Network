namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDialogEntity1 : DbMigration
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
                        SenderId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.UserMessages", "Dialog_Id", c => c.Int());
            CreateIndex("dbo.UserMessages", "Dialog_Id");
            AddForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs", "Id");
            DropColumn("dbo.UserMessages", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserMessages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Dialogs", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Dialogs", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.UserMessages", new[] { "Dialog_Id" });
            DropIndex("dbo.Dialogs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Dialogs", new[] { "ReceiverId" });
            DropIndex("dbo.Dialogs", new[] { "SenderId" });
            DropColumn("dbo.UserMessages", "Dialog_Id");
            DropTable("dbo.Dialogs");
            CreateIndex("dbo.UserMessages", "ApplicationUser_Id");
            AddForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.Users", "Id");
        }
    }
}
