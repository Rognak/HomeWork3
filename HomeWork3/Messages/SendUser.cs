using HomeWork3.Models;

namespace HomeWork3.Messages
{
    public class SendUser : ISendUser
    {
       public User user { get; set; }
    }
}
