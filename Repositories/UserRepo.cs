using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUM.Data;
using RUM.Interfaces;
using RUM.Models;
using SQLitePCL;

namespace RUM.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {

        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<User?> Find(string email)
        {
            return await _context
            .users
            .SingleOrDefaultAsync(u => u.Email == email);
        }

          public async Task<User?> Find(int id)
        {
            return await _context
            .users
            .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}