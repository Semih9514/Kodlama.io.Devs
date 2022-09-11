using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Commands.CreateSocial.Validator
{
    public class CreateSocialCommandValidator : AbstractValidator<CreateSocialCommand>
    {
        public CreateSocialCommandValidator()
        {
            RuleFor(c => c.Github).NotEmpty();
            RuleFor(c => c.Github).MinimumLength(2);
        }
    }
}
