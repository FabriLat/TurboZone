using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        List<Vehicle> GetAllVehicles();

        List<Vehicle> GetPendingVehicles();

        List<Vehicle> GetActiveVehicles();

        Vehicle GetById(int id);
    }
}
