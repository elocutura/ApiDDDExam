using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Rebeld.Dtos;
using VY.Rebeld.Business.Contracts.Domain;
using VY.Rebeld.Data.Contracts.Entities;

namespace VY.Rebeld.Business.Impl.MappingProfiles
{
    public class RebeldProfile : Profile
    {
        public RebeldProfile()
        {
            CreateMap<RebeldDto, RebeldDomain>().ReverseMap();
            CreateMap<RebeldDomain, RebeldEntity>().ReverseMap();
        }
    }
}
