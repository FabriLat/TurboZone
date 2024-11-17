﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class CreateClientDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }

        public UserRol Rol  = UserRol.Client;




    }
}
