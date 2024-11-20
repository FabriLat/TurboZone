using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public void CreateVehicle(CreateVehicleDTO vehicle, int userId)
        {
            Vehicle newVehicle = new Vehicle();
            newVehicle.Brand=vehicle.Brand;
            newVehicle.Model=vehicle.Model;
            newVehicle.Year=vehicle.Year;
            newVehicle.Color=vehicle.Color;
            newVehicle.Transmission=vehicle.Transmission;
            newVehicle.MaxSpeed=vehicle.MaxSpeed;
            newVehicle.Price=vehicle.Price;
            newVehicle.SellerId = userId;
            newVehicle.State = VehicleState.Pending;
            _vehicleRepository.Add(newVehicle);
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
