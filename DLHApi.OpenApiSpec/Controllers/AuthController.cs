using System.Net;
using System.Threading.Tasks;
using DLHApi.Common.Constants;
using DLHApi.Common.Logger.Contracts;
using DLHApi.Common.Utils;
using DLHApi.DAL.EISHandler.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DLHApi.OpenApiSpec.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly GenerateToken _generateToken;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILoggerManager _logger;

        /// <summary>
        /// 
        /// </summary>
        public AuthController(GenerateToken generateToken, ILoggerManager logger)
        {
            _generateToken = generateToken;
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            _logger.LogInfo($"{Project.DLHAPIOpenSpec} - Requested authentication for OpenApi");
            var acessToken = await _generateToken.GetToken();

            if (acessToken != null)
            {
                _logger.LogInfo($"{Project.DLHAPIOpenSpec} - Authentication successful. Returning token to Swagger");
                return Ok(acessToken);
            }

            _logger.LogError($"{Project.DLHAPIOpenSpec} - Authentication request failed. {(int)HttpStatusCode.BadRequest}");
            throw new ApiException((string.Format(ErrorConstants.IncorrectUsrnameOrPsswrd)), (int)HttpStatusCode.BadRequest);
            
            /*var acessToken = await _tokenHandler.RetrieveToken();

            if (acessToken != null)
            {
                return Ok(acessToken);
            }

            return BadRequest("Username of Password is incorrect"); */
        }
    }
}
