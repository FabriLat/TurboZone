using Application.Interfaces;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public List<FeatureDTO> GetAllFeatures()
        {
           List<Feature> features = _featureRepository.GetAllFeatures();

             List<FeatureDTO> featureDTOs = features.Select(f => new FeatureDTO
              {
                Id = f.Id,
                Name = f.Name,
              }).ToList();
            return featureDTOs;




        }
    }
}
