using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFeatureRepository : IRepositoryBase<Feature>
    {
        List<Feature> GetAllFeatures();

        List<Feature> GetFeaturesByIds(List<int> featureIds);
    }
}
