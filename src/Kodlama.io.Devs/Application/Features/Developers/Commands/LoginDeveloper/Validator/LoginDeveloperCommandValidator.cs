using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.LoginDeveloper.Validator
{
    public class LoginDeveloperCommandValidator : AbstractValidator<LoginDeveloperCommand>
    {
        public LoginDeveloperCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty();

        }
    }
}
