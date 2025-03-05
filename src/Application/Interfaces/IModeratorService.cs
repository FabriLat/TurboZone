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
    public interface IModeratorService
    {
        Moderator CreateModerator(CreateModeratorDTO createModeratorDTO);

        List<ModeratorDTO> GetAllModerators();

        bool UpdateModerator(UpdateModeratorDTO moderatorDTO, int id);

        void DeleteModerator(int moderatorId);
    }
}
