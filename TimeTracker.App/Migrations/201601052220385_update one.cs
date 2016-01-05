namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckInStatus = c.Int(nullable: false),
                        CheckOutStatus = c.Int(nullable: false),
                        DayStatus = c.Int(nullable: false),
                        BillableHours = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Employee_Id = c.Int(),
                        Shift_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Shifts", t => t.Shift_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Shift_Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntries", "Shift_Id", "dbo.Shifts");
            DropForeignKey("dbo.TimeEntries", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.TimeEntries", new[] { "Shift_Id" });
            DropIndex("dbo.TimeEntries", new[] { "Employee_Id" });
            DropTable("dbo.Shifts");
            DropTable("dbo.TimeEntries");
        }
    }
}
