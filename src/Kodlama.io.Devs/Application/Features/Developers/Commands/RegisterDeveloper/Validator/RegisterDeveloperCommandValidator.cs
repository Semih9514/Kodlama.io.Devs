using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper.Validator
{
    public class RegisterDeveloperCommandValidator : AbstractValidator<RegisterDeveloperCommand>
    {
        public RegisterDeveloperCommandValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }
    }
}
