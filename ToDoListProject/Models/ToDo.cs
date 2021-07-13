using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoListProject.Models
{
    public class ToDo
    {
        // Adding the respective prameters
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public virtual ApplicationUser User { get; set; } // Adding the reference to the specific user
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ToDoDate { get; set; }

    }
}