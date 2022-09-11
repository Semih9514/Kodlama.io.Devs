using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Social : Entity
    {
        public int DeveloperId { get; set; }
        public string Github { get; set; }
        public virtual User? User { get; set; }
        public Social()
        {
        }

        public Social(int id, string github,int developerId) : this()
        {
            Id = id;
            Github = github;
            DeveloperId = developerId;
        }
    }
}
