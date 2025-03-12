using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        ClientDTO? CreateNewClient(CreateClientDTO ClientDto);

        List<ClientDTO> GetAllClients();

        ClientDTO? GetClientById(int id);

        bool UpdateClient(UpdateClientDTO ClientDto, int id);

        Client? DeleteClient(int id);


    }
}
