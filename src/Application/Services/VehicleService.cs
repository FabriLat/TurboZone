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
        private readonly IImageService _imageService;
        public VehicleService(IVehicleRepository vehicleRepository, IImageService imageService)
        {
            _vehicleRepository = vehicleRepository;
            _imageService = imageService;
        }

        public bool? CreateVehicle(CreateVehicleDTO vehicle, int userId)
        {
            Vehicle newVehicle = new Vehicle();
            UploadImageDTO uploadImage = new UploadImageDTO();
            if(userId < 0 || string.IsNullOrWhiteSpace(vehicle.Brand) ||
                string.IsNullOrWhiteSpace(vehicle.Model) ||
                string.IsNullOrWhiteSpace(vehicle.Year) ||
                string.IsNullOrWhiteSpace(vehicle.Color) ||
                string.IsNullOrWhiteSpace(vehicle.Transmission) ||
                string.IsNullOrWhiteSpace(vehicle.Price.ToString()) ||
                string.IsNullOrWhiteSpace(vehicle.Price.ToString()))
            {
                return null;
            }

            newVehicle.Brand=vehicle.Brand;
            newVehicle.Model=vehicle.Model;
            newVehicle.Year=vehicle.Year;
            newVehicle.Color=vehicle.Color;
            newVehicle.Transmission=vehicle.Transmission;
            newVehicle.MaxSpeed=vehicle.MaxSpeed;
            newVehicle.Price=vehicle.Price;
            newVehicle.SellerId = userId;
            newVehicle.State = VehicleState.PendingCreate;
            var createdVehicle = _vehicleRepository.Add(newVehicle);
            
            uploadImage.VehicleId = createdVehicle.Id;
            uploadImage.ImageName=vehicle.ImageName;
            uploadImage.ImageUrl = vehicle.ImageUrl;
            _imageService.UploadImage(uploadImage , userId);
            
            return true;
        }

        public void DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDTO> GetAllVehicles()
        {
            List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
            var vehicles = _vehicleRepository.GetAllVehicles();
            
            foreach (var vehicle in vehicles)
            {
                VehicleDTO vehicleDTO = VehicleDTO.Create(vehicle);
                vehicleDTOs.Add(vehicleDTO);
            }
            return vehicleDTOs;
        }


        public List<VehicleDTO> GetActiveVehicles()
        {
            List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
            var vehicles = _vehicleRepository.GetActiveVehicles();

            foreach (var vehicle in vehicles)
            {
                VehicleDTO vehicleDTO = VehicleDTO.Create(vehicle);
                vehicleDTOs.Add(vehicleDTO);
            }
            return vehicleDTOs;
        }



        public VehicleDTO? GetVehicleById(int id)
        {
            Vehicle? vehicle = _vehicleRepository.GetById(id);
            if (vehicle != null)
            {
                VehicleDTO dto = VehicleDTO.Create(vehicle);
                return dto;
            }
            return null;
        }

        public Vehicle? UpdateVehicle(UpdateVehicleDTO vehicle, int userId, int vehicleId)
        {

            if (userId < 0 || string.IsNullOrWhiteSpace(vehicle.Brand) ||
                string.IsNullOrWhiteSpace(vehicle.Model) ||
                string.IsNullOrWhiteSpace(vehicle.Year) ||
                string.IsNullOrWhiteSpace(vehicle.Color) ||
                string.IsNullOrWhiteSpace(vehicle.Transmission) ||
                string.IsNullOrWhiteSpace(vehicle.Price.ToString()) ||
                string.IsNullOrWhiteSpace(vehicle.Price.ToString()))
            {
                return null;
            }


            Vehicle? vehicleToUpdate = _vehicleRepository.GetById(vehicleId);
            if (vehicleToUpdate != null && vehicleToUpdate.SellerId == userId)
            {
                vehicleToUpdate.Brand = vehicle.Brand;
                vehicleToUpdate.Model = vehicle.Model;
                vehicleToUpdate.Year = vehicle.Year;
                vehicleToUpdate.Color = vehicle.Color;
                vehicleToUpdate.Transmission = vehicle.Transmission;
                vehicleToUpdate.MaxSpeed = vehicle.MaxSpeed;
                vehicleToUpdate.Price = vehicle.Price;
                vehicleToUpdate.State = VehicleState.PendingUpdate;
                _vehicleRepository.Update(vehicleToUpdate);
                return vehicleToUpdate;
            }
            return null;
        }

        public List<VehicleDTO> GetPendingVehicles()
        {
            var pendingVehicles = _vehicleRepository.GetPendingCreateVehicles();
            List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
            if (pendingVehicles != null)
            {
                foreach (var vehicle in pendingVehicles)
                {
                    VehicleDTO vehicleDTO = VehicleDTO.Create(vehicle);
                    vehicleDTOs.Add(vehicleDTO);
                }
                return vehicleDTOs;
            }
            return null;

        }

        public List<VehicleDTO> GetPendingUpdateVehicles()
        {
            var pendingVehicles = _vehicleRepository.GetPendingUpdateVehicles();
            List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
            if (pendingVehicles != null)
            {
                foreach (var vehicle in pendingVehicles)
                {
                    VehicleDTO vehicleDTO = VehicleDTO.Create(vehicle);
                    vehicleDTOs.Add(vehicleDTO);
                }
                return vehicleDTOs;
            }
            return null;

        }

        public bool ChangeVehicleState(int id, string newState)
        {
            Vehicle? vehicle = _vehicleRepository.GetById(id);
            if (vehicle != null)
            {
                if (newState == "Active")
                { 
                vehicle.State = VehicleState.Active;
                }else if(newState == "Pending")
                {
                    vehicle.State = VehicleState.PendingUpdate;
                }
                _vehicleRepository.Update(vehicle);
                return true;
            }
            return false;
        }

    }
}
