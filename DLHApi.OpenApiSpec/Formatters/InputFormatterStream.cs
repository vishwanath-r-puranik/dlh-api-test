using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Org.OpenAPITools.Formatters
{
    /// <summary>
    /// Input Type Formatter to allow model binding to Streams
    /// </summary>
    public class InputFormatterStream : InputFormatter
    {
        /// <summary>
        /// Input request media types added
        /// </summary>
        public InputFormatterStream()
        {
            SupportedMediaTypes.Add("application/octet-stream");
            SupportedMediaTypes.Add("image/jpeg");
        }

        /// <summary>
        /// Input request to handleif readable type property
        /// </summary>
        protected override bool CanReadType(Type type)
        {
            if (type == typeof(Stream))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Input request to handle if readable request body property
        /// </summary>
        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            return InputFormatterResult.SuccessAsync(context.HttpContext.Request.Body);
        }
    }
}