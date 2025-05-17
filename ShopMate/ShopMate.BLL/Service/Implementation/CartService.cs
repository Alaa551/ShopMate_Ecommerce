using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BLL.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartReposit ory _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItem>> SearchCartAsync(int id)
        {
            return await _cartRepository.SearchAsync(id);
        }
    }

}
