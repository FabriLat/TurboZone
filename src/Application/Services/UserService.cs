using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Requests.Clients;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO? GetUserById(int userId)
        {
            User? user = _userRepository.GetById(userId);

            if(user != null && user.State == UserState.Active)
            {
                UserDTO result = user.Rol.ToString() switch
                {
                    "Client" => new ClientDTO
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Rol = user.Rol,
                        Email = user.Email,
                        PhoneNumber = user.phoneNumber,
                        Location = user.Location,
                        ImageUrl = user.ImageUrl
                    },
                    "Moderator" => new ModeratorDTO
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Rol = user.Rol,
                        Email = user.Email,
                        PhoneNumber = user.phoneNumber,
                        Location = user.Location,
                        ImageUrl = user.ImageUrl

                    },
                    "SysAdmin" => new SysAdminDTO
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Rol = user.Rol,
                        Email = user.Email,
                        PhoneNumber = user.phoneNumber,
                        Location = user.Location,
                        ImageUrl = user.ImageUrl

                    },
                    _ => throw new InvalidOperationException("Rol no soportado")
                };
                return result;
            };
            return null;
            
            
        }


        public bool UpdateUser(int userId, UpdateUserDTO updateUserDTO, int id)
        {
            User? userToModify = _userRepository.GetById(id);

            if (userToModify != null && userToModify.Id == userId)
            {
                if (updateUserDTO.FullName.Trim().Length > 4 && updateUserDTO.Password.Trim().ToLower().Length > 6 && userToModify.Password == updateUserDTO.Password)
                {
                    userToModify.FullName = updateUserDTO.FullName;
                    userToModify.Email = updateUserDTO.Email;
                    userToModify.phoneNumber = updateUserDTO.PhoneNumber;
                    userToModify.ImageUrl = updateUserDTO.ImageUrl;
                    userToModify.Password = updateUserDTO.NewPassword;
                    userToModify.Location = updateUserDTO.Location;
                    _userRepository.Update(userToModify);
                    return true;
                }
                else if (updateUserDTO.FullName.Trim().Length > 4)
                {
                    userToModify.FullName = updateUserDTO.FullName;
                    userToModify.Email = updateUserDTO.Email;
                    userToModify.phoneNumber = updateUserDTO.PhoneNumber;
                    userToModify.ImageUrl = updateUserDTO.ImageUrl;
                    userToModify.Location = updateUserDTO.Location;
                    _userRepository.Update(userToModify);
                    return true;
                }
            }
            return false;
        }


    }
}
