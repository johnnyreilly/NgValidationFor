using System.ComponentModel.DataAnnotations;

namespace NgValidationFor.Documentation.Models
{
    public class RequiredDemoModel
    {
        [Required]
        public string RequiredField { get; set; }
    }
}