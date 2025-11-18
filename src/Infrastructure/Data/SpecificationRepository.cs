using Domain.Entities;
using Domain.Interfaces;


namespace Infrastructure.Data
{
    public class SpecificationRepository : RepositoryBase<Specification>, ISpecificationRepository
    {

        public SpecificationRepository(ApplicationContext context) : base(context)
        {
        }

        public Specification? GetByVehicleId(int vehicleId)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Specifications.FirstOrDefault(s => s.VehicleId == vehicleId);
        }


        public bool AddSpecification(Specification specification)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            appDbContext.Specifications.Add(specification);
            return appDbContext.SaveChanges() > 0;
        }

    }
}
