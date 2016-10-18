namespace L_OccitaneHugsData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_create_imageUrl_collumn_in_hug_entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hug", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hug", "ImageUrl");
        }
    }
}
