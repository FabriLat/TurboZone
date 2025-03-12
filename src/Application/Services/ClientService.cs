using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
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

        public ClientDTO? CreateNewClient(CreateClientDTO createClientDTO)
        {
            if(createClientDTO.FullName.Trim().Length > 4 &&
                createClientDTO.Password.Trim().ToLower().Length > 6 &&
                createClientDTO.Password == createClientDTO.ConfirmPassword)
            {
                int spaces = 0;
                string name = createClientDTO.FullName.Trim();
                foreach (var letter in name)
                {
                    if (string.IsNullOrWhiteSpace(letter.ToString()))
                    {
                        spaces += 1;
                    }
                };
                if (spaces > 2)
                    return null;
                Client client = new Client();
                client.FullName = createClientDTO.FullName;
                client.Email = createClientDTO.Email;
                client.Password = createClientDTO.Password;
                client.Location = createClientDTO.Location;
                _clientRepository.Add(client);
                return ClientDTO.Create(client);
            }
            return null;
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


        public ClientDTO? GetClientById(int id)
        {
            Client? client = _clientRepository.GetById(id);
            if (client != null)
            {
                ClientDTO clientDTO = ClientDTO.Create(client);
                return clientDTO;
            }
            return null;
        }

        public bool UpdateClient(UpdateClientDTO updateClientDTO, int id)
        {
            Client? clientToModify = _clientRepository.GetById(id);

            if (clientToModify != null)
            {
                if (updateClientDTO.FullName.Trim().Length > 4 && updateClientDTO.Password.Trim().ToLower().Length > 6)
                {
                    clientToModify.FullName = updateClientDTO.FullName;
                    clientToModify.Email = updateClientDTO.Email;
                    clientToModify.Password = updateClientDTO.Password;
                    clientToModify.Location = updateClientDTO.Location;
                    _clientRepository.Update(clientToModify);
                    return true;
                }
            }
            return false;
            
        }

        public Client? DeleteClient(int id)
        {
            Client? client = _clientRepository.GetById(id);
            if (client != null)
            {
                client.State = UserState.Inactive;
                _clientRepository.Update(client);
                return client;
            }
            return null;
        }

    }
}
