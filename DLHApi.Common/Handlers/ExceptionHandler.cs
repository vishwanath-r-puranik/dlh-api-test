using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DLHApi.Common.Constants;
using DLHApi.Common.Models;
using DLHApi.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace DLHApi.Common.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            switch (exception)
            {
                case ApiException ex:
                    if (ex?.ValidationErrors is not null)
                    {
                        await HandleDlhValidationErrorAsync(context, ex);
                    }
                    await HandleDlhApiErrorAsync(context, ex);
                    break;

                default:
                    //get inner exception......
                    Exception exp = exception;
                    if(exception.InnerException != null)
                            exp = exception.InnerException;

                    await HandleGeneralExceptionAsync(context, exp);
                    break;
            }

        }

        private async Task HandleDlhApiErrorAsync(HttpContext context, ApiException ex)
        {
            var response = new DlhApiFailureResponse { Error = new DlhErrorModel { Message = ex.Message, Status = ex.StatusCode } };

            var jsonString = JsonSerializer.Serialize(response);

            await WriteFormattedRespToHttpContextAsync(context, ex.StatusCode, jsonString!);
        }

        private async Task HandleDlhValidationErrorAsync(HttpContext context, ApiException ex)
        {
            var response = new DlhApiFailureResponse { Error = new DlhErrorModel { Message = ErrorConstants.ValidationError, ValidationErrors = (IEnumerable<DlhValidationError>)ex.ValidationErrors, Status = ex.StatusCode } };

            var jsonString = JsonSerializer.Serialize(response);

            await WriteFormattedRespToHttpContextAsync(context, ex.StatusCode, jsonString);
        }

        private async Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new DlhApiFailureResponse { Error = new DlhErrorModel { Message = ex.Message, Status = (int)HttpStatusCode.InternalServerError } };

            var jsonString = JsonSerializer.Serialize(response);

            await WriteFormattedRespToHttpContextAsync(context, (int)HttpStatusCode.InternalServerError, jsonString!);
        }

        protected static async Task WriteFormattedRespToHttpContextAsync(HttpContext context, int httpStatusCode, string jsonString)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.ContentLength = jsonString != null ? Encoding.UTF8.GetByteCount(jsonString!) : 0;

            await context.Response.WriteAsync(jsonString);
        }
    }
}
