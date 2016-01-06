namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeTeamMemberOf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.TeamEmployees", new[] { "Team_Id" });
            DropIndex("dbo.TeamEmployees", new[] { "Employee_Id" });
            AddColumn("dbo.Employees", "MemberOf_Id", c => c.Int());
            CreateIndex("dbo.Employees", "MemberOf_Id");
            AddForeignKey("dbo.Employees", "MemberOf_Id", "dbo.Teams", "Id");
            DropTable("dbo.TeamEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamEmployees",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Employee_Id });
            
            DropForeignKey("dbo.Employees", "MemberOf_Id", "dbo.Teams");
            DropIndex("dbo.Employees", new[] { "MemberOf_Id" });
            DropColumn("dbo.Employees", "MemberOf_Id");
            CreateIndex("dbo.TeamEmployees", "Employee_Id");
            CreateIndex("dbo.TeamEmployees", "Team_Id");
            AddForeignKey("dbo.TeamEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamEmployees", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
