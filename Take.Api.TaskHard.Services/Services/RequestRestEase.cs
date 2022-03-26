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

        public static async Task<GitResponseList> RequestRestEasyGetReposAsync(UserGitHubBody userGitHubBody)
        {
            IGithubClientRepos api = RestEase.RestClient.For<IGithubClientRepos>("https://api.github.com");
            GitResponseList tokenResponse = await api.GetReposAsync(userGitHubBody.language, userGitHubBody.org, userGitHubBody.direction, userGitHubBody.type);
            return tokenResponse;   
        }
    }
}
