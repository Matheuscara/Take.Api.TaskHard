using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using RestEase;

using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Services.Services
{
    public interface PostJWT
    {
        [Post("/v1/accounts:signInWithPassword?key=AIzaSyDyNsVLeo4mLbxww_y-WUKVBbsKttKAeL0=")]
        Task<UserLogin> CreateTokenAsync([Body] UserLoginBody userLoginBody);
    }

    [Header("Accept", "application/vnd.github.v3+json")]
    [Header("User-Agent", "Andre1999Lopes")]

    public interface IGithubClientRepos
    {
        [Get("orgs/{org}/repos")]
        Task<GirResponseList> GetReposAsync(
            [Path("org")] string org,
            [Query("type")] string type,
            [Query("direction")] string direction,
            [Query("per_page")] int perPage
        );
    }
}
