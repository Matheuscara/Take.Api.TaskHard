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
        public async Task<GitResponseList> ReturnReposUserAsync(UserGitHubBody userGitHubBody)
        {
            Func<Task<GitResponseList>> RequestRestEasyGetReposAsync = async () =>
                await RequestRestEase.RequestRestEasyGetReposAsync(userGitHubBody);

            return await RequestRestEasyGetReposAsync();
        }
    }
}
