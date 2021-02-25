using PizzaShopApplication.Models.Data.Entities.Authentification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Review
{
    /// <summary>
    /// Represents database table, contained
    /// different users reviews.
    /// </summary>
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public DateTime CommentDateTime { get; set; }
        public string Comment { get; set; }
        // Link to "User" entity.
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
