using HomeWork3.BusinessLogic;
using HomeWork3.Messages;
using MassTransit;
using System.Threading.Tasks;

namespace HomeWork3.ConsumerHandler
{
    public class UpdateDBConsumer : IConsumer<SendUser>
    {
        private readonly UpdateUserInfoRequestHandler _updateUserInfoRequestHandler;

        public UpdateDBConsumer(UpdateUserInfoRequestHandler updateUserInfoRequestHandler)
        {
            _updateUserInfoRequestHandler = updateUserInfoRequestHandler;
        }

        public async Task Consume(ConsumeContext<SendUser> context)
        {
           var user =  context.Message.user;

            await _updateUserInfoRequestHandler.Handle(user);

        }
    }
}
