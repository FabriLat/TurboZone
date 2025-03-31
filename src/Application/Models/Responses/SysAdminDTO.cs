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
    public class SysAdminDTO : UserDTO
    {

        public static SysAdminDTO Create(SysAdmin sysAdmin)
        {
            SysAdminDTO sysAdminDTO = new SysAdminDTO();
            sysAdminDTO.FullName = sysAdmin.FullName;
            sysAdminDTO.Email = sysAdmin.Email;
            sysAdminDTO.PhoneNumber = sysAdmin.phoneNumber;
            sysAdminDTO.Location = sysAdmin.Location;
            return sysAdminDTO;
        }


    }
}
