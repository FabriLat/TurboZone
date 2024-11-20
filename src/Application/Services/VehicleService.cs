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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public void CreateVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDTO> GetAllVehicles()
        {
            throw new NotImplementedException();
        }

        public VehicleDTO GetVehicleById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateVehicle(UpdateVehicleDTO vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
