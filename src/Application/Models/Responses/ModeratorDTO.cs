using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class ModeratorDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserRol Rol {  get; set; }

        public UserState State { get; set; }


        public static ModeratorDTO Create(Moderator moderator)
        {
            ModeratorDTO dto = new ModeratorDTO();
            dto.Id = moderator.Id;
            dto.FullName = moderator.FullName;
            dto.Email = moderator.Email;
            dto.Rol = moderator.Rol;
            dto.State = moderator.State;
            return dto;
        }

    }
}
