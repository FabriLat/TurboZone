using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryUser : RepositoryBase<User>, IUserRepository
    {
        private readonly ApplicationContext _context;
        public RepositoryUser(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            return user;
        }

        public User? GetByFullName(string fullName)
        {
            var user = _context.Users.FirstOrDefault(u => u.FullName.ToLower() == fullName.ToLower());
            return user;
        }

    }
}
