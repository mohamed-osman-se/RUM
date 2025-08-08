using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RUM.Models;

namespace RUM.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> Find(string email);
        Task<User?> Find(int id);
    }
}