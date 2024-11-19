using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IModeratorService
    {
        void CreateModerator(CreateModeratorDTO createModeratorDTO);

        List<ModeratorDTO> GetAllModerators();

        void UpdateModerator(UpdateModeratorDTO moderatorDTO);

        void DeleteModerator(int moderatorId);
    }
}
