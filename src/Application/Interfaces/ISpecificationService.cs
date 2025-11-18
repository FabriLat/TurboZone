using Application.Models.Requests.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISpecificationService
    {
        bool AddSpecification(SpecificationDTO dto);

        SpecificationDTO? GetSpecificationByVehicleId(int vehicleId);
    }
}
