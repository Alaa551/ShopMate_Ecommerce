using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DAL.Repository.Abstraction
{
    internal interface ICategoryRepository
    {
        Task<IEnumerable<Category>> SearchAsync(string keyword);
    }
}
