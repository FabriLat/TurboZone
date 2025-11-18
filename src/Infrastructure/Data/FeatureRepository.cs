using Application.Models.Responses;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
    {
        public FeatureRepository(ApplicationContext context) : base(context)
        { }

        public List<Feature> GetAllFeatures()
        {
            var appDbContext = (ApplicationContext)_dbContext;

            return appDbContext.Features.ToList();
        }


        public List<Feature> GetFeaturesByIds(List<int> featureIds)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Features
                .Where(f => featureIds.Contains(f.Id))
                .ToList();
        }
    }
}
