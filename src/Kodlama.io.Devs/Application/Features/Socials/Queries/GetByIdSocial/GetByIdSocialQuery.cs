using Application.Features.Socials.Dtos;
using Application.Features.Socials.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Queries.GetByIdSocial
{
    public class GetByIdSocialQuery : IRequest<SocialGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdSocialQueryHandler   : IRequestHandler<GetByIdSocialQuery, SocialGetByIdDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;
            public GetByIdSocialQueryHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<SocialGetByIdDto> Handle(GetByIdSocialQuery request, CancellationToken cancellationToken)
            {
                Social? social = await _socialRepository.GetAsync(b=>b.Id==request.Id);
                _socialBusinessRules.SocialShouldExistWhenRequested(social);

                SocialGetByIdDto socialGetByIdDto = _mapper.Map<SocialGetByIdDto>(social);
                return socialGetByIdDto;
            }
        }
    }
}
