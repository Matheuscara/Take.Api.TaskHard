using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Take.Api.TaskHard.Facades.Interfaces;
using Take.Api.TaskHard.Models.ErrorsMessages;
using Take.Api.TaskHard.Models.Models;
using Take.Api.TaskHard.Models.Validations;
using Take.Api.TaskHard.Services.Services;

namespace Take.Api.TaskHard.Facades.Facades
{
    public class AuthJWTFuntions : IAuthJWT
    {
        public async Task<UserLogin> CreateTokenAsync(UserLoginBody userLoginBody)
        {
            Func<Task<UserLogin>> RequestRestEasyPostCreateTokenAsync = async () =>
                await RequestRestEase.RequestRestEasyPostCreateTokenAsync(userLoginBody);

            return await RequestRestEasyPostCreateTokenAsync();
        }
    }
}
