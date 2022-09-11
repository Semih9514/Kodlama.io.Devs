using Application.Features.Socials.Dtos;
using Application.Features.Socials.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialCommand : IRequest<CreatedSocialDto>
    {
        public string Github { get; set; }
        public int DeveloperId { get; set; }
        public class CreateSocialCommandHandler : IRequestHandler<CreateSocialCommand,CreatedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;
            public CreateSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<CreatedSocialDto> Handle(CreateSocialCommand request, CancellationToken cancellationToken)
            {
                await _socialBusinessRules.SocialNameCanNotBeDuplicatedWhenInsterted(request.Github);

                Social mappedSocial = _mapper.Map<Social>(request);
                Social createdSocial = await _socialRepository.AddAsync(mappedSocial);
                CreatedSocialDto createdSocialDto = _mapper.Map<CreatedSocialDto>(createdSocial);
                return createdSocialDto;
            }
        }
    }
}
