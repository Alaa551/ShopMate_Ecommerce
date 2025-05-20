using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DAL.Repository.product_repo
{
    internal interface IProductService
    {
        Task<IEnumerable<Product>> SearchAsync(string keyword);
    }
}
