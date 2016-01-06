using System.Web.Mvc;

namespace TimeTracker.App.Models
{
    public class EmployeeEditVM
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int SelectedTeamId { get; set; }
        public SelectList AvailableTeams { get; set; }
    }
}