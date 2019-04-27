using HomeWork3.BusinessLogic;
using HomeWork3.Messages;
using HomeWork3.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeWork3.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly GetUsersInfoRequestHandler _getUsersInfoRequestHandler;
        private readonly IBus _bus;
        //private readonly UpdateUserInfoRequestHandler _updateUserInfoRequestHandler;

        public UsersController(GetUsersInfoRequestHandler getUsersInfoRequestHandler,
            UpdateUserInfoRequestHandler updateUserInfoRequestHandler,
            IBus bus)
        {
            _getUsersInfoRequestHandler = getUsersInfoRequestHandler;
            //_updateUserInfoRequestHandler = updateUserInfoRequestHandler;
            _bus = bus;
        }

        [HttpGet("{id}")]
        public Task<User> GetUserInfo(Guid id)
        {
            return _getUsersInfoRequestHandler.Handle(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] User myuser)
        {
            Uri adress = new Uri("rabbitmq://localhost/submit");


            var endpoint = await _bus.GetSendEndpoint(adress);

            await endpoint.Send(new SendUser() { user = myuser });

            return new StatusCodeResult(200);


        }

        //public Task UpdateUserInfo([FromBody] User user)
        //{
          //  return _updateUserInfoRequestHandler.Handle(user);
        //}
    }
}