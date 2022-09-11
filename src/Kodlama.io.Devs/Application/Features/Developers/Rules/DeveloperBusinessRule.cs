using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;
        public DeveloperBusinessRules(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }
        public async Task<User> UserControlCorrespondingToEmailAddress(string email)
        {
            User? developer = await _developerRepository.GetAsync(u => u.Email == email);

            if (developer == null)
                throw new BusinessException("Email or password is incorrect!");

            return developer;
        }

        public async Task DuplicateEmail(string email)
        {
            var data = await _developerRepository.GetAsync(p => p.Email == email);
            if (data != null) throw new BusinessException("duplicate email");

        }

        public async Task CheckEmailAndPassword(string password, User developer)
        {

            if (developer == null) throw new BusinessException("email is not found");

            if (!HashingHelper.VerifyPasswordHash(password, developer.PasswordHash, developer.PasswordSalt))
                throw new BusinessException("Password is not match");
        }
    }
}
