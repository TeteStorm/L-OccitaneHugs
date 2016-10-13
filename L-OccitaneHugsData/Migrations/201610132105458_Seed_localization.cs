namespace L_OccitaneHugsData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed_localization : DbMigration
    {
        public override void Up()
        {
            SqlFile("Scripts\\localizations.sql");
        }
        
        public override void Down()
        {
            SqlFile("Scripts\\clearLocalizations.sql");
        }
    }
}
