using Application.Interfaces;
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
    }
}
