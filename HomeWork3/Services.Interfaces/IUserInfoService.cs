using HomeWork3.Models;
using System;
using System.Threading.Tasks;

namespace HomeWork3.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<User> GetById(Guid id);
    }
}
