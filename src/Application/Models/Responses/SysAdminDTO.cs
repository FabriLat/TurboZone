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
    public class SysAdminDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }

        public UserRol Rol {  get; set; }


        public static SysAdminDTO Create(SysAdmin sysAdmnin)
        {
            SysAdminDTO sysAdminDTO = new SysAdminDTO();
            sysAdminDTO.FullName = sysAdmnin.FullName;
            sysAdminDTO.Email = sysAdmnin.Email;
            sysAdminDTO.Password = sysAdmnin.Password;
            sysAdminDTO.Location = sysAdmnin.Location;
            return sysAdminDTO;
        }


    }
}
