﻿using Application.Models.Requests;
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

       VehicleDTO GetVehicleById(int id);

        void CreateVehicle(CreateVehicleDTO vehicle, int userId);
        
       Vehicle? UpdateVehicle(UpdateVehicleDTO vehicle, int userId);

        void DeleteVehicle(int id);
    }
}
