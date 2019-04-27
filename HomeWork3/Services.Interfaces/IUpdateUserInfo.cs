using HomeWork3.Models;
using System.Threading.Tasks;

namespace HomeWork3.Services.Interfaces
{
    public interface IUpdateUserInfo
    {
        Task Update(User user);
    }
}
