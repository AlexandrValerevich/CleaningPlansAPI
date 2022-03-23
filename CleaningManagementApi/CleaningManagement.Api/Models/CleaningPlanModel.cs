using System.ComponentModel.DataAnnotations;

namespace CleaningManagement.Api.Models
{
    public class CleaningPlanModel
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [MaxLength(512)]
        public string Description { get; set; }
    }
}