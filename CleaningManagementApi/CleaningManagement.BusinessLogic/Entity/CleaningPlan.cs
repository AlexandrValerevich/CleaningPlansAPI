using System;
using System.ComponentModel.DataAnnotations;

namespace CleaningManagement.BusinessLogic.Entity
{
    public class CleaningPlan
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [MaxLength(512)]
        public string Description { get; set; }

    }
}