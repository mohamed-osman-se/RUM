using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RUM.Data;
using RUM.Interfaces;

namespace RUM.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Creat(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}