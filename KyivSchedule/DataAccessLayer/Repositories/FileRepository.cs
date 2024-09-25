using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ILineParserService _lineParserService;

        public FileRepository(ILineParserService lineParserService)
        {
            _lineParserService = lineParserService;
        }

        public async Task<List<Group>> ReadGroupsFromFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var groups = new List<Group>();

            using var reader = new StreamReader(file.OpenReadStream());

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var group = _lineParserService.ParseLine(line);
                groups.Add(group);
            }

            return groups;
        }
    }
}
