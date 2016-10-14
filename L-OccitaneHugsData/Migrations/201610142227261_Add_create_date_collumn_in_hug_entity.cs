namespace L_OccitaneHugsData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_create_date_collumn_in_hug_entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hug", "CreateDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hug", "CreateDate");
        }
    }
}
