using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IViewService
    {
        void LogView(int vehicleId, int? userId);

        int GetTotalViewsByVehicle(int vehicleId);
    }
}
