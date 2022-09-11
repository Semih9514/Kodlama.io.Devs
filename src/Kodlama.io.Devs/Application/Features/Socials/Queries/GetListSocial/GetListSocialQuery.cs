using Application.Features.Socials.Models;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Queries.GetListSocial
{
    public class GetListSocialQuery : IRequest<SocialListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListSocialQueryHandler : IRequestHandler<GetListSocialQuery,SocialListModel>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            public GetListSocialQueryHandler(ISocialRepository socialRepository, IMapper mapper)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
            }

            public async Task<SocialListModel> Handle(GetListSocialQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Social> socials = await _socialRepository.GetListAsync(
                    include: m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                SocialListModel mappedSocialListModel = _mapper.Map<SocialListModel>(socials);
                return mappedSocialListModel;
            }
        }
    }
}
