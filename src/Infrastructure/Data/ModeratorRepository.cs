using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ModeratorRepository : RepositoryBase<Moderator>, IModeratorRepository
    {
        public ModeratorRepository(ApplicationContext context) : base(context)
        {
        }

    }
}
