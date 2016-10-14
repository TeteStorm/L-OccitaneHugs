namespace L_OccitaneHugsData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_relationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hug", "FeelingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Hug", "CityId");
            CreateIndex("dbo.Hug", "FeelingId");
            AddForeignKey("dbo.Hug", "CityId", "dbo.City", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Hug", "FeelingId", "dbo.Feeling", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hug", "FeelingId", "dbo.Feeling");
            DropForeignKey("dbo.Hug", "CityId", "dbo.City");
            DropIndex("dbo.Hug", new[] { "FeelingId" });
            DropIndex("dbo.Hug", new[] { "CityId" });
            DropColumn("dbo.Hug", "FeelingId");
        }
    }
}
