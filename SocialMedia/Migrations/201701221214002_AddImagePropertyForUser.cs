namespace SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePropertyForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageDate", c => c.Binary());
            AddColumn("dbo.Users", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageMimeType");
            DropColumn("dbo.Users", "ImageDate");
        }
    }
}
