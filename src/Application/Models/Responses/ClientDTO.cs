﻿using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public UserRol Rol {  get; set; }

        public UserState State { get; set; }


        public static ClientDTO Create(Client client)
        {
            var clientDTO = new ClientDTO();
            clientDTO.Id = client.Id;
            clientDTO.FullName = client.FullName;
            clientDTO.Email = client.Email;
            clientDTO.Location = client.Location;
            clientDTO.ImageUrl = client.ImageUrl;
            clientDTO.Rol = client.Rol;
            clientDTO.State = client.State;
            return clientDTO;
        }


       

    }
}
