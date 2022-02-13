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
        /// Get filter repositories in Github Organization
        /// </summary>
        /// <param name="UserGitHubBody">Organization Name - takenet</param>
        /// <param name="language">Language Filter Name - C#, javascript, java, etc</param>
        /// <param name="direction">Direction of the repositories order - asc or desc</param>
        /// <param name="type">Type filter - stars,forks or updated</param>
        /// <param name="perPage">Number of results per page - 1-5</param>
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
