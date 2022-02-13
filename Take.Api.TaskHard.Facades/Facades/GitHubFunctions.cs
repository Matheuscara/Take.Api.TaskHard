using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Take.Api.TaskHard.Facades.Interfaces;
using Take.Api.TaskHard.Models.Models;
using Take.Api.TaskHard.Services.Services;

namespace Take.Api.TaskHard.Facades.Facades
{
    public class GitHubFunctions : IGitHubFunctions
    {
        public async Task<GirResponseList> ReturnReposUserAsync(UserGitHubBody userGitHubBody)
        {
            Func<Task<GirResponseList>> RequestRestEasyGetReposAsync = async () =>
                await RequestRestEase.RequestRestEasyGetReposAsync(userGitHubBody);

            return await RequestRestEasyGetReposAsync();
        }
    }
}
