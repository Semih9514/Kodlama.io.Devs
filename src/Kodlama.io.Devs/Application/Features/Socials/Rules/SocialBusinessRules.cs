using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Rules
{
    public class SocialBusinessRules
    {
        private readonly ISocialRepository _socialRepository;

        public SocialBusinessRules(ISocialRepository socialRepository)
        {
            _socialRepository = socialRepository;
        }

        public async Task SocialNameCanNotBeDuplicatedWhenInsterted(string name)
        {
            IPaginate<Social> result = await _socialRepository.GetListAsync(p => p.Github == name);
            if (result.Items.Any()) throw new BusinessException("Github name exits");
        }

        public void SocialShouldExistWhenRequested(Social social)
        {
            if (social == null) throw new BusinessException("Requsted Github name does not exist");
        }
    }
}
