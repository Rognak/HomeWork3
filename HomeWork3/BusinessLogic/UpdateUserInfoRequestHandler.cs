using HomeWork3.Models;
using HomeWork3.Services.Interfaces;
using System.Threading.Tasks;

namespace HomeWork3.BusinessLogic
{
    public class UpdateUserInfoRequestHandler
    {
        private readonly IUpdateUserInfo _userUpdateInfo;

        public UpdateUserInfoRequestHandler(IUpdateUserInfo userUpdateInfo)
        {
            _userUpdateInfo = userUpdateInfo;
        }

        public Task Handle(User user)
        {
           return _userUpdateInfo.Update(user);
        }
    }
}
