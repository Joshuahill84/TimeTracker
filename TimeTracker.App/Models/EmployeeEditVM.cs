using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TimeTracker.App.Models
{
    public class EmployeeEditVM
    {
        public int Id { get; set; }
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Select Team")]
        public int SelectedTeamId { get; set; }
        public SelectList AvailableTeams { get; set; }
    }
}