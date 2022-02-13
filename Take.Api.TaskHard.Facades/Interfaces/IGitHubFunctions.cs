using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Facades.Interfaces
{
    public interface IGitHubFunctions
    {
        Task<GirResponseList> ReturnReposUserAsync(UserGitHubBody userGitHubBody);
    }
}
