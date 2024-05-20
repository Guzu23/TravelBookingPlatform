using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Active { get; set; }

        public string ImageProfile { get; set; }

        public User() { }

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            Active = true;
        }

        public void ActivateUser()
        {
            Active = true;
        }

        public void DeactivateUser()
        {
            Active = false;
        }
    }
}
