using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class LineParserService : ILineParserService
    {
        private readonly ITimeRangeValidationService _timeRangeValidationService;

        public LineParserService(ITimeRangeValidationService timeRangeValidationService)
        {
            _timeRangeValidationService = timeRangeValidationService;
        }

        public Group ParseLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("The input line cannot be empty.");

            var parts = line.Split('.');
            if (parts.Length != 2)
                throw new FormatException("Invalid format. Expected format: 'Group.Time'.");

            if (!int.TryParse(parts[0], out var groupNumber) || groupNumber <= 0)
                throw new FormatException("Group number must be a positive integer.");

            var timeRanges = _timeRangeValidationService.ParseAndValidateTimeRanges(parts[1]);

            return new Group { GroupId = groupNumber, OutageTimes = timeRanges };
        }
    }
}
