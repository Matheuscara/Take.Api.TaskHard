using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;
using FluentValidation.Validators;

using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Models.Validations
{
    public class ValidUserLoginBody : AbstractValidator<UserLoginBody>
    {
        public ValidUserLoginBody()
        {
            RuleFor(option => option.email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Format invalid");

            RuleFor(option => option.password)
                .NotNull().WithMessage("Password is required");

            RuleFor(option => option.returnSecureToken)
                .NotNull().WithMessage("Return Secure Token is required")
                .Equal(true).WithMessage("Return Secure Token must be true");
        }
    }
}
