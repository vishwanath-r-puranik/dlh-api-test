using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.Common.Handlers
{
    public interface IExceptionHandler
    {
        public Task InvokeAsync(HttpContext httpContext);
    }
}
