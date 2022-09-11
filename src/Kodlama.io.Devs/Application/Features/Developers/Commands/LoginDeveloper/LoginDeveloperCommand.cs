using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.LoginDeveloper
{
    public class LoginDeveloperCommand : IRequest<LoginedDeveloperDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, LoginedDeveloperDto>
        {
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly DeveloperBusinessRules _developerBusinessRules;
            private readonly IDeveloperRepository _developerRepository;

            public LoginDeveloperCommandHandler(IMapper mapper, ITokenHelper tokenHelper,
                DeveloperBusinessRules developerBusinessRules, IDeveloperRepository developerRepository)
            {
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _developerBusinessRules = developerBusinessRules;
                _developerRepository = developerRepository;
            }
            public async Task<LoginedDeveloperDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
            {
                var developer = await _developerRepository.GetAsync(p => p.Email == request.Email);
                await _developerBusinessRules.CheckEmailAndPassword(request.Password, developer);

                var token = _tokenHelper.CreateToken(developer, new List<OperationClaim>());
                var mappedToken = _mapper.Map<LoginedDeveloperDto>(token);
                return mappedToken;

            }
        }
    }
}
