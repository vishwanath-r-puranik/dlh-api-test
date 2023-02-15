using DLHApi.Common.Constants;
using System.Net;
using DLHApi.Common.Utils;
using DLHApi.DAL.Data;
using DLHApi.DAL.Models;
using DLHApi.Shared;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text;
using DLHApi.EIS.Authentication;
using Microsoft.IdentityModel.Tokens;
using DLHApi.Common.Logger.Contracts;

namespace DLHApi.DAL.Repo
{
    public class AuditRepo: IAuditRepo
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenHandler _tokenHandler;

        public static readonly string? AuditEndPointBase = Environment.GetEnvironmentVariable("AuditEndPointBase");
        private readonly ILoggerManager _logger;

        public AuditRepo(IHttpClientFactory httpClientFactory, ITokenHandler tokenHandler, ILoggerManager logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._tokenHandler= tokenHandler;
            this._logger = logger;

        }


        public async Task<AuditResponse> AddRequestAudit( CreateAuditRequest audit)
        {
            var response = new AuditResponse() { Success = false };

            try
            {
                if (AuditEndPointBase == null)
                    throw new ApiException(ErrorConstants.InvalidUrl, (int)HttpStatusCode.BadRequest);

                _logger.LogInfo($"{Project.DLHAPIDAL} - start AddRequestAudit");
                var client = _httpClientFactory.CreateClient("AuditRepo");

                var accessToken = await _tokenHandler.RetrieveToken();
             
                client.BaseAddress = new Uri(AuditEndPointBase);
               
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(JsonSerializer.Serialize(audit),Encoding.UTF8,"application/json");

                var resp = await client.PostAsync("DlhAudit", content);

                if (resp != null && resp.IsSuccessStatusCode)
                {                    
                    response.Success = true;
                    _logger.LogInfo($"{Project.DLHAPIDAL} - Successfully AddRequestAudit.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Project.DLHAPIDAL} - Error AddRequestAudit {ex.Message}");
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<AuditResponse> UpdateRequestAudit(UpdateAuditRequest audit)
        {
            var response = new AuditResponse() { Success = false };

            try
            {
                if (AuditEndPointBase == null)
                    throw new ApiException(ErrorConstants.InvalidUrl, (int)HttpStatusCode.BadRequest);
                
                _logger.LogInfo($"{Project.DLHAPIDAL} - start UpdateRequestAudit");
                var client = _httpClientFactory.CreateClient("AuditRepo");

                var accessToken = await _tokenHandler.RetrieveToken();

                client.BaseAddress = new Uri(AuditEndPointBase);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(JsonSerializer.Serialize(audit), Encoding.UTF8, "application/json");

                var resp = await client.PatchAsync("DlhAudit", content);

                if (resp != null && resp.IsSuccessStatusCode)
                {
                    response.Success = true;
                    _logger.LogInfo($"{Project.DLHAPIDAL} - Successfully UpdateRequestAudit");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Project.DLHAPIDAL} - Error UpdateRequestAudit {ex.Message}");
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
