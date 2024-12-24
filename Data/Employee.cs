using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string? Gender { get; set; } 
        public string? Class { get; set; }
        public string? Designation { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
