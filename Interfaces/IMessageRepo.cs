using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUM.Interfaces
{
    public interface IMessageRepo : IGenericRepo<Message>
    {
        Task<IReadOnlyList<Message>> GetMessagesForUser(int userId);
    }
}