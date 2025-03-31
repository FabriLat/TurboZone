using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class ClientDTO : UserDTO
    {

        public static ClientDTO Create(Client client)
        {
            var clientDTO = new ClientDTO();
            clientDTO.Id = client.Id;
            clientDTO.FullName = client.FullName;
            clientDTO.Email = client.Email;
            clientDTO.PhoneNumber = client.phoneNumber;
            clientDTO.Location = client.Location;
            clientDTO.Rol = client.Rol;
            clientDTO.State = client.State;
            return clientDTO;
        }
    }
}
