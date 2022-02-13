using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

using Take.Api.TaskHard.Models.Models;

namespace Take.Api.TaskHard.Models.Validations
{
    public class ValidUserGitHubBody : AbstractValidator<UserGitHubBody>
    {
        public ValidUserGitHubBody()
        {
            RuleFor(option => option.org)
                .NotEmpty().WithMessage("Org is Required");

            RuleFor(option => option.direction)
                .NotEmpty().WithMessage("Direction is Required")
                .Must(BeAValidDirection).WithMessage("Direction must be a 'asc' or 'desc'");

            RuleFor(option => option.language)
                .NotEmpty().WithMessage("language is Required");

            RuleFor(option => option.type)
                .NotEmpty().WithMessage("Type is Required");

            RuleFor(option => option.perPage)
                .NotEmpty().WithMessage("PerPage is Required")
                .InclusiveBetween(1,5).WithMessage("PerPage must be between one and five");
        }

        private bool BeAValidDirection(string direction)
        {
            if (direction == "asc" || direction == "desc")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
