namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModifyDatesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Employees", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Teams", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Teams", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.TimeEntries", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.TimeEntries", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Shifts", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Shifts", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.PayCycles", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.PayCycles", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Positions", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Positions", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Positions", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PayCycles", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PayCycles", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shifts", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shifts", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeEntries", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeEntries", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Teams", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Teams", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
