using Application.Interfaces;
using Application.Models.Requests;
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


       public  void CreateModerator(CreateModeratorDTO dto)
        {
            Moderator moderator = new Moderator();
            moderator.FullName = dto.FullName;
            moderator.Email = dto.Email;
            moderator.Password = dto.Password;
            moderator.Location = dto.Location;
            moderator.Rol = UserRol.Moderator;
            _moderatorRepository.Add(moderator);
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

        public void UpdateModerator(UpdateModeratorDTO dto)
        {
            Moderator? moderatorToModify = _moderatorRepository.GetById(dto.Id);
            if (moderatorToModify != null) 
            {
                moderatorToModify.FullName = dto.FullName;
                moderatorToModify.Email = dto.Email;
                moderatorToModify.Password = dto.Password;
                moderatorToModify.Location = dto.Location;
                _moderatorRepository.Update(moderatorToModify);
            }

            
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
