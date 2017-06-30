namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameOfPropertyInUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageData", c => c.Binary());
            DropColumn("dbo.Users", "ImageDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ImageDate", c => c.Binary());
            DropColumn("dbo.Users", "ImageData");
        }
    }
}
