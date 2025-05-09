using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DAL.Repository.Abstraction
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> SearchAsync(int id);
    }
}
