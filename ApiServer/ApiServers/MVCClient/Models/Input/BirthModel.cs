using System.ComponentModel.DataAnnotations;

namespace MVCClient.Models
{
    public class BirthModel
    {
        [Required(ErrorMessage=("{0}不能为空"))]
        [MaxLength(8)]
        [RegularExpression(@"^\d{8}$")]
        public string birthday{get;set;}
    }
}