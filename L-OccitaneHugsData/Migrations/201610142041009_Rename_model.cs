namespace L_OccitaneHugsData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_model : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sentiment", newName: "Feeling");
            AddColumn("dbo.Hug", "Identifier", c => c.String());
            DropColumn("dbo.Hug", "Identificator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hug", "Identificator", c => c.String());
            DropColumn("dbo.Hug", "Identifier");
            RenameTable(name: "dbo.Feeling", newName: "Sentiment");
        }
    }
}
