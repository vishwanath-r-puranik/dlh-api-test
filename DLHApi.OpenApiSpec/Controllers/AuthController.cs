﻿using System.Net;
using System.Threading.Tasks;
using DLHApi.Common.Constants;
using DLHApi.Common.Utils;
using DLHApi.DAL.EISHandler.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DLHApi.OpenApiSpec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GenerateToken _generateToken;

        public AuthController(GenerateToken generateToken)
        {
            _generateToken = generateToken;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            var acessToken = await _generateToken.GetToken();

            if (acessToken != null)
            {
                return Ok(acessToken);
            }

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
