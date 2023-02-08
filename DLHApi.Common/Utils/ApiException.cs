﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DLHApi.Common.Models;

namespace DLHApi.Common.Utils
{
    public class ApiException : Exception
    {
        public int StatusCode { get; init; }

        public IEnumerable<DlhValidationError> ValidationErrors { get; init; }

        public ApiException(string message, int statusCode = (int)HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(Exception ex, int statusCode = (int)HttpStatusCode.InternalServerError) : base(ex.Message)
        {
            StatusCode = statusCode;
        }

        public ApiException(IEnumerable<DlhValidationError> errors, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            ValidationErrors = errors;
            StatusCode = statusCode;
        }

        public ApiException(ModelStateDictionary modelState, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            ValidationErrors = modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new DlhValidationError(key, x.ErrorMessage))).ToList();
            StatusCode = statusCode;
        }

        //can add more......
    }
}