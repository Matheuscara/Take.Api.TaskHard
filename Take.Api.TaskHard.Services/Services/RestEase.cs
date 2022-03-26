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
    [Header("User-Agent", "Matheuscara")]
    public interface IGithubClientRepos
    {
        [Get("/search/repositories?q=language:{language} org:{org}&order={direction}&sort={type}")]
        Task<GitResponseList> GetReposAsync(
            [Path("language")] string language,
            [Path("org")] string org,
            [Path("direction")] string direction,
            [Path("type")] string type
        );
    }
}
