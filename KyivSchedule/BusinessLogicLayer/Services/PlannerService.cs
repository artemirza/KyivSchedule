using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PlannerService : IPlannerService
    {
        private readonly IPlannerRepository _plannerRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ITimeRangeValidationService _timeRangeValidationService;
        private readonly IMapper _mapper;
        private readonly ILogger<PlannerService> _logger;

        public PlannerService(IPlannerRepository plannerRepository, IFileRepository fileRepository, 
            ITimeRangeValidationService timeRangeValidationService, IMapper mapper, ILogger<PlannerService> logger)
        {
            _plannerRepository = plannerRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _logger = logger;
            _timeRangeValidationService = timeRangeValidationService;
        }

        public async Task ImportFromFileAsync(IFormFile file)
        {
            try
            {
                var groups = await _fileRepository.ReadGroupsFromFileAsync(file);
                await _plannerRepository.SaveOrUpdateGroupsAsync(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while importing groups from the file.");
                throw;
            }
        }

        public async Task UpdateOutageTimesAsync(int groupNumber, List<string> newOutageTimesStrings)
        {
            try
            {
                var newOutageTimes = new List<TimeRange>();

                foreach (var outageTimeString in newOutageTimesStrings)
                {
                    var timeRanges = _timeRangeValidationService.ParseAndValidateTimeRanges(outageTimeString);
                    newOutageTimes.AddRange(timeRanges);
                }

                await _plannerRepository.UpdateOutageTimesAsync(groupNumber, newOutageTimes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating outage times for group {groupNumber}.");
                throw;
            }
        }

        public async Task<bool> IsGroupInOutageNowAsync(int groupNumber)
        {
            try
            {
                var group = await _plannerRepository.GetGroupByNumberAsync(groupNumber);

                if (group == null)
                {
                    throw new GroupNotFoundException(groupNumber);
                }

                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                bool isInOutage = group.OutageTimes.Any(tr => currentTime >= tr.Start && currentTime <= tr.End);

                return isInOutage;
            }
            catch (GroupNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Group {groupNumber} was not found."); 
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking outage status for group {groupNumber}."); 
                throw;
            }
        }

        public async Task DeleteGroupAsync(int groupNumber)
        {
            try
            {
                var group = await _plannerRepository.GetGroupByNumberAsync(groupNumber);

                if (group == null)
                {
                    throw new GroupNotFoundException(groupNumber);
                }

                await _plannerRepository.DeleteGroupAsync(group);
            }
            catch (GroupNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Group {groupNumber} was not found."); 
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting group {groupNumber}."); 
                throw;
            }
        }
    }
}
