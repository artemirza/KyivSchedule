using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFileRepository
    {
        Task<List<Group>> ReadGroupsFromFileAsync(IFormFile file);
    }
}
