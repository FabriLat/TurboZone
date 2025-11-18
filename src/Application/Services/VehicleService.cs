using Application.Interfaces;
using Application.Models.Requests.Images;
using Application.Models.Requests.Vehicles;
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
        private readonly IFeatureRepository _featureRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IImageService imageService, IFeatureRepository featureRepository)
        {
            _vehicleRepository = vehicleRepository;
            _imageService = imageService;
            _featureRepository = featureRepository;
        }

        public VehicleDTO? CreateVehicle(CreateVehicleDTO vehicle, int userId)
{
    // Validaciones básicas
    if (userId < 0 ||
        string.IsNullOrWhiteSpace(vehicle.Brand) ||
        string.IsNullOrWhiteSpace(vehicle.Model) ||
        string.IsNullOrWhiteSpace(vehicle.Year) ||
        string.IsNullOrWhiteSpace(vehicle.Color) ||
        string.IsNullOrWhiteSpace(vehicle.Transmission) ||
        vehicle.Price <= 0 ||
        vehicle.MaxSpeed <= 0)
    {
        return null;
    }

    var newVehicle = new Vehicle
    {
        Brand = vehicle.Brand,
        Model = vehicle.Model,
        Year = vehicle.Year,
        Color = vehicle.Color,
        Transmission = vehicle.Transmission,
        MaxSpeed = vehicle.MaxSpeed,
        Price = vehicle.Price,
        OwnerId = userId,
        State = VehicleState.PendingCreate
    };

    // Guardamos el vehículo primero para obtener su Id
    var createdVehicle = _vehicleRepository.Add(newVehicle);

            // Asociar las Features (relación N–N)
            if (vehicle.FeatureIds != null && vehicle.FeatureIds.Count > 0)
            {
                var existingFeatures = _featureRepository.GetFeaturesByIds(vehicle.FeatureIds);

                createdVehicle.Features = existingFeatures;
                _vehicleRepository.Update(createdVehicle);
            }


            // Guardar la imagen (si viene)
            if (!string.IsNullOrWhiteSpace(vehicle.ImageUrl))
    {
        var uploadImage = new UploadImageDTO
        {
            VehicleId = createdVehicle.Id,
            ImageName = vehicle.ImageName,
            ImageUrl = vehicle.ImageUrl
        };

        _imageService.UploadImage(uploadImage, userId);
    }

    return VehicleDTO.Create(createdVehicle);
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
                vehicle.Price <= 0 || vehicle.MaxSpeed <= 0)
            {
                return null;
            }

            Vehicle? vehicleToUpdate = _vehicleRepository.GetById(vehicleId);

            if (vehicleToUpdate != null && vehicleToUpdate.OwnerId == userId)
            {
                vehicleToUpdate.Brand = vehicle.Brand;
                vehicleToUpdate.Model = vehicle.Model;
                vehicleToUpdate.Year = vehicle.Year;
                vehicleToUpdate.Color = vehicle.Color;
                vehicleToUpdate.Transmission = vehicle.Transmission;
                vehicleToUpdate.MaxSpeed = vehicle.MaxSpeed;
                vehicleToUpdate.Price = vehicle.Price;
                vehicleToUpdate.State = VehicleState.PendingUpdate;

                // ✅ Actualizar features correctamente
                vehicleToUpdate.Features.Clear();

                if (vehicle.FeaturesIds != null && vehicle.FeaturesIds.Any())
                {
                    var existingFeatures = _featureRepository.GetFeaturesByIds(vehicle.FeaturesIds);
                    foreach (var feature in existingFeatures)
                    {
                        vehicleToUpdate.Features.Add(feature);
                    }
                }

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


        public List<VehicleDTO>? GetPendingUpdateVehicles()
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
