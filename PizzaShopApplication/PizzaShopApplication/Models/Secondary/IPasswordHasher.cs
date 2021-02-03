using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Secondary
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool IsPasswordMathcingHash(string password, string savedPasswordHash);
    }
}
