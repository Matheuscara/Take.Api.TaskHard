using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Take.Api.TaskHard.Facades.Interfaces;
using Take.Api.TaskHard.Models.ErrorsMessages;
using Take.Api.TaskHard.Models.Models;
using Take.Api.TaskHard.Models.Validations;
using Take.Api.TaskHard.Services.Services;

namespace Take.Api.TaskHard.Controllers
{
    [Route("Api/v1")]
    public class Auth : ControllerBase
    {
        private readonly IAuthJWT _authJWT;
        private readonly ValidUserLoginBody _validUser;

        public Auth(IAuthJWT authJWT)
        {
            _authJWT = authJWT;
            _validUser = new ValidUserLoginBody();
        }

        [HttpPost("/token")]
        [ProducesResponseType(typeof(UserLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectErrorValidate), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MiddlwareError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJWTAsync(UserLoginBody userLoginBody)
        {
            ValidationResult results = _validUser.Validate(userLoginBody);

            if (results.IsValid)
            {
                return Ok(await _authJWT.CreateTokenAsync(userLoginBody));
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, ValidatorResult.ValidatorResultProperAsync(results));
            }
        }
    }
}
