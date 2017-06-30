namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendConfirmedProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "IsConfirmed");
        }
    }
}
