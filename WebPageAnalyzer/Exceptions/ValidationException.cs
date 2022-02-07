using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebPageAnalyzer.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(ModelStateDictionary modelState)
    {
        StatusCode = (int) HttpStatusCode.Conflict;
        ErrorMessage = "";
        foreach (var state in modelState)
            ErrorMessage += $"Invalid format {state.Key}: {state.Value.Errors[0].ErrorMessage} ";
    }

    public int StatusCode { get; }
    public string ErrorMessage { get; }
}