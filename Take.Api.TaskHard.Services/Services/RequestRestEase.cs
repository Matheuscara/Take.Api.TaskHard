using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Take.Api.TaskHard.Models.ErrorsMessages;
using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Services.Services
{
    public class RequestRestEase
    {
        public static async Task<UserLogin> RequestRestEasyPostCreateTokenAsync(UserLoginBody userLoginBody)
        {
            try
            {
                PostJWT api = RestEase.RestClient.For<PostJWT>("https://identitytoolkit.googleapis.com");
                UserLogin tokenResponse = await api.CreateTokenAsync(userLoginBody);
                return tokenResponse;
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorsConstants.PERSON_NOT_FOUND);
            }
        }

        public static async Task<GirResponseList> RequestRestEasyGetReposAsync(UserGitHubBody userGitHubBody)
        {

                IGithubClientRepos api = RestEase.RestClient.For<IGithubClientRepos>("https://api.github.com");
                GirResponseList tokenResponse = await api.GetReposAsync(userGitHubBody.org, userGitHubBody.type, userGitHubBody.direction, userGitHubBody.perPage);
                return tokenResponse;

        }
    }
}
