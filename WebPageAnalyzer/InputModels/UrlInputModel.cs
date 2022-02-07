using System.ComponentModel.DataAnnotations;
using WebPageAnalyzer.Validations;

namespace WebPageAnalyzer.Storage.Dto;

public class UrlInputModel
{
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Minimum length 3 chars")]
    [UrlValidation]
    public string Url { get; set; }
}