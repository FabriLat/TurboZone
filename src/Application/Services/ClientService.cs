using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
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
        private readonly IUserRepository _userRepository;

        public ClientService(IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public ClientDTO? CreateNewClient(CreateClientDTO createClientDTO)
        {
            var nameUsed = _userRepository.GetByFullName(createClientDTO.FullName);
            var emailUsed = _userRepository.GetByEmail(createClientDTO.Email);
            if (nameUsed == null && emailUsed == null)
            {

                if (createClientDTO.FullName.Trim().Length > 4 &&
                    createClientDTO.Password.Trim().ToLower().Length > 6 &&
                    createClientDTO.Password == createClientDTO.ConfirmPassword)
                {
                    int spacesName = createClientDTO.FullName.Count(c => c == ' ');
                    int spacesPassword = createClientDTO.Password.Count(c => c == ' ');
                    if (spacesName > 2 || spacesPassword > 0)
                        return null;


                    try
                    {

                    Client client = new Client();
                    client.FullName = createClientDTO.FullName;
                    client.Email = createClientDTO.Email;
                    client.Password = createClientDTO.Password;
                    client.Location = createClientDTO.Location;
                    _clientRepository.Add(client);
                    return ClientDTO.Create(client);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("El error: "+  ex.InnerException?.Message);
                    }

                    
                }
                return null;
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

        public bool UpdateClient(UpdateClientDTO updateClientDTO, int id, int userId)
        {
            Client? clientToModify = _clientRepository.GetById(id);
                
            if (clientToModify != null && clientToModify.Id == userId)
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

        public Client? DeleteClient(int id, int userId)
        {
            Client? clientToDelete = _clientRepository.GetById(id);
            var user = _userRepository.GetById(userId);

            if(user != null && clientToDelete != null)
            {
                Console.WriteLine(user.FullName +" " + user.Id + " y " + clientToDelete.FullName + " "+ clientToDelete.Id);
                if (user.Rol == UserRol.Moderator || user.Rol == UserRol.SysAdmin)
                {
                    _clientRepository.Delete(clientToDelete);
                    return clientToDelete;
                }
                else if(user.Id == clientToDelete.Id)
                {
                    _clientRepository.Delete(clientToDelete);
                    return clientToDelete;
                }
            }
            return null;
        }

    }
}
