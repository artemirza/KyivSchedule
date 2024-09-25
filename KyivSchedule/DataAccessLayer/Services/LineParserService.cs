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
        public Group ParseLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("The input line cannot be empty.");

            var parts = line.Split('.');
            if (parts.Length != 2)
                throw new FormatException("Invalid format. Expected format: 'Group.Time'.");

            if (!int.TryParse(parts[0], out var groupNumber) || groupNumber <= 0)
                throw new FormatException("Group number must be a positive integer.");

            var timeRanges = new List<TimeRange>();
            var timeParts = parts[1].Split(';');

            foreach (var timePart in timeParts)
            {
                var times = timePart.Split('-');
                if (times.Length != 2)
                    throw new FormatException("Invalid time range format. Expected format: 'start-end'.");

                if (!ValidateTime(times[0], out var startTime))
                {
                    throw new FormatException($"Invalid start time: '{times[0]}'. Expected format is 'HH:mm' with valid hours and minutes.");
                }

                if (!ValidateTime(times[1], out var endTime))
                {
                    throw new FormatException($"Invalid end time: '{times[1]}'. Expected format is 'HH:mm' with valid hours and minutes.");
                }

                if (startTime >= endTime)
                {
                    throw new FormatException($"Start time '{times[0]}' must be earlier than end time '{times[1]}'.");
                }

                timeRanges.Add(new TimeRange { Start = startTime, End = endTime });
            }

            return new Group { GroupId = groupNumber, OutageTimes = timeRanges };
        }

        private bool ValidateTime(string timeString, out TimeSpan time)
        {
            time = default;

            if (!TimeSpan.TryParseExact(timeString, @"hh\:mm", null, out var parsedTime)) 
            {
                return false; 
            }

            if (parsedTime.Hours < 0 || parsedTime.Hours > 23 || parsedTime.Minutes < 0 || parsedTime.Minutes > 59)
            {
                return false; 
            }

            time = parsedTime;
            return true;
        }
    }
}
