using Application.Features.Socials.Dtos;
using Application.Features.Socials.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommand: IRequest<UpdatedSocialDto>
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string Github { get; set; }

        public class UpdateSocialCommandHandler : IRequestHandler<UpdateSocialCommand,UpdatedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;
            public UpdateSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<UpdatedSocialDto> Handle(UpdateSocialCommand request, CancellationToken cancellationToken)
            {
                await _socialBusinessRules.SocialNameCanNotBeDuplicatedWhenInsterted(request.Github);

                Social mappedSocial = _mapper.Map<Social>(request);
                Social updatedSocial = await _socialRepository.UpdateAsync(mappedSocial);
                UpdatedSocialDto updatedSocialDto = _mapper.Map<UpdatedSocialDto>(updatedSocial);

                return updatedSocialDto;
            }
        }
    }
}
