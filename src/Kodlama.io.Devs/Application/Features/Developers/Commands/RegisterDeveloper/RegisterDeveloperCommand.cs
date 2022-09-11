using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper
{
    public class RegisterDeveloperCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, AccessToken>
        {
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly DeveloperBusinessRules _developerBusinessRules;
            private readonly IDeveloperRepository _developerRepository;
            public RegisterDeveloperCommandHandler(IMapper mapper, ITokenHelper tokenHelper,
                DeveloperBusinessRules developerBusinessRules, IDeveloperRepository developerRepository)
            {
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _developerBusinessRules = developerBusinessRules;
                _developerRepository = developerRepository;
            }

            public async Task<AccessToken> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.DuplicateEmail(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                User developer = _mapper.Map<User>(request);
                developer.PasswordHash = passwordHash;
                developer.PasswordSalt = passwordSalt;
                developer.Status = true;

                User createdDeveloper = await _developerRepository.AddAsync(developer);

                var token = _tokenHelper.CreateToken(createdDeveloper, new List<OperationClaim>());
                var mappedToken = _mapper.Map<AccessToken>(token);
                return mappedToken;
            }
        }
    }
}
