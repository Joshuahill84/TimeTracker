namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.TeamEmployees",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Employee_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.TeamEmployees", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "Owner_Id" });
            DropTable("dbo.TeamEmployees");
            DropTable("dbo.Teams");
            DropTable("dbo.Employees");
        }
    }
}
