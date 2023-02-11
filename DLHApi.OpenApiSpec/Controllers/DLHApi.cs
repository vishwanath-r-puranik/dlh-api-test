/*
 * IBM MOVES DLH API
 *
 * IBM solution for MOVES
 *
 * The version of the OpenAPI document: 0.0.1
 * Contact: support@ibm.com
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DLHApi.DTO.V1.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.OpenAPITools.Attributes;
using Org.OpenAPITools.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Org.OpenAPITools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DLHApiController : ControllerBase
    {
        private readonly DlhistoryModelMapper _dlhistoryModelMapper;
        private readonly ILogger _logger;

        public DLHApiController(DlhistoryModelMapper dlhistoryModelMapper, ILogger<DLHApiController> logger)
        {
            this._dlhistoryModelMapper = dlhistoryModelMapper;
            this._logger = logger;
        }

        /// <summary>
        /// List DLH data
        /// </summary>
        /// <remarks>Returns the pdf document with other information</remarks>
        /// <param name="mvid"></param>
        /// <response code="200">Returns the pdf document with other information</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Request document not found</response>
        /// <response code="405">Validation exception</response>
        [HttpGet]
        [Route("/DLHDocument/{mvid}")]
        [ValidateModelState]
        [SwaggerOperation("DLHDocumentMvidGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(DLHApiPDFData), description: "Returns the pdf document with other information")]
        [Authorize]
        public async Task<IActionResult> DLHDocumentMvidGet([FromRoute(Name = "mvid")][Required] int mvid)
        {
            _logger.LogInformation($"Logger Received DLH request for Mvid:{mvid} on {DateTime.Now.ToString()}.");
  
            return await _dlhistoryModelMapper.DLHHistoryData(mvid);
        
        }

        ///// <summary>
        ///// List DLH document
        ///// </summary>
        ///// <remarks>Returns the pdf document from 3 party api</remarks>
        ///// <param name="mvid"></param>
        ///// <response code="200">Returns the pdf document with other information</response>
        ///// <response code="400">Invalid ID supplied</response>
        ///// <response code="404">Request document not found</response>
        ///// <response code="405">Validation exception</response>
        [HttpGet]
        [Route("/DLHDocument/merge/{mvid}")]
        [ValidateModelState]
        [SwaggerOperation("DLHDocumentMergeMvidGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(DLHApiPDFData), description: "Returns the pdf document with other information")]
        [Authorize]
        public async Task<IActionResult> DLHDocumentMergeMvidGet([FromRoute(Name = "mvid")][Required] int mvid)
        {

            return await _dlhistoryModelMapper.DLHDocumentMerge(mvid);

        }
    }
}
