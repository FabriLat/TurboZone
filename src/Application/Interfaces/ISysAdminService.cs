using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISysAdminService
    {
        void CreateSysAdmin(CreateSysAdminDTO dto);

        List<SysAdminDTO> GetAll();
    }
}
