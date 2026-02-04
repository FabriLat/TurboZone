using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Invalid email adress")]
        public string Email { get; set; }

        [Required]
        public string phoneNumber { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password should be at least 8 characters.")]
        public string Password { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Location { get; set; }

        [Required]
        public UserRol Rol { get; set; } = UserRol.Client;

        public UserState State { get; set; } = UserState.Active;

        public User() { }

        public User(string fullName, string email, string password, string location,UserRol rol)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Location = location;
            Rol = rol;
        }


        
    }
}
