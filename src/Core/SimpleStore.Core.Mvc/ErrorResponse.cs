using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleStore.Core.Mvc
{
    public class ErrorResponse
    {
        public string? Message { get; set; }

        public object? Content { get; set; }

        public string? StackTrace { get; set; }

        public ErrorResponse()
        {
            //
        }

        public ErrorResponse(ModelStateDictionary modelStateDictionary)
        {
            Message = "The request data is invalid.";
            Content = modelStateDictionary.Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage));
        }
    }
}