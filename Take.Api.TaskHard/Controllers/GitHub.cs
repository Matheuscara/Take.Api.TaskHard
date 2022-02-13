using System.Collections.Generic;
using System.Threading.Tasks;
using Take.Api.TaskHard.Models.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using Take.Api.TaskHard.Models.Validations;
using Take.Api.TaskHard.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Take.Api.TaskHard.Facades.Interfaces;
using Take.Api.TaskHard.Models.ErrorsMessages;

namespace Take.Api.TaskHard.Controllers
{
    [Microsoft.AspNetCore.Components.Route("Api/v1")]
    public class GitHub : ControllerBase
    {
        private readonly IGitHubFunctions _gitHubFunctions;
        private readonly ValidUserGitHubBody _validUserGitHub;

        public GitHub(IGitHubFunctions gitHubFunctions)
        {
            _gitHubFunctions = gitHubFunctions;
            _validUserGitHub = new ValidUserGitHubBody();
        }


        /// <summary>
        ///     Get repositories in GitHub
        /// </summary>
        /// <param name="org">Org name</param>
        /// <param name="direction">Direction of the repositories order</param>
        /// <param name="type">Repository type</param>
        /// <param name="perPage">Number of results per page</param>
        [HttpGet("/github/repos")]
        [Authorize]
        [ProducesResponseType(typeof(List<Repository>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MiddlwareError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ObjectErrorValidate), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MiddlwareError), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAsync(UserGitHubBody userGitHubBody)
        {
            ValidationResult results = _validUserGitHub.Validate(userGitHubBody);

            if (results.IsValid)
            {
                return Ok(await _gitHubFunctions.ReturnReposUserAsync(userGitHubBody));
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, ValidatorResult.ValidatorResultProperAsync(results));
            }
        }
    }
}
