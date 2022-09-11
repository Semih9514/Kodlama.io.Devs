using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Commands.UpdateSocial.Validator
{
    public class UpdateSocialCommandValidator : AbstractValidator<UpdateSocialCommand>
    {
        public UpdateSocialCommandValidator()
        {
            RuleFor(c => c.Github).NotEmpty();
        }
    }
}
