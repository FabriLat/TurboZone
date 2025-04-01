using Application.Interfaces;
using Application.Models.Requests.Moderators;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ModeratorService : IModeratorService
    {
        private readonly IModeratorRepository _moderatorRepository;
        public ModeratorService(IModeratorRepository moderatorRepository)
        {
            _moderatorRepository = moderatorRepository;
        }


       public  Moderator? CreateModerator(CreateModeratorDTO dto)
        {
            if (dto.FullName.Trim().Length > 4 && dto.Password.Trim().ToLower().Length > 6 && dto.Password == dto.ConfirmPassword)
            {
                Moderator moderator = new Moderator();
                moderator.FullName = dto.FullName;
                moderator.Email = dto.Email;
                moderator.phoneNumber = dto.PhoneNumber;
                moderator.Password = dto.Password;
                moderator.Location = dto.Location;
                moderator.Rol = UserRol.Moderator;
                _moderatorRepository.Add(moderator);
                return moderator;
            }
           return null;
        }


        public List<ModeratorDTO> GetAllModerators()
        {
            List<ModeratorDTO> moderatorDTOs = new List<ModeratorDTO>();

            var moderators = _moderatorRepository.GetAll();

            foreach ( var moderator in moderators )
            {
                var moderatorDTO = ModeratorDTO.Create(moderator);
                moderatorDTOs.Add(moderatorDTO);
            }
            return moderatorDTOs;
        }

        public bool UpdateModerator(UpdateModeratorDTO dto, int id)
        {
            Moderator? moderatorToModify = _moderatorRepository.GetById(id);
            if (moderatorToModify != null) 
            {
              if (dto.FullName.Trim().Length > 4 && dto.Password.Trim().Length > 6)
                {
                moderatorToModify.FullName = dto.FullName;
                moderatorToModify.Email = dto.Email;
                moderatorToModify.phoneNumber = dto.PhoneNumber;
                moderatorToModify.Password = dto.Password;
                moderatorToModify.Location = dto.Location;
                _moderatorRepository.Update(moderatorToModify);
                  return true;
                }
              return false;
            }
            return false;  
        }


        public void DeleteModerator(int id)
        {
            Moderator? moderatorToDelete = _moderatorRepository.GetById(id);

            if (moderatorToDelete != null)
            {
                moderatorToDelete.State = UserState.Inactive;
                _moderatorRepository.Update(moderatorToDelete);
            }
            

        }

    }
}
