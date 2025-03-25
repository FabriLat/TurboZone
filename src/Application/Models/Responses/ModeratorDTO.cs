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
    public class ModeratorDTO : UserDTO
    {


        public static ModeratorDTO Create(Moderator moderator)
        {
            ModeratorDTO dto = new ModeratorDTO();
            dto.Id = moderator.Id;
            dto.FullName = moderator.FullName;
            dto.Email = moderator.Email;
            dto.Location = moderator.Location;
            dto.ImageUrl = moderator.ImageUrl;
            dto.Rol = moderator.Rol;    
            dto.State = moderator.State;
            return dto;
        }

    }
}
