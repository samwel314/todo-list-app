using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Api
{
    public class GlobalErrorHandling : IExceptionHandler
    {
        private readonly IProblemDetailsService problemDetailsService;

        public GlobalErrorHandling(IProblemDetailsService problemDetailsService)
        {
            this.problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync
            (HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest , 
                _ => StatusCodes.Status500InternalServerError ,    
            };
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {

                HttpContext = httpContext,      
                Exception = exception,  
                ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "error has occurred ",   
                    Detail = exception.Message      
                }
            } ); 
        }
    }
}
