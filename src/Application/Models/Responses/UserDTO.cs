using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public string Location { get; set; }

        public UserRol Rol {  get; set; }

        public UserState State { get; set; }

    }
}
