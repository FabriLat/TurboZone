using Application.Interfaces;
using Application.Models.Requests.Specifications;
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
    public class SpecificationService : ISpecificationService
    {
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;
        private readonly ISpecificationRepository _specificationRepository;
        public SpecificationService(IVehicleService vehicleService, ISpecificationRepository specificationRepository, IUserService userService)
        {
            _vehicleService = vehicleService;
            _specificationRepository = specificationRepository;
            _userService = userService;
        }

        public bool AddSpecification(SpecificationDTO dto)
        {
            
            VehicleDTO vehicle = _vehicleService.GetVehicleById(dto.VehicleId);
            if (vehicle == null)
            {
                return false;
            }
            var user = _userService.GetUserById(vehicle.SellerId);
            if (user == null)
            {
                return false;
            }
            if(user.Id == vehicle.SellerId || user.Rol == Domain.Enums.UserRol.Moderator || user.Rol == Domain.Enums.UserRol.SysAdmin)
            {
                Specification specification = new Specification
                {
                    Engine = dto.Engine,
                    Power = dto.Power,
                    Torque = dto.Torque,
                    Acceleration = dto.Acceleration,
                    FuelConsumption = dto.FuelConsumption,
                    Co2Emissions = dto.Co2Emissions,
                    Doors = dto.Doors,
                    Seats = dto.Seats,
                    Weight = dto.Weight,
                    VehicleId = dto.VehicleId
                };
                _specificationRepository.AddSpecification(specification);
                return true;
            }
            return false;


           

        }

        public SpecificationDTO? GetSpecificationByVehicleId(int vehicleId)
        {
            Specification? specification = _specificationRepository.GetByVehicleId(vehicleId);
            if (specification == null)
            {
                return null;
            }
            SpecificationDTO dto = new SpecificationDTO
            {
                Engine = specification.Engine,
                Power = specification.Power,
                Torque = specification.Torque,
                Acceleration = specification.Acceleration,
                FuelConsumption = specification.FuelConsumption,
                Co2Emissions = specification.Co2Emissions,
                Doors = specification.Doors,
                Seats = specification.Seats,
                Weight = specification.Weight,
                VehicleId = specification.VehicleId
            };
            return dto;

        }
    }
}
