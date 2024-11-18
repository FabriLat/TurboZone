using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientDTO CreateNewClient(CreateClientDTO createClientDTO)
        {

            Client client = new Client();
            client.FullName= createClientDTO.FullName;
            client.Email= createClientDTO.Email;
            client.Password= createClientDTO.Password;
            client.Location= createClientDTO.Location;
            _clientRepository.Add(client);

            return ClientDTO.Create(client);

        }


        public List<ClientDTO> GetAllClients()
        {
            var clients = _clientRepository.GetAll();
            var clientsDTO = new List<ClientDTO>();
            foreach (var client in clients)
            {
                ClientDTO clientDTO = new ClientDTO();
               clientDTO = ClientDTO.Create(client);
               clientsDTO.Add(clientDTO);
            }
            return clientsDTO;
        }


        public void UpdateClient(UpdateClientDTO updateClientDTO)
        {
            Client? clientToModify = _clientRepository.GetById(updateClientDTO.Id);

            if (clientToModify != null)
            {
                clientToModify.FullName = updateClientDTO.FullName;
                clientToModify.Email = updateClientDTO.Email;
                clientToModify.Password = updateClientDTO.Password;
                clientToModify.Location = updateClientDTO.Location;
                _clientRepository.Update(clientToModify);
            }
            
        }

    }
}
