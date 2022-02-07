using System.ComponentModel.DataAnnotations;
using Quartz;

namespace WebPageAnalyzer.Validations;

public class CronValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value != null && CronExpression.IsValidExpression(value.ToString())) return true;

        ErrorMessage = "Cron not valid";
        return false;
    }
}