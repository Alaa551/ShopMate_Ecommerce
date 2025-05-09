using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> SearchCartAsync(int id);

    }
}
