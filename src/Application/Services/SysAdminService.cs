using Application.Interfaces;
using Application.Models.Requests.Moderators;
using Application.Models.Requests.SysAdmins;
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

        public SysAdminDTO? CreateSysAdmin(CreateSysAdminDTO dto)
        {
            if(dto.Password == dto.ConfirmPassword)
            {
                var sysAdmin = new SysAdmin();
                sysAdmin.FullName = dto.FullName;
                sysAdmin.Email = dto.Email;
                sysAdmin.phoneNumber = dto.PhoneNumber;
                sysAdmin.Password = dto.Password;
                sysAdmin.Location = dto.Location;
                sysAdmin.Rol = Domain.Enums.UserRol.SysAdmin;
                _sysAdminRepository.Add(sysAdmin);
                return SysAdminDTO.Create(sysAdmin);
            }
            return null;

        }

        public bool UpdateSysAdmin(UpdateSysAdminDTO dto, int id)
        {
            SysAdmin? sysAdminToModify = _sysAdminRepository.GetById(id);
            if (sysAdminToModify != null)
            {
                if (dto.FullName.Trim().Length > 4 && dto.Password.Trim().Length > 6 && dto.Password == sysAdminToModify.Password)
                {
                    sysAdminToModify.FullName = dto.FullName;
                    sysAdminToModify.Email = dto.Email;
                    sysAdminToModify.phoneNumber = dto.PhoneNumber;
                    sysAdminToModify.Password = dto.Password;
                    sysAdminToModify.Location = dto.Location;
                    _sysAdminRepository.Update(sysAdminToModify);
                    return true;
                }
                return false;
            }
            return false;
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
                sysAdmin.PhoneNumber = admin.phoneNumber;
                sysAdmin.Location = admin.Location;
                sysAdmin.Rol = admin.Rol;
                dtosList.Add(sysAdmin);
            }
            return dtosList ?? [];
        }
    }
}
