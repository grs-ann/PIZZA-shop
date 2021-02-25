using PizzaShopApplication.Models.Data.Entities.Authentification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Authentification
{
    /// <summary>
    /// Represents a user enitity in database "Users" table.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        // User login.
        public string Login { get; set; }
        public string Password { get; set; }
        // User name.
        public string Name { get; set;}

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
