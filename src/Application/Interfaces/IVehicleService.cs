using Application.Models.Requests.Vehicles;
using Application.Models.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleService
    {
       List<VehicleDTO> GetAllVehicles();

        List<VehicleDTO> GetActiveVehicles();

       VehicleDTO? GetVehicleById(int id);

        VehicleDTO? CreateVehicle(CreateVehicleDTO vehicle, int userId);
        
       Vehicle? UpdateVehicle(UpdateVehicleDTO vehicle, int userId, int vehicleId);

        void DeleteVehicle(int id);

        List<VehicleDTO> GetPendingVehicles();

        public List<VehicleDTO>? GetPendingUpdateVehicles();

        public bool ChangeVehicleState(int id, string newState);

    }
}
