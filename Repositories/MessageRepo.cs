using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUM.Data;
using RUM.Interfaces;

namespace RUM.Repositories
{
    public class MessageRepo : GenericRepo<Message>, IMessageRepo
    {

        private readonly AppDbContext _context;
        public MessageRepo(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<Message>> GetMessagesForUser(int userId)
        {
            return await _context.messages
          .AsNoTracking()
          .Where(msg => msg.UserId == userId)
          .OrderByDescending(msg => msg.CreatedAt)
          .ToListAsync();
        }
    }
}