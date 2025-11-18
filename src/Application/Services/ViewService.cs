using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ViewService : IViewService
    {
        private readonly IViewRepository _viewRepository;
        public ViewService(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public void LogView(int vehicleId, int? userId)
        {
            
            var viewedAt = DateTime.UtcNow;
            VehicleView newView = new VehicleView
            {
                VehicleId = vehicleId,
                UserId = userId,
                ViewedAt = viewedAt
            };

            _viewRepository.Add(newView);

        }

        public int GetTotalViewsByVehicle(int vehicleId)
        {
            return _viewRepository.GetTotalViewsByVehicle(vehicleId);
        }

    }
}
