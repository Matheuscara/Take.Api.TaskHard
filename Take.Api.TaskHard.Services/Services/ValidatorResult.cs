using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Take.Api.TaskHard.Models.ErrorsMessages;
using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Services.Services
{
    public class ValidatorResult
    {
        public static ObjectErrorValidate ValidatorResultProperAsync(ValidationResult result)
        {
            ObjectErrorValidate ObjectError = new ObjectErrorValidate();

            foreach (var failure in result.Errors)
            {
                ObjectError.Property = failure.PropertyName;
                ObjectError.Error = failure.ErrorMessage;
                return ObjectError;
            }

            return ObjectError;
        }
    }
}
