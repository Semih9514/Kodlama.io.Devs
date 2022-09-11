using Application.Features.Socials.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Commands.DeleteSocial
{
    public class DeleteSocialCommand : IRequest<DeletedSocialDto>
    {
        public int Id { get; set; }
        public class DeleteSocialCommandHandler : IRequestHandler<DeleteSocialCommand,DeletedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;

            public DeleteSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
            }

            public async Task<DeletedSocialDto> Handle(DeleteSocialCommand request, CancellationToken cancellationToken)
            {
                Social? social = await _socialRepository.GetAsync(p => p.Id == request.Id);
                Social deletedSocial = await _socialRepository.DeleteAsync(social);
                DeletedSocialDto deletedSocialDto = _mapper.Map<DeletedSocialDto>(deletedSocial);

                return deletedSocialDto;
            }
        }
    }
}
