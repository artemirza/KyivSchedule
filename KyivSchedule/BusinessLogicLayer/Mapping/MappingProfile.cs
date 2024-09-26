using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, ExportGroupDto>()
                .ForMember(dest => dest.OutageTimes, opt => opt.MapFrom(src => src.OutageTimes));

            CreateMap<TimeRange, ExportTimeRangeDto>();
        }
    }
}
