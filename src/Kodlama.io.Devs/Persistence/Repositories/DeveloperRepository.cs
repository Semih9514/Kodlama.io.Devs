﻿using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DeveloperRepository : EfRepositoryBase<User, BaseDbContext>, IDeveloperRepository
    {
        public DeveloperRepository(BaseDbContext context) : base(context)
        {
        }
        public IList<OperationClaim> GetClaims(User user)
        {
            var result = from OperationClaim in Context.OperationClaims
                         join UserOperationClaim in Context.UserOperationClaims

                             on OperationClaim.Id equals UserOperationClaim.OperationClaimId
                         where UserOperationClaim.UserId == user.Id

                         select new OperationClaim { Id = OperationClaim.Id, Name = OperationClaim.Name };

            return result.ToList();
        }
    }
}
