using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ExportService : IExportService
    {
        private readonly IExportRepository _exportRepository;
        private readonly IMapper _mapper;

        public ExportService(IExportRepository exportRepository, IMapper mapper)
        {
            _exportRepository = exportRepository;
            _mapper = mapper;
        }

        public async Task<List<ExportGroupDto>> ExportAllSchedulesAsync(int page)
        {
            var groups = await _exportRepository.GetAllGroupsAsync(page);
            return _mapper.Map<List<ExportGroupDto>>(groups);
        }

        public async Task<ExportGroupDto> ExportScheduleByGroupAsync(int groupNumber)
        {
            var group = await _exportRepository.GetGroupByNumberAsync(groupNumber);

            if (group == null)
            {
                throw new GroupNotFoundException(groupNumber);
            }

            return _mapper.Map<ExportGroupDto>(group);
        }
    }
}
