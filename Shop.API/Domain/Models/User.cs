using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}