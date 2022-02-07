using System.ComponentModel.DataAnnotations;

namespace WebPageAnalyzer.Validations;

public class UrlValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value != null && Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute)) return true;

        ErrorMessage = "Url not valid";
        return false;
    }
}