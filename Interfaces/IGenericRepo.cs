using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUM.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task Creat(T entity);
    }
}