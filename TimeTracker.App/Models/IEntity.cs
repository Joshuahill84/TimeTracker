using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.App.Models
{
    public interface IEntity
    {
        int Id { get; set; }

        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }

        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }

    public class Entity : IEntity
    {
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }
        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime ModifiedOn { get; set; }
    }
}