using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ExportService> _logger;

        public ExportService(IExportRepository exportRepository, IMapper mapper, ILogger<ExportService> logger)
        {
            _exportRepository = exportRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ExportGroupDto>> ExportAllSchedulesAsync(int page)
        {
            try
            {
                var groups = await _exportRepository.GetAllGroupsAsync(page);
                return _mapper.Map<List<ExportGroupDto>>(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while exporting all schedules for page {Page}.", page); 
                throw new ExportServiceException("An error occurred while exporting all schedules.", ex);
            }
        }

        public async Task<ExportGroupDto> ExportScheduleByGroupAsync(int groupNumber)
        {
            try
            {
                var group = await _exportRepository.GetGroupByNumberAsync(groupNumber);

                if (group == null)
                {
                    _logger.LogWarning("Group with number {GroupNumber} not found.", groupNumber); 
                    throw new GroupNotFoundException(groupNumber);
                }

                return _mapper.Map<ExportGroupDto>(group);
            }
            catch (GroupNotFoundException ex)
            {
                _logger.LogWarning(ex, "Group not found with number {GroupNumber}.", groupNumber); 
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while exporting schedule for group {GroupNumber}.", groupNumber); 
                throw new ExportServiceException("An error occurred while exporting the schedule.", ex);
            }
        }
    }
}
