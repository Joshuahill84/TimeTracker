namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeentry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "Day", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeEntries", "Day");
        }
    }
}
