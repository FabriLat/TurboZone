using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SysAdminService : ISysAdminService
    {
        private readonly ISysAdminRepository _sysAdminRepository;
        public SysAdminService(ISysAdminRepository sysAdminRepository)
        {
            _sysAdminRepository = sysAdminRepository;
        }

        public void CreateSysAdmin(CreateSysAdminDTO dto)
        {
            var sysAdmin = new SysAdmin();
            sysAdmin.FullName = dto.FullName;
            sysAdmin.Email = dto.Email;
            sysAdmin.Password = dto.Password;
            sysAdmin.Location = dto.Location;
            sysAdmin.Rol = Domain.Enums.UserRol.SysAdmin;
            _sysAdminRepository.Add(sysAdmin);
        }

        public List<SysAdminDTO> GetAll()
        {
            var admins = _sysAdminRepository.GetAll();
            var dtosList = new List<SysAdminDTO>();

            foreach (var admin in admins)
            {
                var sysAdmin = new SysAdminDTO();
                sysAdmin.Id = admin.Id;
                sysAdmin.FullName = admin.FullName;
                sysAdmin.Email = admin.Email;
                sysAdmin.Password = admin.Password;
                sysAdmin.Location = admin.Location;
                sysAdmin.Rol = admin.Rol;
                dtosList.Add(sysAdmin);
            }
            return dtosList ?? [];
        }
    }
}
