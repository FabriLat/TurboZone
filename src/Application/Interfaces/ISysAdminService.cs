using Application.Models.Requests.SysAdmins;
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
        SysAdminDTO? CreateSysAdmin(CreateSysAdminDTO dto);

        public bool UpdateSysAdmin(UpdateSysAdminDTO dto, int id);

        List<SysAdminDTO> GetAll();
    }
}
